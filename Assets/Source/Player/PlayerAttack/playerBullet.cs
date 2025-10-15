using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem; // 모바일 이식 시 InputSystem 활용해야 함

public class PlayerBullet : MonoBehaviour
{
	private float timer;
	private Vector3 wayToGO;                  // 발사 방향
	private WeaponStatus weapon;
	public bool destroyByCollision = true;

	void Start()
    {
		weapon = GetComponent<WeaponStatus>();
		Vector3 mouseScreenPos = Input.mousePosition;
		Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
		//getkey를 이용한 방향 조정은 차후 모바일로 이식할 때 수정되어야 한다.
        if (Input.GetMouseButton(0))
		{
			wayToGO = Vector3.Normalize(mouseWorldPos-gameObject.transform.position);	
		}
	}

    void Update()
    {
		timer += Time.deltaTime;
		if (timer >= weapon.lifeTime)
		{
			Destroy(gameObject);
			return;
		}
		gameObject.transform.position += wayToGO * weapon.speed * Time.deltaTime;


    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//적 피격
		if (collision.CompareTag("Enemy"))
		{
			Debug.Log("적 피격!");
			if (destroyByCollision)
			{
				Destroy(gameObject);
			}
			
			return;
		}
		if (collision.CompareTag("Wall"))
		{
			Debug.Log("벽 충돌!");
			if (destroyByCollision)
			{
				Destroy(gameObject);
			}
		}
	}
}
