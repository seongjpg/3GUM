using UnityEngine;

public class GlobalSceneControl : MonoBehaviour
{
    [SerializeField] SceneMoveManager SMM;
    [SerializeField] SerializeSceneName SSN;
    [SerializeField] GlobalAmt GA;
    void Start()
    {
        Debug.Log("json 직렬화 파일 해제");
        SSN.LoadSceneName();
        //SMM.MoveToScene(GA.GetSceneName01());
    }

    public void SaveScene(string sceneName)
    {
        GA.SetSceneName01(sceneName);
        SSN.SaveSceneName();
    }

    public void LoadScene()
    {
        SSN.LoadSceneName();
        SMM.MoveToScene(GA.GetSceneName01());
    }
}
