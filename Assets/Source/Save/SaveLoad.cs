using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    [SerializeField] SerializeSceneName SSN;

    void Start()
    {
        SSN.LoadSceneName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
