using UnityEngine;

public class MirrorDotDamage : MonoBehaviour
{
    public WeaponStatus MDDStat;
    public float lifeTimer;
    public float timer;

    private GameObject parent;
    private MobManager mM;
    void Start()
    {
        parent = transform.parent.gameObject;
        mM = parent.GetComponent<MobManager>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer = Time.deltaTime;
        timer += Time.deltaTime;
        if (lifeTimer > MDDStat.lifeTime)
        {
            GameObject.Destroy(this.gameObject);
            return;
        }
        if (timer > MDDStat.atkSpeed)
        {
            mM.curHealth -= MDDStat.damage;
            timer = 0;
        }
    }
}
