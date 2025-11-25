using UnityEngine;

[CreateAssetMenu(menuName = "Passive/RunningShoes")]

public class RunningShoes : ScriptableObject, IPassiveEquip, IFusionWithWeapon, IApplyEffect
{
    [SerializeField] float speed = 0.5f;

    public void SavePassive(GameObject user, GameObject prefab)
    {
        if (user != null && prefab != null)
        {
            PlayerManager pm = user.GetComponent<PlayerManager>();
            GameObject save = Instantiate(
                    prefab,
                    user.transform.position,
                    Quaternion.identity,
                    user.transform
                    );
            pm.passive.Add(save);
            pm.applyPassives.Add(this);
            ApplyPassive(user);
            save.SetActive(false);
        }
        else
        {
            Debug.Log("플레이어 존재 안한다!(by Shield)");
        }
    }

    public void AddEffectToWeapon(GameObject weapon)
    {
        //추후 추가 요망
        return;
    }
    public void RemoveEffectFromWeapon(GameObject weapon)
    {
        // 추후 추가 요망
        return;
    }

    public void ApplyPassive(GameObject user)
    {
        PlayerManager pm = user.GetComponent<PlayerManager>();
        if (pm != null)
        {
            pm.speed += speed;
        }
    }

    public void ApplyEffect(GameObject user)
    {
        return;
    }


    public void LostEffect(GameObject user)
    {
        PlayerMovement pMov = user.GetComponent<PlayerMovement>();
        if (pMov.playerWeapon != null)
        {
            PlayerBullet pb = pMov.playerWeapon.GetComponent<PlayerBullet>();
            pb.destroyByCollision = true;
        }
    }

    public void RemoveFromPM(GameObject user, GameObject passiveName, IApplyEffect pass)
    {
        PlayerManager pm = user.GetComponent<PlayerManager>();
        PlayerMovement pMov = user.GetComponent<PlayerMovement>();
        PlayerBullet pb = pMov.playerWeapon.GetComponent<PlayerBullet>();
        LostEffect(user);
        //RemoveEffectFromWeapon(pb);
        pm.passive.Remove(passiveName);
        pm.applyPassives.Remove(pass);
    }
}
