using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMoveManager : MonoBehaviour {
	public static SceneMoveManager Instance { get; private set; }

	void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject); // 씬 전환 시에도 유지
	}

	public void MoveToScene(string sceneName)
	{
		if (string.IsNullOrEmpty(sceneName))
		{
			Debug.LogWarning("이동할 씬 이름이 비어 있습니다.");
			return;
		}

		SceneManager.LoadScene(sceneName);
	}
}