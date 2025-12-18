using UnityEngine;

public class FireField_Effect : MonoBehaviour, IWeaponBody, IPerWeapon
{
    private float timer = 0f;
    private float effectTimer = 0f;

    GameObject player;

    public WeaponStatus activeStat;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= activeStat.lifeTime)
        {
            Destroy(gameObject);
            return;
        }
        this.transform.position = player.transform.position;
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
    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            effectTimer += Time.deltaTime;
            if (effectTimer >= activeStat.shotSpeed) // shotSpeed를 도트대미지를 부여하는 기준으로 적용
            {
                MobManager mm = collision.gameObject.GetComponent<MobManager>();
                mm.curHealth -= activeStat.damage;
                effectTimer = 0;
            }
        }
    }
    */
}


