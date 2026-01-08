using UnityEngine;

public class TheHands_Effect : MonoBehaviour, IWeaponBody, IPerWeapon
{
    private float timer = 0f;

    GameObject player;

    public WeaponStatus activeStat;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        this.transform.position = player.transform.position;
        transform.SetParent(player.transform);
        Debug.Log("효과 발동. 지속시간 동안 사용할 능력을 채워야 함. 그런데 플레이어의 deltatime을 전부 unscaledDeltaTime으로 변경해주어야 한다. 이것은 매우매우 귀찮은.");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= activeStat.lifeTime)
        {
            Destroy(gameObject);
            return;
        }
    }

    public float DMG()
    {
        return activeStat.damage;
    }
    public float DMGCycle()
    {
        return activeStat.shotSpeed;
    }

    public float CoolTime()
    {
        if (activeStat != null)
        {

            return activeStat.atkSpeed;
        }
        else
        {
            return 1000;
        }
    }
}


