using UnityEngine;

public class SceneGate : MonoBehaviour {
	[Tooltip("이 게이트가 이동시킬 씬 이름")]
	public string targetSceneName;

	[Tooltip("플레이어 태그")]
	public string playerTag = "Player";

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag(playerTag))
		{
			SceneMoveManager.Instance.MoveToScene(targetSceneName);
		}
	}
}