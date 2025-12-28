using UnityEngine;
using UnityEngine.SceneManagement;

//실질적으로 씬 이동에 관여하는 스크립트. WTG_ASSET에서 적은(굳이 필요한가 모르겠긴 함) 현재 씬 이름과 이동할 씬 이름을 이용해서 작동.
public class SceneMoveManager : MonoBehaviour {

	void Awake()
	{
	
		//DontDestroyOnLoad(gameObject); // 씬 전환 시에도 유지
	}

	public void MoveToScene(string sceneName)
	{
		if (string.IsNullOrEmpty(sceneName))
		{
			Debug.LogWarning("이동할 씬 이름이 비어 있습니다.");
			return;
		}
		SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
		Time.timeScale = 1.0f;
	}
}