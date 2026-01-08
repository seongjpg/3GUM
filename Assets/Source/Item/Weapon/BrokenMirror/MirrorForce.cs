using UnityEngine;

public class MirrorForce : MonoBehaviour, IWeaponBody
{
    private float timer = 0f;
    public WeaponStatus mfStat;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= mfStat.lifeTime)
        {
            Destroy(gameObject);
            return;
        }
        gameObject.transform.position = new Vector3(player.transform.position.x-1f, player.transform.position.y+0.3f, player.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collider = collision.gameObject;

        if (collision.CompareTag("EnemyBullet"))
        {
            GameObject reflect = GameObject.Instantiate(collider, collider.transform.position, Quaternion.identity);
            reflect.tag = "PlayerAttack";
            Debug.Log("반사! 이걸 다듬어서 플레이어 불릿이 소환되는 것으로 해야 한다.");
            GameObject.Destroy(collision);
        }
    }
    public float DMG()
    {
        return 0;
    }

    public float CoolTime()
    {
        if (mfStat != null)
        {

            return mfStat.atkSpeed;
        }
        else
        {
            Debug.Log("이건 없다");
            return 1000;
        }
    }
}
