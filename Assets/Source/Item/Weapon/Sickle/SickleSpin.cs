using UnityEngine;

public class SickleSpin : MonoBehaviour, IWeaponBody
{
    private float timer = 0f;
    public WeaponStatus weapon;
    private GameObject player;
    private float angle;
    private Vector3 wayToGo;
    private SpriteRenderer sr;
    private Vector3 initialScale;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        initialScale = transform.localScale;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        wayToGo = mouseWorldPos - player.transform.position;
        wayToGo.Normalize();

        angle = Mathf.Atan2(wayToGo.y, wayToGo.x) * Mathf.Rad2Deg - 90;

        transform.Rotate(0, 0, 0);

        sr = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        timer += Time.deltaTime;

        gameObject.transform.position = player.transform.position;
        float alpha = Mathf.Clamp01(1f -  timer/ weapon.lifeTime);
        if (timer >= weapon.lifeTime || alpha <= 0f)
        {
            Destroy(gameObject);
            return;
        }

        // 알파값 적용
        Color c = sr.color;
        c.a = alpha;
        sr.color = c;

        // 대미지 약화
        int currentDamage = Mathf.RoundToInt(weapon.damage * alpha);

        // 크기 증가 (선형 보간)
        float scaleFactor = Mathf.Lerp(1f, 3f, timer / weapon.lifeTime);
        transform.localScale = initialScale * scaleFactor;


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
        if (weapon != null && weapon.isBullet)
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
