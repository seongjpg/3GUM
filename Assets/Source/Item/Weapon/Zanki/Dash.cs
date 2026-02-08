using UnityEngine;

public class Dash : MonoBehaviour, IWeaponBody
{
    private float timer = 0f;
    public WeaponStatus weapon;
    private GameObject player;
    private float angle;
    private Vector3 wayToGo;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        PlayerManager pm = player.GetComponent<PlayerManager>();
        pm.BeInvicible(weapon.lifeTime+1);
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        wayToGo = mouseWorldPos - player.transform.position;
        wayToGo.Normalize();

        angle = Mathf.Atan2(wayToGo.y, wayToGo.x) * Mathf.Rad2Deg - 90;
        
    }


    void Update()
    {

        timer += Time.deltaTime;
        if (timer >= weapon.lifeTime)
        {
            Destroy(gameObject);
            return;
        }
        player.transform.position += Time.deltaTime * weapon.shotSpeed * wayToGo;
        this.transform.position = player.transform.position;
        //Debug.Log(this.transform.position);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collider = collision.gameObject;

        //적 피격(일단 이건 범위공격이라 굳이 처리할 건 없다.)
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("적 피격!");
        }
    }
    public float DMG()
    {
        if (weapon != null)
        {
            Debug.Log($"검기 피해량 : {weapon.damage}");
            return weapon.damage;
        }

        else
        {
            Debug.Log("이건 근접무기 아니다.");
            return 0;
        }
    }

    public float CoolTime()
    {
        if (weapon != null && weapon.isBullet)
        {

            return weapon.atkSpeed;
        }
        else
        {
            Debug.Log("이건 근접무기 아니다.");
            return 1000;
        }
    }
}
