using UnityEngine;

public class MobManager : MonoBehaviour
{
    //적 라이프 사이클 관리(탄생, 사망), 플레이어 스테이터스
    [SerializeField] public float maxHealth; // 최대 체력
	[SerializeField] public float curHealth; // 현재 체력
	[SerializeField] public float speed; // 이동 속도
	[SerializeField] public int damage; // 공격력

	void Start()
    {
        
    }

    void Update()
    {
        if (curHealth <= 0)
        {
            Debug.Log($"{gameObject.name} 사망!");
            Destroy(gameObject);
        }
    }
}
