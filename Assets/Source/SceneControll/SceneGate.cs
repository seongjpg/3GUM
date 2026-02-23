using UnityEngine;

public class SceneGate : MonoBehaviour {

	//WTG_ASSET에서 현재 씬 이름과 이동할 씬 이름을 받아와서 SceneMoveManager를 사용해서 관리한다.
	private string playerTag = "Player";
	public GameObject gameManager;
	public SceneMoveManager moveManager;
	[SerializeField] Scene_WTG_ASSET WTG_ASSET;
	[SerializeField] GameManager GM;
	[SerializeField] GlobalAmt GA;

    private void Start()
    {

		gameManager = GameObject.FindWithTag("GameManager");
		GM = gameManager.GetComponent<GameManager>();
		GA = GlobalAmt.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
	{
        Debug.Log("감지!!!!!!!!!");

        if (other.CompareTag(playerTag))
		{

            GA.goToNextScene = true;
            Debug.Log("이동!!!!!!!");
			//GM.LoadNextScene();
			GM.SaveForNextScene();
			moveManager.MoveToScene(WTG_ASSET.fts.targetScene);
			GameObject.Destroy(gameObject);
		}
	}
}