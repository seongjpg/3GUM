using UnityEngine;

public class Zako_shortRich : MonoBehaviour
{
	private Transform target;                  // 추적 대상(플레이어)
	private GameObject p;
	public GameObject m;
	public MobManager mm;
	private PlayerManager pm;
	// 외부에서 추적 대상을 지정
	public void Seek()
	{
		mm = m.GetComponent<MobManager>();
		p = GameObject.FindWithTag("Player");
		pm = p.GetComponent<PlayerManager>();
		target = pm.transform;
	}

	public void Start()
	{
		Seek();
	}

	void Update()
	{
		if (target == null)
		{
			Debug.Log("추적 대상 소실");
			return;
		}

		// 1) 목표 방향 계산
		Vector3 direction = target.position - transform.position;

		// 2) 단위 벡터화
		Vector3 normalizedDir = direction.normalized;

		// 3) 속도 적용 + DeltaTime
		transform.position += normalizedDir * mm.speed * Time.deltaTime;
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		GameObject collided = other.gameObject;
		WeaponStatus weapon = collided.GetComponent<WeaponStatus>();
		if (collided.CompareTag("PlayerAttack"))
		{
			//PlayerManager pm = collided.GetComponent<PlayerManager>();
			mm.curHealth -= pm.damage * weapon.damage; // 단순히 공격력에서 차감하는 것이 아닌, 계수를 고려해주어야 한다.
		}
	}
}
