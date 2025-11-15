using UnityEngine;
using UnityEngine.UIElements;

public class SettingUI: MonoBehaviour
{

    Button Save;
    Button Quit;
    private void OnEnable()
    {
        // UIDocument 컴포넌트를 가져옵니다.
        var uiDocument = GetComponent<UIDocument>();

        // 루트 요소를 가져옵니다.
        var root = uiDocument.rootVisualElement;

        Save = root.Q<Button>("BTN_SAVE");
        Quit = root.Q<Button>("BTN_QUIT");


        Save.clicked += SaveClicked;
        Quit.clicked += QuitClicked;
    }

    void SaveClicked()
    {
        Debug.Log("save clicked.");
        //저장
        //
        //나가기
        this.gameObject.SetActive(false);
    }

    void QuitClicked()
    {
        Debug.Log("Quit Clicked");
        //그냥 나가기
        this.gameObject.SetActive(false);

    }

}