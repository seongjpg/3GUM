using UnityEngine;
using UnityEngine.UIElements;

public class MYUI : MonoBehaviour
{
    Label myLabel;
    Button myButton;

    private void OnEnable()
    {
        // UIDocument 컴포넌트를 가져옵니다.
        var uiDocument = GetComponent<UIDocument>();

        // 루트 요소를 가져옵니다.
        var root = uiDocument.rootVisualElement;

        myLabel = root.Q<Label>("myLabel");
        myButton = root.Q<Button>("myButton");
        myButton.clicked += MyButton_clicked;
    }

    private void MyButton_clicked()
    {
        myLabel.text = myLabel.text == "千本桜影厳" ? "헬로우, 유니버스!" : "千本桜影厳";
    }
}



/*
 기본적인 ui document 사용 1형

using UnityEngine;
using UnityEngine.UIElements;

public class myUi : MonoBehaviour
{
    private void OnEnable()
    {
        // UIDocument 컴포넌트를 가져옵니다.
        var uiDocument = GetComponent<UIDocument>();

        // 루트 요소를 가져옵니다.
        var root = uiDocument.rootVisualElement;
        // 이후, root를 이용해 실제로 uiDocument에 존재하는 ui object들에 접근할 수 있다.
        myLabel = root.Q<Label>("myLabel");
    
    }

}
 
 
 */