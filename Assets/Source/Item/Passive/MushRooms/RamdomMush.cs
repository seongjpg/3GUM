using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

[CreateAssetMenu(menuName = "Passive/RandMush")]

public class RandomMush : ScriptableObject, IPassiveEquip, IFusionWithWeapon, IApplyEffect
{
    [SerializeField] public int health;
    [SerializeField] public int damage;
    [SerializeField] public int fireRate;
    [SerializeField] public int speed;
    [SerializeField] public int HitRate;

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
        int[] x = new int[5];
        if (pm != null)
        {
            for (int i = 0; i < 5; i++)
            {
                x[i] = Random.Range(int.MinValue, int.MaxValue) / 2;
            }
            pm.curHealth = x[0] == 0 ? pm.curHealth / 2 : (pm.curHealth * 2 > pm.maxHealth ? pm.maxHealth : pm.curHealth * 2);
            pm.speed = x[1] == 0 ? pm.speed / 2 : pm.speed * 2;
            pm.hitRate = x[2] == 0 ? pm.hitRate / 2 : pm.hitRate * 2;
            pm.damage = x[3] == 0 ? pm.damage / 2 : pm.damage * 2;
            pm.fireRate = x[4] == 0 ? pm.damage * 2 : pm.damage * 2;
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
