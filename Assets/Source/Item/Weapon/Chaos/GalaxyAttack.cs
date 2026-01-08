using UnityEngine;

public class GalaxyAttack : MonoBehaviour, IWeaponBody // 평타 눌렀을 때 완드의 움직임 및 화염구 소환 담당
{
	private float timer = 0f;
	public WeaponStatus weapon;
	[SerializeField] GameObject galaxy;
	private GameObject player;
	private float startAngle;

	void Start()
	{
		player = GameObject.FindWithTag("Player");
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mouseWorldPos - transform.position;

        float startAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg+45;

        transform.Rotate(0, 0, startAngle);
		GameObject.Instantiate(galaxy, this.transform.position, Quaternion.identity);
	}

	void Update()
	{
		timer += Time.deltaTime;
		if (timer >= weapon.lifeTime)
		{
			Destroy(gameObject);
			return;
		}
		float swingDelta = -weapon.swingSpeed * Time.deltaTime;
		transform.Rotate(0, 0, swingDelta);
		transform.position = player.transform.position;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		GameObject collider = collision.gameObject;

		//적 피격(일단 이건 범위공격이라 굳이 처리할 건 없다.)
		if (collision.CompareTag("Enemy"))
		{
			//Debug.Log("적 피격!");
		}
	}
    public float DMG()
    {
        if (weapon != null && !weapon.isBullet)
        {

            return weapon.damage;
        }

        else
        {
            Debug.Log("이건 근접무기 아니다.");
            return 0;
        }
    }

}
