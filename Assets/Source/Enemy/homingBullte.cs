using UnityEngine;

public class HomingBullet : MonoBehaviour {
	public float speed = 5f;                  // 탄환 속도
	public float damage;
	public float lifetime;
	public float timer;
	private Transform target;                  // 추적 대상
	private GameObject p;
	private PlayerManager pm;
	// 외부에서 추적 대상을 지정
	public void Seek()
	{
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
		timer += Time.deltaTime;
		if (lifetime <= timer)
		{
			Destroy(gameObject);
			return;
		}
		if (target == null)
		{
			Debug.Log("적의 normal bullet 추적 대상 소실");
			Destroy(gameObject);               // 대상이 사라지면 자기 자신도 제거
			return;
		}

		// 1) 목표 방향 계산
		Vector3 direction = target.position - transform.position;

		// 2) 단위 벡터화
		Vector3 normalizedDir = direction.normalized;

		// 3) 속도 적용 + DeltaTime
		transform.position += normalizedDir * speed * Time.deltaTime;
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform == target)
		{
			// 충돌 처리 (이펙트, 데미지 등)
			Debug.Log("적의 homingBullet 명중!");
			Destroy(gameObject);
			return;
		}
	}
}