using UnityEngine;
using UnityEngine.UIElements;

public interface IDamageDealer {
    [SerializeField] int damage { get; }
}

public class PlayerMovement : MonoBehaviour
{
    //플레이어의 이동 방식 / 플레이어 체력 가감 
    private GameObject player;
    [SerializeField] GameObject UI;
    private MainUI mainUI;
    public PlayerManager pm;
	public Transform playerTr;
	public GameObject playerWeapon;
    public float fireTimer;

    void Start()
    {
        //플레이어 게임 오브젝트 내에 존재하는 PlayerManager를 뽑아온다.
        player = GameObject.Find("Player");
        pm = player.GetComponent<PlayerManager>();
		playerTr = player.GetComponent<Transform>();
        mainUI = UI.GetComponent<MainUI>();
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= pm.fireRate)
        {
            PlayerAttack();
        }
        PlayerMove();
        
        Skill_01();
        Skill_02();
        OTS();
        DebugingPassive();
    }

	private void PlayerMove() // 방향키를 이용한 조작은 모바일로 이식하는 과정에서 대체된다.
	{
		float speed = pm.speed;
        if (Input.GetKey(KeyCode.LeftShift)) speed = pm.speed * 2;
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(x * speed, y * speed, 0);

        playerTr.position += move * Time.deltaTime;

	}
    public void PlayerUp()
    {
        float speed = pm.speed;
        playerTr.position += new Vector3(0, speed, 0) * Time.deltaTime;
    }
    public void PlayerDown()
    {
        float speed = pm.speed;
        playerTr.position += new Vector3(0, -speed, 0) * Time.deltaTime;
    }
    public void PlayerLeft()
    {
        float speed = pm.speed;
        playerTr.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
    }
    public void PlayerRight()
    {
        float speed = pm.speed;
        playerTr.position += new Vector3(speed, 0, 0) * Time.deltaTime;
    }

    //기본 공격
    void PlayerAttack() 
	{
        if (Input.GetMouseButton(1))
        {
            Debug.Log("좌클릭");
            Instantiate(playerWeapon, playerTr.position, playerTr.rotation);
            fireTimer = 0;
        }
	}

    public void NewPlayerAttack()
    {
        Debug.Log("좌클릭-구조개선");
        Instantiate(playerWeapon, playerTr.position, playerTr.rotation);
        fireTimer = 0;
    }

    //스킬 1번
    private void Skill_01()
    {
        if (Input.GetKeyDown("1")){
			Debug.Log("스킬1");
		}
    }
    
    public void NewSkill_01()
    {
        Debug.Log("스킬1-구조개선");
    }

    //스킬 2번
    public void Skill_02()
    {
        if (Input.GetKeyDown("2"))
        {
			Debug.Log("스킬2");
		}
    }

    public void NewSkill_02()
    {
        Debug.Log("스킬2-구조개선");
    }

    //1회성 아이템
    private void OTS()
    {
        if (Input.GetKeyDown("q"))
        {
            if (pm.oneTime != null)
            {
                Debug.Log($"1회용 아이템 사용 - {pm.oneTime.name}");
                GameObject item = pm.oneTime.gameObject;
                pm.oneTimeSpell.UseSpell(player, item);
                Destroy(pm.oneTime);
            }
		}
        return;
    }

    public void Spell()
    {
        if (pm.oneTime != null)
        {
            Debug.Log($"1회용 아이템 사용 - {pm.oneTime.name}");
            GameObject item = pm.oneTime.gameObject;
            pm.oneTimeSpell.UseSpell(player, item);
            Destroy(pm.oneTime);
        }
    }

    private void DebugingPassive()
    {
        if (Input.GetKeyDown("x"))
        {
            Debug.Log("패시브 아이템 제거");
            GameObject pssItem = pm.passive[pm.passive.Count-1];
            IApplyEffect pss = pm.applyPassives[pm.applyPassives.Count-1];
            pss.RemoveFromPM(gameObject, pssItem, pss);
        }
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		GameObject collided = collision.gameObject;
		//적과 충돌시
		if (collided.CompareTag("Enemy"))
		{
            Debug.Log("적과 접촉!");
            MobManager mm = collided.GetComponent<MobManager>();
			// 피해 처리 (이펙트, 데미지 등)
			if (Random.value > pm.hitRate)
			{
				Debug.Log("피해 방어!");
			}
			// 피해 처리 (이펙트, 데미지 등)
			else
			{
				Debug.Log($"{mm.damage}만큼의 피해!");
				pm.curHealth -= mm.damage;
                if (pm.curHealth < 0) pm.curHealth = 0;
                //ui에서의 작용도 해줘야 한다.
                mainUI.HealthChange(pm.curHealth);
			}
		}

		//아이템 드랍(여기서 안할듯?)
		if (collided.CompareTag("Active") || collided.CompareTag("Passive") ||
			collided.CompareTag("Weapon") || collided.CompareTag("Shield") ||
			collided.CompareTag("OneTime"))
		{	
			string name = collision.gameObject.name;
            pm.equipdItemName = name;
			pm.equipdItemType = collided.tag;
			Debug.Log($"아이템 탐지 : {name}");
		}

		//다음 스테이지 이동 차원문
		if (collided.CompareTag("Gate"))
		{
            Debug.Log("차원문 이동..");
		}
	}

    public void EarnHeal()
    {
        mainUI.HealthChange(pm.curHealth);
    }
}
