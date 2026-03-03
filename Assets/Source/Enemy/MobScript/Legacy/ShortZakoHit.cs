using UnityEngine;

public class ShortZakoHit : MonoBehaviour
{
    private Zako_shortRich MobEntity;

    [SerializeField] GameObject hpBar;
    private HealthGauge gauge;
    private float timer;

    void Awake()
    {
        MobEntity = GetComponentInParent<Zako_shortRich>();
        if (MobEntity == null)
            Debug.LogError("EntityParent not found in parents of " + gameObject.name);

        gauge = hpBar.GetComponent<HealthGauge>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collided = other.gameObject;
        IWeaponBody weaponBody = collided.GetComponent<IWeaponBody>();
        if (collided.CompareTag("PlayerAttack"))
        {
            float damage = MobEntity.pm.damage * weaponBody.DMG();
            Debug.Log("공격 성공!");
            MobEntity.mm.curHealth -= damage; ; // 단순히 공격력에서 차감하는 것이 아닌, 계수를 고려해주어야 한다.
            gauge.ChangeBar(60 * MobEntity.mm.curHealth / MobEntity.mm.maxHealth);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        GameObject collided = collision.gameObject;
        IWeaponBody weaponBody = collided.GetComponent<IWeaponBody>();
        if (collided.CompareTag("PerAttack"))
        {
            IPerWeapon pw = collided.GetComponent<IPerWeapon>();
            timer += Time.timeScale;
            if (timer >= pw.DMGCycle())
            {
                float damage = weaponBody.DMG();
                Debug.Log("공격 성공!");
                MobEntity.mm.curHealth -= damage; ; // 단순히 공격력에서 차감하는 것이 아닌, 계수를 고려해주어야 한다.
                gauge.ChangeBar(60 * MobEntity.mm.curHealth / MobEntity.mm.maxHealth);
                timer = 0;
            }

        }
    }
}
