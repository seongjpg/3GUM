using Unity.VisualScripting;
using UnityEngine;

public class TimeLeft_Effect : MonoBehaviour
{
    PlayerManager pm;
    SpriteRenderer sr;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        pm =player.GetComponent<PlayerManager>();
        sr = player.GetComponent<SpriteRenderer>();
    }


    void Update() // 구조를 변경해주어야 한다.
    {
        if (pm.curHealth <= 0)
        {
            Debug.Log("플레이어 체력 0으로 변환, 마지막 목숨 부여!");
            PlayerVisualHandler PVH = GameObject.FindWithTag("PlayerBody").GetComponent<PlayerVisualHandler>();
            pm.curHealth = 1;
            pm.speed *= 1.2f;
            pm.damage *= 1.5f;
            pm.fireRate *= 1.3f;
            pm.hitRate *= 0.9f;
            PVH.ConvertColor(Color.black, int.MaxValue);
            Destroy(this.gameObject);
            return;
            
        }
    }
}
