using UnityEngine;

public class LongZakoHit : MonoBehaviour
{
    private zako_longRich MobEntity;

    void Awake()
    {
        MobEntity = GetComponentInParent<zako_longRich>();
        if (MobEntity == null)
            Debug.LogError("EntityParent not found in parents of " + gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collided = other.gameObject;
        IWeaponBody weaponBody = collided.GetComponent<IWeaponBody>();
        if (collided.CompareTag("PlayerAttack"))
        {
            Debug.Log("공격 성공!");
            MobEntity.mm.curHealth -= MobEntity.pm.damage * weaponBody.DMG(); ; // 단순히 공격력에서 차감하는 것이 아닌, 계수를 고려해주어야 한다.

        }
    }
}
