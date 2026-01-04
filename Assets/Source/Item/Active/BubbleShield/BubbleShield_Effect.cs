using UnityEngine;

public class BubbleShield_Effect : MonoBehaviour, IWeaponBody, IPerWeapon
{
    private float timer = 0f;
    [SerializeField] public float knockbackForce = 10f;

    GameObject player;

    public WeaponStatus activeStat;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        // 보호막을 플레이어의 자식으로 둔다
        transform.SetParent(player.transform);

        // 플레이어와 보호막 충돌 무시
        Physics.IgnoreLayerCollision(
            LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("Effect"),
            true
            );

    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= activeStat.lifeTime)
        {
            Destroy(gameObject);
            return;
        }
    }

    public float DMG()
    {
        return activeStat.damage;
    }
    public float DMGCycle()
    {
        return activeStat.shotSpeed;
    }

    public float CoolTime()
    {
        if (activeStat != null && activeStat.isBullet)
        {

            return activeStat.atkSpeed;
        }
        else
        {
            Debug.Log("이건 근접무기 아니다.");
            return 1000;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Rigidbody rb = col.rigidbody;
            if (rb != null)
            {
                Vector3 dir = (col.transform.position - transform.position).normalized;
                rb.AddForce(dir * knockbackForce, ForceMode.Impulse);
            }
        }
    }
}


