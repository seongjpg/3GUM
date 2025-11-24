using UnityEngine;
using UnityEngine.UIElements;

public class MainSceneUI : MonoBehaviour
{
    [SerializeField] GameObject SaveFileUI; 
    [SerializeField] GameObject SettingUI;

    Button Start;
    Button Setting;
    Button Quit;

    private void OnEnable()
    {
        SaveFileUI.SetActive(false);
        SettingUI.SetActive(false);
        // UIDocument 컴포넌트를 가져옵니다.
        var uiDocument = GetComponent<UIDocument>();

        // 루트 요소를 가져옵니다.
        var root = uiDocument.rootVisualElement;

        Start = root.Q<Button>("BTN_START");
        Setting = root.Q<Button>("BTN_SETTING");
        Quit = root.Q<Button>("BTN_QUIT_GAME");

        Start.clicked += StartClicked;
        Setting.clicked += SettingClicked;
        Quit.clicked += QuitClicked;
    }

    private void OnDisable()
    {
        SaveFileUI.SetActive(false);
        SettingUI.SetActive(false);
        Setting.clicked -= SettingClicked;
        Quit.clicked -= QuitClicked;
        Start.clicked -= StartClicked;
    }

    void StartClicked()
    {
        SaveFileUI.SetActive(true);
    }

    void SettingClicked()
    {
        SettingUI.SetActive(true);
    }

    void QuitClicked()
    {
        //게임 애플리케이션 종료
        Application.Quit();
    }
}
