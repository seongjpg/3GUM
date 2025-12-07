using UnityEngine;
[CreateAssetMenu(menuName = "Spell/Effect/SummonPrefab")]
public class SummonPrefab : ScriptableObject, ISpellEffect, ISummonPrefab
{
    float timer;
    public void Effect(GameObject user, GameObject prefab)
    {
        UsePrefab(user, prefab);
    }

    public void UsePrefab(GameObject user, GameObject prefab)
    {
        PlayerManager pm = user.GetComponent<PlayerManager>();
        Vector3 userPos = user.transform.position;
        if (user != null && prefab != null)
        {
            Instantiate(prefab, userPos, Quaternion.identity);
            Destroy(pm.oneTime);
            pm.oneTime = null;
            pm.oneTimeSpell = null;
        }
        else
        {
            Debug.Log("프리팹 존재하지 않음!");
        }
    }
}
