using UnityEngine;
using UnityEngine.UIElements;

public class PauseUI : MonoBehaviour
{ 
    [SerializeField] GameObject SettingUI;

    [SerializeField] Scene_WTG_ASSET WTG_ASSET;
    [SerializeField] SceneMoveManager smm;

    VisualElement Mother;
    Button Resume;
    Button Setting;
    Button Quit;

    private void OnEnable()
    {
        //인게임 시간을 멈추는 작용 필요로 함.
        Time.timeScale = 0f;
        // UIDocument 컴포넌트를 가져옵니다.
        var uiDocument = GetComponent<UIDocument>();

        // 루트 요소를 가져옵니다.
        var root = uiDocument.rootVisualElement;
        // 이후, root를 이용해 실제로 uiDocument에 존재하는 ui object들에 접근할 수 있다.
        Mother = root.Q<VisualElement>("Mother");
        Resume = root.Q<Button>("BTN_RESUME");
        Setting = root.Q<Button>("BTN_SETTING");
        Quit = root.Q<Button>("BTN_QUIT_TITLE");

        Resume.clicked += ResumeClicked;
        Setting.clicked += SettingClicked;
        Quit.clicked += QuitClicked;
    }

    void ResumeClicked()
    {
        //debug
        Debug.Log("Resume Clicked.");
        //시간 정지를 멈추고, 현재의 settingsUi를 비활성화.
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
    }
    void SettingClicked()
    {
        //debug
        Debug.Log("Setting Clicked.");
        // 환경 설정 창 활성화. 
        SettingUI.SetActive(true);

    }

    void QuitClicked()
    {
        Debug.Log("Quit Clicked");
        // 게임의 진행 상황을 Json 직렬화하여 저장.
        // 게임의 현재 씬을 종료하고, 시작 화면 씬으로 이동.   
        smm.MoveToScene(WTG_ASSET.fts.targetScene);
        this.gameObject.SetActive(false);
    }

}