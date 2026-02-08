using UnityEngine;

public interface IDamageDealer {
    [SerializeField] int damage { get; }
}

public class PlayerMovement : MonoBehaviour
{
    //플레이어의 이동 방식 / 플레이어 체력 가감 

    public LayerMask wallLayer;      // 벽 레이어

    private GameObject player;
    Rigidbody2D rb;
    [SerializeField] GameObject UI;
    private MainUI mainUI;
    public PlayerManager pm;
	public Transform playerTr;
	public GameObject playerWeapon;
    public ISkill skill01;
    public ISkill skill02;
    public float fireTimer;
    public float skill01Timer;
    public float skill02Timer;

    void Start()
    {
        //플레이어 게임 오브젝트 내에 존재하는 PlayerManager를 뽑아온다.
        player = GameObject.FindWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        pm = player.GetComponent<PlayerManager>();
		playerTr = player.GetComponent<Transform>();
        mainUI = UI.GetComponent<MainUI>();
    }

    // Update is called once per frame
    void Update()
    {
        fireTimer += Time.deltaTime;
        skill01Timer += Time.deltaTime;
        skill02Timer += Time.deltaTime;
        if (fireTimer >= pm.fireRate)
        {
            PlayerAttack();
        }
        PlayerMove();
        
        if(skill01Timer >= pm.CoolTime_01)
        {
            Skill_01();
        }
        if(skill02Timer >= pm.CoolTime_02)
        {

            Skill_02();
        }
        OTS();
        DebugingPassive();
    }

    /*
	private void PlayerMove() // 방향키를 이용한 조작은 모바일로 이식하는 과정에서 대체된다.
	{
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Vector2 dir = input.normalized;
        RaycastHit2D hit = Physics2D.CapsuleCast(rb.position, gameObject.transform.localScale, CapsuleDirection2D.Vertical, 0f, dir, pm.speed, wallLayer);

        if (hit.collider == null)
        {
            rb.MovePosition(rb.position + dir * pm.speed);
        }
        else
        {
        }

	}
    */

    private void PlayerMove() {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 dir = input.normalized;

        Vector2 moveX = new Vector2(dir.x, 0);
        Vector2 moveY = new Vector2(0, dir.y);

        // X축 검사 -> raycast를 이용한 x축 이동여부 검사. 만일 이를 y축과 동시에 진행할 경우 x축과 y축 둘 중 하나라도 벽에 닿아버리면 반대방향 이외에는 이동을 할 수 없게 되어버린다.
        if (moveX != Vector2.zero) {
            RaycastHit2D hitX = Physics2D.CapsuleCast(
                rb.position, transform.localScale, CapsuleDirection2D.Vertical,
                0f, moveX, pm.speed, wallLayer
            );

            if (hitX.collider != null)
                moveX = Vector2.zero; // X방향 벽 → X이동 차단
        }

        // Y축 검사 -> x축과 마찬가지. 둘을 분리하여야 한다. 
        if (moveY != Vector2.zero) {
            RaycastHit2D hitY = Physics2D.CapsuleCast(
                rb.position, transform.localScale, CapsuleDirection2D.Vertical,
                0f, moveY, pm.speed, wallLayer
            );

            if (hitY.collider != null)
                moveY = Vector2.zero; // Y방향 벽 → Y이동 차단
        }

        Vector2 finalMove = moveX + moveY;
        rb.MovePosition(rb.position + finalMove * pm.speed);
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
        if (skill01 == null)
        {
            return;
        }
        if (Input.GetKeyDown("e")){
			Debug.Log("스킬1");
            skill01.SkillEffect(player);
            //weapon.SkillATK();
            skill01Timer = 0;
		}
    }
    
    public void NewSkill_01()
    {
        Debug.Log("스킬1-구조개선");
        //weapon.SkillATK();
        skill01Timer = 0;
    }

    //스킬 2번
    public void Skill_02()
    {
        if (skill02 == null)
        {
            return;
        }
        if (Input.GetKeyDown("3"))
        {
            Debug.Log("스킬2");
            skill02.SkillEffect(player);
            skill02Timer = 0;
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

    public void EarnHeal()
    {
        mainUI.HealthChange(pm.curHealth);
    }
    
}
