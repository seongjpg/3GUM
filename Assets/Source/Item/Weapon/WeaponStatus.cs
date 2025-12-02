using UnityEngine;

[CreateAssetMenu(menuName ="Weapon/Status")]
public class WeaponStatus : ScriptableObject
{
	//탄환의 정보만을 담아서 관리.
	[SerializeField] public float speed = 5f; // 공격속도?
	[SerializeField] public float damage; // 공격력 계수와 관련.
	[SerializeField] public float lifeTime; // 게임 오브젝트가 존재할 수 있는 시간
	[SerializeField] public float swingSpeed; // 휘두르는 근접 무기의 경우, swingspeed에 비례하여 무기를 휘두르게 된다.
	[SerializeField] public bool isBullet; // 투사체 무기와 근접 무기는 각자 받을 수 있는 효과가 상이할 수 있다.
	[SerializeField] public bool originalyPenetrate; // 애초에 관통이 가능한 무기인지를 알아야 한다. 

	[SerializeField] public float ReadyForSkill; // 스킬 쿨타임
}
