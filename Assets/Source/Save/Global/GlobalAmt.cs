using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalAmt : MonoBehaviour
{

    public string sceneName01;

    void Start()
    {
        SetSceneName01(SceneManager.GetActiveScene().name);
    }

    void Update()
    {
        
    }

    public void SetCurSceneName()
    {
        SetSceneName01(SceneManager.GetActiveScene().name);
    }

    public void SetSceneName01(string _sceneName)
    {

        sceneName01 = _sceneName;
    }

    public string GetSceneName01()
    {
        return sceneName01;
    }
}
