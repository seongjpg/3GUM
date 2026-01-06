using UnityEngine;

[CreateAssetMenu(menuName = "Passive/BrokenCross")]

public class BrokenCross : ScriptableObject, IPassiveEquip, IFusionWithWeapon, IApplyEffect
{
    [SerializeField] float hitRate = 2f;
    [SerializeField] private GameObject BubbleSupplier;

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
            ApplyEffect(user);
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
            pm.hitRate *= hitRate;
        }
    }

    public void ApplyEffect(GameObject user)
    {
        GameObject.Instantiate(BubbleSupplier, Vector3.zero, Quaternion.identity);
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
