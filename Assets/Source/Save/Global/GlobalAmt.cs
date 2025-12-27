using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalAmt : MonoBehaviour
{

    public string sceneName01;
    public bool isInitFile01 = true;

    void Start()
    {
        SetSceneName01(SceneManager.GetActiveScene().name);
        DontDestroyOnLoad(this.gameObject);
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
