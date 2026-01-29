using UnityEngine;

public class EyeEffect : MonoBehaviour, IWeaponBody
{
    private float timer = 0f;
    public WeaponStatus weapon;
    private GameObject player;
    private Vector3 eyeVec;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        GameObject bossGate = GameObject.FindWithTag("Gate");
        eyeVec = Vector3.Normalize(bossGate.transform.position - player.transform.position);

    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= weapon.lifeTime)
        {
            Destroy(gameObject);
            return;
        }
        gameObject.transform.position += Time.deltaTime * weapon.shotSpeed * eyeVec;
    }

    public float DMG()
    {
        return 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.CompareTag("Gate"))
        {
            
            GameObject.Destroy(gameObject);
            return;
        }
    }

    public float CoolTime()
    {
        if (weapon != null)
        {

            return weapon.atkSpeed;
        }
        else
        {
            Debug.Log("이건 없는거다.");
            return 1000;
        }
    }

}
