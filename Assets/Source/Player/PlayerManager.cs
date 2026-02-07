using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    //플레이어 라이프 사이클 관리(탄생, 사망), 플레이어 스테이터스
    [SerializeField] public int maxHealth; // 최대 체력
    [SerializeField] public int curHealth; // 현재 체력
    [SerializeField] public float maxSpeed;
    [SerializeField] public float speed; // 이동 속도
    [SerializeField] public float hitRate; // 피격 확률(0-1범위)
    [SerializeField] public float damage; // 공격력
    [SerializeField] public float fireRate; // 연사 속도
    [SerializeField] public float CoolTime_01; // 스킬1 쿨타임
    [SerializeField] public float CoolTime_02; // 스킬2 쿨타임

    //피격 처리 관련 변수
    [SerializeField] public bool isHit = false;
    [SerializeField] private float hitTime = 1.5f;

    //무적 처리 관련 변수
    [SerializeField] private bool isInvicible = false;
    [SerializeField] private  float invicibleTime = 0f;

    private float hitTimer;
    private float ivTimer;

    public bool isUsingBullet = false;


    public GameObject oneTime;
    public IUseSpell oneTimeSpell;

    public List<GameObject> passive = new List<GameObject>();
    public List<IApplyEffect> applyPassives = new List<IApplyEffect>();

    public string equipdItemName;
    public string equipdItemType;

    void Start()
    {
        
    }

    void Update()
    {
        //사망
        if (curHealth <= 0)
        {
            Debug.Log("플레이어 사망!");
            //Destroy(gameObject); // 지금은 파괴하지만 이건 좋지 않은 구조. 플레이어가 죽었을 때에도 플레이어의 오브젝트가 남아있도록 해야 한다. 그래야 카메라나 ui 등이 안날아감.
        }

        //피격의 처리. 피격시에는 1.5초로 우선 고정시켜둠. 보고 적절하게 수정하길.
        if (isHit)
        {
            hitTimer += Time.deltaTime;
            //피격 애니메이션 부여는 여기서
            Debug.Log("아픈!");
            if (hitTimer >= hitTime)
            {
                hitTimer = 0;
                isHit = false;
            }
        }

        //패시브의 동작
        if (applyPassives != null)
        {
			foreach (IApplyEffect pass in applyPassives)
			{
				//pass.ApplyEffect(gameObject); // 이는 위험한 구조라 제거
			}
		}
        
        //무적의 적용
        if (isInvicible)
        {
            ivTimer += Time.deltaTime;
            //깜빡이는 효과 부여 가능.
            Debug.Log("앙무적");
            if (ivTimer >= invicibleTime)
            {
                isInvicible = false;
                ivTimer = 0;
                invicibleTime = 0;
            }
        }

    }
    //피격처리함수(무적 판정도 포함. 따라서 플레이어 맞는 상황에는 이 함수 하나만 호출하면 됨)
    public void BeHit()
    {
        BeInvicible(2);
        isHit = true;

    }

    //무적처리함수(피격처리함수에도 포함되어있으니 피격시에는 따로 호출하지 않아도 됨. 이는 별도의 무적처리가 필요할 때만 따로 호출하면 됨.)
	public void BeInvicible(float durable)
    {
        isInvicible = true;
        ivTimer = 0;
        invicibleTime = durable;
    }
}
