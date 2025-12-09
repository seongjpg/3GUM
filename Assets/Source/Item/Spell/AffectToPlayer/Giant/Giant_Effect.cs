using UnityEngine;

public class Giant_Effect : MonoBehaviour
{
    private float timer = 0f;

    GameObject player;
    PlayerManager pm;

    public WeaponStatus spellStat;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pm = player.GetComponent<PlayerManager>();
        ActiveEffect();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spellStat.lifeTime)
        {
            Destroy(gameObject);
            UnActiveEffect();
            return;
        }
        this.transform.position = player.transform.position;
    }

    public float CoolTime()
    {
        if (spellStat != null && spellStat.isBullet)
        {

            return spellStat.atkSpeed;
        }
        else
        {
            Debug.Log("이건 근접무기 아니다.");
            return 1000;
        }
    }

    private void ActiveEffect()
    {
        player.transform.localScale *= 2;
        pm.damage += spellStat.damage;
        pm.speed -= spellStat.shotSpeed;
    }

    private void UnActiveEffect()
    {
        player.transform.localScale /= 2;
        pm.damage -= spellStat.damage;
        pm.speed += spellStat.shotSpeed;
    }
}


