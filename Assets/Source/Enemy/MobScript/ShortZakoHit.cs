using UnityEngine;

public class ShortZakoHit : MonoBehaviour
{
    private Zako_shortRich MobEntity;

    void Awake()
    {
        MobEntity = GetComponentInParent<Zako_shortRich>();
        if (MobEntity == null)
            Debug.LogError("EntityParent not found in parents of " + gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject collided = other.gameObject;
        WeaponStatus weapon = collided.GetComponent<WeaponStatus>();
        if (collided.CompareTag("PlayerAttack"))
        {
            Debug.Log("공격 성공!");
            MobEntity.mm.curHealth -= MobEntity.pm.damage * weapon.damage; // 단순히 공격력에서 차감하는 것이 아닌, 계수를 고려해주어야 한다.
        }
    }
}
