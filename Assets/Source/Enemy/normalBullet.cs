using UnityEngine;

public class NormalBullet : MonoBehaviour {
	public float speed = 5f;                  // 탄환 속도
	public float damage;
	public float lifeTime;
	public float timer;
	private Transform target;                  // 추적 대상
	public GameObject p;

	private Vector3 dir;
	private Vector3 normalizedDir;

	// 외부에서 추적 대상을 지정
	public void Seek()
	{
		PlayerManager pm = p.GetComponent<PlayerManager>();
		target = pm.transform;
	}

	public void Start()
	{
		Seek();

		//목표 방향 계산. 한 번 정해진 방향은 변경되지 않는다.
		dir = target.position - transform.position;

		//목표 방향을 기반으로 normalize실시
		normalizedDir = dir.normalized;
	}

	void Update()
	{
		timer += Time.deltaTime;
		if (lifeTime <= timer)
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

		//속도 적용 + DeltaTime
		transform.position += normalizedDir * speed * Time.deltaTime;
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform == target)
		{
			// 충돌 처리 (이펙트, 데미지 등)
			Debug.Log("적의 normal bullet 명중!");
			Destroy(gameObject);
		}
	}
}