using UnityEngine;

public class Stealth_Effect : MonoBehaviour
{
    private float timer = 0f;

    GameObject player;

    public WeaponStatus activeStat;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        ActiveEffect();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= activeStat.lifeTime)
        {
            Destroy(gameObject);
            UnActiveEffect();
            return;
        }
        this.transform.position = player.transform.position;
    }

    public float CoolTime()
    {
        if (activeStat != null && activeStat.isBullet)
        {

            return activeStat.atkSpeed;
        }
        else
        {
            Debug.Log("이건 근접무기 아니다.");
            return 1000;
        }
    }

    private void ActiveEffect()
    {

        player.tag = "Stealth";
    }

    private void UnActiveEffect()
    {

        player.tag = "Player";
    }
}


