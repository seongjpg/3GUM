using System.Threading;
using UnityEngine;

public class BubbleSupplier : MonoBehaviour
{
    [SerializeField] GameObject BubbleShield;
    [SerializeField] float coolTime = 60f;
    private float timer = 0;
    private void Start()
    {
        GameObject.Instantiate(BubbleShield);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= coolTime)
        {
            GameObject.Instantiate(BubbleShield);
            
            timer = 0;
        }
    }
}
