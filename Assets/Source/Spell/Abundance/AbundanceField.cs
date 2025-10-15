using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Spell/AbundacneField")]

public class AbundanceField : ScriptableObject, IAffectToPlayer, ISummonPrefab, IUseSpell
{
    [SerializeField] GameObject FieldPrefab;
    public void SaveSpell(GameObject user, GameObject prefab)
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

			if (pm.oneTime == null)
			{
				pm.oneTime = save;
				pm.oneTimeSpell = this;

				Debug.Log("일회용 아이템 장착!");
			}

			//교체
			else
			{
                DiscardSpell(pm);
				pm.oneTime = save;
				pm.oneTimeSpell = this;

				Debug.Log("일회용 아이템 교체");
			}
		}
        else
        {
            Debug.Log("플레이어 존재하지 않는다.");
        }
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

    public void UseSpell(GameObject user, GameObject prefab)
    {
        if (user != null && prefab != null)
        {
            Debug.Log("스펠 사용! 이제 내용물 구현하거라");
            UsePrefab(user, FieldPrefab);
        }
        return;
    }

    public void DiscardSpell(PlayerManager pm)
    {
		Instantiate(pm.oneTime,
				new Vector3(pm.transform.position.x, pm.transform.position.y - 2, pm.transform.position.z),
				Quaternion.identity);
        Destroy(pm.oneTime);
        pm.oneTime = null;
        pm.oneTimeSpell = null;
	}
}
