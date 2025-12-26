using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    [SerializeField] SerializeSceneName SSN;
    [SerializeField] SerializePlayer SP;

    void Start()
    {
        SSN.LoadSceneName();
    }

}
