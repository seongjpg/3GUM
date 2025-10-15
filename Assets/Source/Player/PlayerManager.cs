using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class PlayerManager : MonoBehaviour
{
    //플레이어 라이프 사이클 관리(탄생, 사망), 플레이어 스테이터스
    [SerializeField] public float maxHealth; // 최대 체력
    [SerializeField] public float curHealth; // 현재 체력
    [SerializeField] public float maxSpeed;
    [SerializeField] public float speed; // 이동 속도
    [SerializeField] public float hitRate; // 피격 확률(0-1범위)
    [SerializeField] public float damage; // 공격력
    [SerializeField] public float fireRate; // 연사 속도


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
            Destroy(gameObject);
        }

        //패시브의 동작
        if (applyPassives != null)
        {
			foreach (IApplyEffect pass in applyPassives)
			{
				pass.ApplyEffect(gameObject);
			}
		}
        
    }

	
}
