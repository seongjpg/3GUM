using UnityEngine;

public class TestBoss : MonoBehaviour
{
    [SerializeField] float range;
	[SerializeField] float fireRate;
	[SerializeField] float fireTimer;
	[SerializeField] private GameObject bulletPrefab;
	
	private Transform target;                  // 추적 대상(플레이어)
	public GameObject p;
	public GameObject boss;
	public BossManager bm;
	public  PlayerManager pm;
	public void Start()
    {
        bm = gameObject.GetComponent<BossManager>();
        p = GameObject.FindWithTag("Player");
        pm = p.GetComponent<PlayerManager>();
        target = pm.transform;
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
			transform.position += normalizedDir * bm.speed * Time.deltaTime;
		}

		fireTimer += Time.deltaTime;
		if (fireTimer >= fireRate)
		{
			fireTimer = 0;
			Shot();
		}

	}


    // 외부에서 추적 대상을 지정
    public void Seek()
    {
    }


    private void Shot()
	{
		Instantiate(bulletPrefab, gameObject.transform.position, gameObject.transform.rotation);
	}
}
