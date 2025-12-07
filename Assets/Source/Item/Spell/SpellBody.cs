using UnityEngine;

[CreateAssetMenu(menuName = "Spell/Important/SpellBody")]

public class SpellEffect : ScriptableObject, IAffectToPlayer, IUseSpell
{
    [SerializeField] GameObject FieldPrefab;
    [SerializeField] private ScriptableObject se;
    ISpellEffect spellEffect;
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
            Debug.Log("플레이어 존재하지 않는다.(by StarveField)");
        }
    }

    

    public void UseSpell(GameObject user, GameObject prefab)
    {
        spellEffect = se as ISpellEffect;
        if (user != null && prefab != null)
        {
            spellEffect.Effect(user, FieldPrefab);
        }
        return;
    }

    public void DiscardSpell(PlayerManager pm)
    {
        Debug.Log("스펠 뱉기");

        Instantiate(pm.oneTime,
                new Vector3(pm.transform.position.x, pm.transform.position.y - 2, pm.transform.position.z),
                Quaternion.identity);
        Destroy(pm.oneTime);
        pm.oneTime = null;
        pm.oneTimeSpell = null;
    }

}
