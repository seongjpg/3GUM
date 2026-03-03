using UnityEngine;

public class MirrorDotDamage : MonoBehaviour
{
    public WeaponStatus MDDStat;
    public float lifeTimer;
    public float timer;

    private GameObject parent;
    public MobManager_Legacy mM;
    void Start()
    {
        parent = transform.parent.root.gameObject;
        mM = parent.GetComponent<MobManager_Legacy>();
        if (mM == null)
        {
            Debug.Log("mM있는.");
        }
        if (MDDStat == null)
        {
            Debug.Log("MDD없는.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer += Time.deltaTime;
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
