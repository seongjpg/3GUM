using UnityEngine;

[CreateAssetMenu(menuName = "Active/Important/SetActive")]
public class SetActive : ScriptableObject, IActiveEquip
{
    [SerializeField] ScriptableObject skill_02;

    public void SaveActive(GameObject user, GameObject prefab)
    {
        if (user != null && prefab != null)
        {
            PlayerManager pm = user.GetComponent<PlayerManager>();
            PlayerMovement pMov = user.GetComponent<PlayerMovement>();

            pMov.skill02 = skill_02 as ISkill;
            pm.CoolTime_02 = pMov.skill02.SkillTimer();
        }
        else
        {
            Debug.Log("플레이어 존재 안한다!");
        }
    }

}
