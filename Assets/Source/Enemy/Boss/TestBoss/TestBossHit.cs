using UnityEngine;

public class TestBossHit : MonoBehaviour
{
    private TestBoss BossEntity;

    [SerializeField] GameObject hpBar;
    private HealthGauge gauge;
    private float timer;

    void Awake()
    {
        BossEntity = GetComponentInParent<TestBoss>();
        if (BossEntity == null)
            Debug.LogError("EntityParent not found in parents of " + gameObject.name);

        gauge = hpBar.GetComponent<HealthGauge>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collided = other.gameObject;
        IWeaponBody weaponBody = collided.GetComponent<IWeaponBody>();
        if (collided.CompareTag("PlayerAttack"))
        {
            float damage = BossEntity.pm.damage * weaponBody.DMG();
            Debug.Log("공격 성공!");
            BossEntity.bm.curHealth -= damage; ; // 단순히 공격력에서 차감하는 것이 아닌, 계수를 고려해주어야 한다.
            gauge.ChangeBar(60 * BossEntity.bm.curHealth / BossEntity.bm.maxHealth);
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
                BossEntity.bm.curHealth -= damage; ; // 단순히 공격력에서 차감하는 것이 아닌, 계수를 고려해주어야 한다.
                gauge.ChangeBar(60 * BossEntity.bm.curHealth / BossEntity.bm.maxHealth);
                timer = 0;
            }

        }
    }
}
