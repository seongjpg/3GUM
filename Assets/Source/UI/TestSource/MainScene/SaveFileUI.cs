using UnityEngine;
using UnityEngine.UIElements;

public class SaveFileUI : MonoBehaviour
{
    [SerializeField] Scene_WTG_ASSET WTG_ASSET;
    [SerializeField] SceneMoveManager smm;

    Button Save01;
    Button Save02;
    Button Save03;
    Button Quit;

    private void OnEnable()
    {

        // UIDocument 컴포넌트를 가져옵니다.
        var uiDocument = GetComponent<UIDocument>();

        // 루트 요소를 가져옵니다.
        var root = uiDocument.rootVisualElement;
        // 이후, root를 이용해 실제로 uiDocument에 존재하는 ui object들에 접근할 수 있다.
        Save01 = root.Q<Button>("BTN_SAVE1");
        Save02 = root.Q<Button>("BTN_SAVE2");
        Save03 = root.Q<Button>("BTN_SAVE3");
        Quit = root.Q<Button>("BTN_QUIT_TITLE");

        Save01.clicked += Save01Clicked;
        Save02.clicked += Save02Clicked;
        Save03.clicked += Save03Clicked;
        Quit.clicked += QuitClicked;
    }

    private void OnDisable()
    {
        Save01.clicked -= Save01Clicked;
        Save02.clicked -= Save02Clicked;
        Save03.clicked -= Save03Clicked;
        Quit.clicked -= QuitClicked;
    }

    //결국 각 세이브버튼은 동일 동작을 하지만, json 파일을 어떤 걸 받아오냐의 차이일 뿐이다. 
    //그래서 차후 json을 제대로 구축하고 나면 targetScene을 동적으로 정할 수 있도록 json에 반영해야 한다.
    void Save01Clicked()
    {
        smm.MoveToScene(WTG_ASSET.fts.targetScene);
        //this.gameObject.SetActive(false);
    }
    void Save02Clicked() 
    {
        smm.MoveToScene(WTG_ASSET.fts.targetScene);
        //this.gameObject.SetActive(false);
        //GameObject.Destroy(gameObject);
    }
    void Save03Clicked()
    {
        smm.MoveToScene(WTG_ASSET.fts.targetScene);
        //this.gameObject.SetActive(false);
    }
    void QuitClicked()
    {
        this.gameObject.SetActive(false);
    }
}