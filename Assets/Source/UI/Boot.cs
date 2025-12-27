using UnityEngine;

public class Boot : MonoBehaviour
{
    [SerializeField] SceneMoveManager SMM;
    [SerializeField] string sceneName;
    public float waitTime = 1f;
    public float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            SMM.MoveToScene(sceneName);
            Destroy(gameObject);
        }
    }
}
