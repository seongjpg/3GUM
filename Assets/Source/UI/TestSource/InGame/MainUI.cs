using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{


    GameObject Player;
    PlayerManager pm;
    PlayerMovement pMov;


    [SerializeField] GameObject PauseUI;
    [SerializeField] GameObject SettingUI;

    [SerializeField] Sprite Heart_full;
    [SerializeField] Sprite Heart_3per4;
    [SerializeField] Sprite Heart_half;
    [SerializeField] Sprite Heart_quarter;
    [SerializeField] Sprite Heart_empty;

    VisualElement Heart01;
    VisualElement Heart02;
    VisualElement Heart03;
    VisualElement Heart04;
    VisualElement Heart05;

    Button Fire;
    Button Skill01;
    Button Skill02;
    Button Spell;
    Button Up;
    Button Down;
    Button Left;
    Button Right;
    Button Tab;

    bool holdFire = false;
    bool holdUp = false;
    bool holdDown = false;
    bool holdLeft = false;
    bool holdRight = false;

    float fireTimer = 0;
    float fireInterval;
    float upTimer = 0;
    float downTimer = 0;
    float leftTimer = 0;
    float rightTimer = 0;

    float holdInterval = 1f;

    private void OnEnable()
    {
        //ui의 관리는 MainUI에서 겸한다.
        PauseUI.SetActive(false);
        SettingUI.SetActive(false);
        Time.timeScale = 1f;

        Player = GameObject.FindWithTag("Player");
        pm = Player.GetComponent<PlayerManager>();
        pMov = Player.GetComponent<PlayerMovement>();
        fireInterval = pm.fireRate;

        // UIDocument 컴포넌트를 가져옵니다.
        var uiDocument = GetComponent<UIDocument>();

        // 루트 요소를 가져옵니다.
        var root = uiDocument.rootVisualElement;
        // 이후, root를 이용해 실제로 uiDocument에 존재하는 ui object들에 접근할 수 있다.

        //ui 영역에서 표시하는 항목
        Heart01 = root.Q<VisualElement>("Heart01");
        Heart02 = root.Q<VisualElement>("Heart02");
        Heart03 = root.Q<VisualElement>("Heart03");
        Heart04 = root.Q<VisualElement>("Heart04");
        Heart05 = root.Q<VisualElement>("Heart05");

        HealthChange(pm.curHealth);

        //interaction 영역
        Fire = root.Q<Button>("Fire");
        Skill01 = root.Q<Button>("Skill01");
        Skill02 = root.Q<Button>("Skill02");
        Spell = root.Q<Button>("Spell");
        Up = root.Q<Button>("Up");
        Down = root.Q<Button>("Down");
        Left = root.Q<Button>("Left");
        Right = root.Q<Button>("Right");
        Tab = root.Q<Button>("Tab");

        Skill01.clicked += Skill01Clicked;
        Skill02.clicked += Skill02Clicked;
        Spell.clicked += SpellClicked;
        Up.clicked += UpClicked;
        Down.clicked += DownClicked;
        Left.clicked += LeftClicked;
        Right.clicked += RightClicked;
        Tab.clicked += TabClicked;

        Fire.RegisterCallback<PointerDownEvent>(OnFireDown);
        Fire.RegisterCallback<PointerUpEvent>(OnFireUp);
    }

    void Update()
    {
        if (holdFire)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= holdInterval)
            {
                fireTimer = 0f;
                FireOnce();
            }
        }
    }



    private void OnDisable()
    {
        if (Player != null) HealthChange(pm.curHealth);

        Skill01.clicked -= Skill01Clicked;
        Skill02.clicked -= Skill02Clicked;
        Spell.clicked -= SpellClicked;
        Up.clicked -= UpClicked;
        Down.clicked -= DownClicked;
        Left.clicked -= LeftClicked;
        Right.clicked -= RightClicked;
        Tab.clicked -= TabClicked;
    }

    public void HealthChange(int health)
    {
        int fullAmt = health / 4;
        int notFullAmt = health % 4;
        Sprite notFullSprite;

        FillEmptyHeart();
        FillFullHeart(fullAmt); // 꽉 찬 하트를 채움, 빈 하트도 채움.
        notFullSprite = FindNotFullSprite(notFullAmt); // 체력을 4로 나눈 나머지가 무엇인지에 따라 스프라이트 결정.
        FillNotFullHeart(fullAmt, notFullSprite); // 꼬다리 하트도 채우기
    }


    private void FillEmptyHeart()
    {
        Heart01.style.backgroundImage = Heart_empty.texture;
        Heart02.style.backgroundImage = Heart_empty.texture;
        Heart03.style.backgroundImage = Heart_empty.texture;
        Heart04.style.backgroundImage = Heart_empty.texture;
        Heart05.style.backgroundImage = Heart_empty.texture;
    }
    private void FillFullHeart(int fullAmt)
    {
        switch (fullAmt)
        {
            case 1:
                Heart01.style.backgroundImage = Heart_full.texture;
                break;
            case 2:
                Heart01.style.backgroundImage = Heart_full.texture;
                Heart02.style.backgroundImage = Heart_full.texture;
                break;
            case 3:
                Heart01.style.backgroundImage = Heart_full.texture;
                Heart02.style.backgroundImage = Heart_full.texture;
                Heart03.style.backgroundImage = Heart_full.texture;
                break;
            case 4:
                Heart01.style.backgroundImage = Heart_full.texture;
                Heart02.style.backgroundImage = Heart_full.texture;
                Heart03.style.backgroundImage = Heart_full.texture;
                Heart04.style.backgroundImage = Heart_full.texture;
                break;

            case 5:
                Heart01.style.backgroundImage = Heart_full.texture;
                Heart02.style.backgroundImage = Heart_full.texture;
                Heart03.style.backgroundImage = Heart_full.texture;
                Heart04.style.backgroundImage = Heart_full.texture;
                Heart05.style.backgroundImage = Heart_full.texture;
                break;
        }
    }

    private Sprite FindNotFullSprite(int notFullAmt)
    {
        switch (notFullAmt)
        {
            case 0:
                return Heart_empty;
            case 1:
                return  Heart_quarter;
            case 2:
                return  Heart_half;
            case 3:
                return Heart_3per4;
        }
        Debug.Log("오류 발생-제대로 된 체력이 안받아와지는 모양");
        return Heart_empty;
    }

    private void FillNotFullHeart(int fullAmt, Sprite notFullSprite)
    {
        switch (fullAmt)
        {
            case 0:
                Heart01.style.backgroundImage = notFullSprite.texture;
                break;
            case 1:
                Heart02.style.backgroundImage = notFullSprite.texture;
                break;
            case 2:
                Heart03.style.backgroundImage = notFullSprite.texture;
                break;
            case 3:
                Heart04.style.backgroundImage = notFullSprite.texture;
                break;
            case 4:
                Heart05.style.backgroundImage = notFullSprite.texture;
                break;
        }
    }


    void OnFireDown(PointerDownEvent evt)
    {
        FireOnce();
        holdFire = true;
        fireTimer = 0;
    }
    void OnFireUp(EventBase evt)
    {
        holdFire = false;
    }

    private void FireOnce()
    {
        Debug.Log("Fire 입력.");
        pMov.NewPlayerAttack();
    }
    private void Skill01Clicked()
    {
        Debug.Log("SKill 01 입력.");
        pMov.NewSkill_01();
    }
    private void Skill02Clicked()
    {
        Debug.Log("Skill 02 입력.");
        pMov.NewSkill_02();
    }
    private void SpellClicked()
    {
        Debug.Log("Spell 입력.");
        pMov.Spell();
    }
    private void UpClicked()
    {
        Debug.Log("Up 입력.");
        pMov.PlayerUp();
        
    }
    private void DownClicked()
    {
        Debug.Log("Down 입력.");
        pMov.PlayerDown();
    }
    private void LeftClicked()
    {
        Debug.Log("Left 입력.");
        pMov.PlayerLeft();
    }
    private void RightClicked()
    {
        Debug.Log("Right 입력.");
        pMov.PlayerRight();
    }
    private void TabClicked()
    {
        Debug.Log(" Tab입력.");
        PauseUI.SetActive(true);
    }
}
