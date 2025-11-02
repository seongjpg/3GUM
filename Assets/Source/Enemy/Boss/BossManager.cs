using UnityEngine;

public class BossManager : MonoBehaviour
{
	//적 라이프 사이클 관리(탄생, 사망), 플레이어 스테이터스
	[SerializeField] public float curHealth; // 현재 체력
	[SerializeField] public float speed; // 이동 속도
	[SerializeField] public float damage; // 공격력

}
