using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
	[SerializeField] GameObject InterfaceUI;
	[SerializeField] GameObject PauseUI;
	[SerializeField] GameObject SettingUI;
	private Stack<GameObject> UIStack = new Stack<GameObject>();
	private Stack<CanvasGroup> canvas = new Stack<CanvasGroup>();
	public bool isPaused = false;

	void Start()
	{
		PauseUI.SetActive(false);
		SettingUI.SetActive(false);
		InterfaceUI.SetActive(true);
		UIStack.Push(InterfaceUI);
		CanvasGroup cg = InterfaceUI.GetComponent<CanvasGroup>();
		canvas.Push(cg);
		ApplyBRC();
	}

	public void RemoveBRC()
	{
		CanvasGroup cg = canvas.Pop();
		cg.blocksRaycasts = false;
		cg.interactable = false;
		Debug.Log(cg);
		canvas.Push(cg);
	}
	public void ApplyBRC()
	{
		CanvasGroup cg = canvas.Pop();
		cg.blocksRaycasts = true;
		cg.interactable = true;
		canvas.Push(cg);
		
	}

	public void RAB(CanvasGroup cg)
	{
		RemoveBRC();
		canvas.Push(cg);
		ApplyBRC();
	}

	public void RA_POP()
	{
		RemoveBRC();
		ApplyBRC();
	}
	public void OnClickPause()
	{
		Time.timeScale = 0; // update로 갱신되는 함수는 timescale로 시간을 멈출 수 없음.
		PauseUI.SetActive(true);
		Debug.Log("pause");
		isPaused = true;
		RAB(PauseUI.GetComponent<CanvasGroup>());
	}

	public void OnClickSetting()
	{
		SettingUI.SetActive(true);
		Debug.Log("설정창으로 넘어가기");
		UIStack.Push(SettingUI);
		RAB(SettingUI.GetComponent<CanvasGroup>());
	}

	public void OnClickQTT()
	{
		//진행 상황 반영하여 세이브 파일을 갱신하고, 메인화면 ui로 넘어가기
	}

	public void OnClickBack()
	{
		GameObject nowUI = UIStack.Pop();
        if (nowUI.name == "Resume")
		{
			Time.timeScale = 1;
		}
		RA_POP();
        nowUI.SetActive(false);
	}

	public void OnClickApply()
	{
		//apply시 playerpref으로 설정 변경된 사항 적용해주기
	}
}
