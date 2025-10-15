using UnityEngine;
using System.Collections.Generic;

public class IntroButtonControl : MonoBehaviour
{

    [SerializeField] GameObject IntroUI;
    [SerializeField] GameObject SettingUI;
    [SerializeField] GameObject SaveFileUI;
    private Stack<GameObject> UIStack = new Stack<GameObject>();

    void Start()
    {
        
        IntroUI.SetActive(true);
		SettingUI.SetActive(false);
        SaveFileUI.SetActive(false);
		UIStack.Push(IntroUI);
	}

    public void OnClickStart()
    {
        Debug.Log("세이브 파일 리스트 넘어가기");
        SaveFileUI.SetActive(true);
        UIStack.Push(SaveFileUI);
    }

    public void OnClickSetting()
    {
		SettingUI.SetActive(true);
		Debug.Log("설정창으로 넘어가기");
        UIStack.Push(SettingUI);
    }

    public void OnClickExit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void OnClickBack()
    {
        GameObject nowUI = UIStack.Pop();
        nowUI.SetActive(false);
    }

    public void OnClickApply()
    {
        //apply시 playerpref으로 설정 변경된 사항 적용해주기
    }
}
