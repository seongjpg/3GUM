using Unity.VisualScripting;
using UnityEngine;

public class WandShield : MonoBehaviour, IWeaponBody // 스킬1을 눌렀을 때 나오는 방어구가 작동하는 스크립트
{
    private float timer = 0f;
    public WeaponStatus shieldStat;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shieldStat.lifeTime)
        {
            Destroy(gameObject);
            return;
        }
        gameObject.transform.position = player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collider = collision.gameObject;

        //적 피격(일단 이건 범위공격이라 굳이 처리할 건 없다.)
        if (collision.CompareTag("Enemy"))
        {
            GameObject.Destroy(gameObject);
            // 플레이어에게 무적 판정 부여
        }
        if (collision.CompareTag("EnemyBullet"))
        {
            GameObject.Destroy(collision);
            GameObject.Destroy(gameObject);
        }
    }
    public float DMG()
    {
        if (shieldStat != null && shieldStat.isBullet)
        {
            Debug.Log($"검기 피해량 : {shieldStat.damage}");
            return shieldStat.damage;
        }

        else
        {
            Debug.Log("이건 근접무기 아니다.");
            return 0;
        }
    }

    public float CoolTime()
    {
        if (shieldStat != null && shieldStat.isBullet)
        {

            return shieldStat.atkSpeed;
        }
        else
        {
            Debug.Log("이건 근접무기 아니다.");
            return 1000;
        }
    }

    
}
