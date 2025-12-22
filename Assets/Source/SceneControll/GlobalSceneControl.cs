using UnityEngine;

public class GlobalSceneControl : MonoBehaviour
{
    [SerializeField] SceneMoveManager SMM;
    [SerializeField] SerializeSceneName SSN;
    [SerializeField] GlobalAmt GA;
    void Start()
    {
    }

    /*
    private void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            Debug.Log("json 직렬화");
            GA.SetCurSceneName();
            SSN.SaveSceneName();
        }
        if (Input.GetKeyDown("o"))
        {
            Debug.Log("json 직렬화 파일 해제");
            SSN.LoadSceneName();
            SMM.MoveToScene(GA.GetSceneName01());
        }
    }
    */

    public void SaveScene()
    {
        GA.SetCurSceneName();
        SSN.SaveSceneName();
    }

    public void LoadScene()
    {
        SSN.LoadSceneName();
        SMM.MoveToScene(GA.GetSceneName01());
    }
}
