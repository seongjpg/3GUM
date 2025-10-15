using UnityEngine;

public class WeaponStatus : MonoBehaviour
{
	//탄환의 정보만을 담아서 관리.
	[SerializeField] public float speed = 5f;               
	[SerializeField] public float damage;
	[SerializeField] public float lifeTime;
	[SerializeField] public float swingSpeed;
}
