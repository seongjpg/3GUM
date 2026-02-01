using Unity.VisualScripting;
using UnityEngine;

public class TimeLeft_Effect : MonoBehaviour
{
    PlayerManager pm;

    void Start()
    {
        pm = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
    }


    void Update()
    {
        if (pm.curHealth <= 0)
        {
            Debug.Log("플레이어 체력 0으로 변환, 마지막 목숨 부여!");
            pm.curHealth = 1;
            Destroy(this.gameObject);
            return;
            
        }
    }
}
