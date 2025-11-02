using UnityEngine;

public class SceneGate : MonoBehaviour {

	//WTG_ASSET에서 현재 씬 이름과 이동할 씬 이름을 받아와서 SceneMoveManager를 사용해서 관리한다.
	private string playerTag = "Player";
	public SceneMoveManager moveManager;
	[SerializeField] Scene_WTG_ASSET WTG_ASSET;
	

	private void OnTriggerEnter2D(Collider2D other)
	{
        Debug.Log("감지!!!!!!!!!");

        if (other.CompareTag(playerTag))
		{
			Debug.Log("이동!!!!!!!");
			moveManager.MoveToScene(WTG_ASSET.fts.targetScene);
			GameObject.Destroy(gameObject);
		}
	}
}