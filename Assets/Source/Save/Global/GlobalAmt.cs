using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalAmt : MonoBehaviour
{

    public string sceneName01;
    public bool isInitFile01 = true;
    public bool goToNextScene = false;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
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

    public void SetSceneFlow01(string _sceneName01, bool _isInitFile01)
    {
        sceneName01 = _sceneName01;
        isInitFile01 = _isInitFile01;
    }

}
