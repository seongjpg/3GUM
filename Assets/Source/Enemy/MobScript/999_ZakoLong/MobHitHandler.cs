using UnityEngine;

public class MobHitHandler : MonoBehaviour
{
    private MobManager mm;
    private VisualEffectHandler vfx;
    private AnimationHandler animHandler;
    [SerializeField] GameObject hpBar;
    private HealthGauge gauge;
    private float perTimer;

    void Awake()
    {
        mm = GetComponentInParent<MobManager>();
        vfx = GetComponentInParent<VisualEffectHandler>();
        animHandler = GetComponentInParent<AnimationHandler>();
        gauge = hpBar.GetComponent<HealthGauge>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collided = other.gameObject;

        if (!collided.CompareTag("PlayerAttack")) return;

        IWeaponBody weaponBody = collided.GetComponent<IWeaponBody>();
        if (weaponBody == null) return;

        TakeDamage(weaponBody.DMG());
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;

        if (!collided.CompareTag("PerAttack")) return;

        IWeaponBody weaponBody = collided.GetComponent<IWeaponBody>();
        IPerWeapon pw = collided.GetComponent<IPerWeapon>();
        if (weaponBody == null || pw == null) return;

        perTimer += Time.deltaTime;
        if (perTimer >= pw.DMGCycle())
        {
            TakeDamage(weaponBody.DMG());
            perTimer = 0;
        }
    }

    private void TakeDamage(float damage)
    {
        mm.curHealth -= damage;
        gauge.ChangeBar(60 * mm.curHealth / mm.maxHealth);

        vfx.Blink(0.2f);
        //animHandler.PlayTrigger("Hit");

        Debug.Log($"피격! 남은 체력: {mm.curHealth}");
    }
}