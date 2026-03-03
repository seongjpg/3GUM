using UnityEngine;

public class zako_longRich : MonoBehaviour
{
    [SerializeField] float range;
	[SerializeField] float fireRate;
	[SerializeField] float fireTimer;
	[SerializeField] private GameObject bulletPrefab;
	private Transform target;                  // 추적 대상(플레이어)
	private GameObject p;
	public GameObject m;
	public MobManager_Legacy mm;
	public PlayerManager pm;
	// 외부에서 추적 대상을 지정
	public void Seek()
	{
		mm = m.GetComponent<MobManager_Legacy>();
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

		//target과 gameobject의 거리가 range 이상인 경우에만 추적
		if (range < Mathf.Sqrt(direction.x*direction.x + direction.y * direction.y))
		{	
			// 2) 단위 벡터화
			Vector3 normalizedDir = direction.normalized;
			
			// 3) 속도 적용 + DeltaTime
			transform.position += normalizedDir * mm.speed * Time.deltaTime;
		}

		fireTimer += Time.deltaTime;
		if (fireTimer >= fireRate)
		{
			fireTimer = 0;
			Shot();
		}

	}

	private void Shot()
	{
		Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
	}

}
