using UnityEngine;

public class PlayerBesideAttack : MonoBehaviour
{
	private float timer = 0f;
	private WeaponStatus weapon;
	private GameObject player;
	private float startAngle = 60;

	void Start()
	{
		weapon = GetComponent<WeaponStatus>();
		player = GameObject.FindWithTag("Player");
		transform.Rotate(0, 0, startAngle);
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
			Debug.Log("적 피격!");
		}
	}
}
