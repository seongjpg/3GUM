using UnityEngine;

[CreateAssetMenu(menuName =("Weapon/Wand/WandShield"))]
public class Skill01_WandShield : ScriptableObject, ISkill // 스킬1을 눌렀을 때 보호막이 나타나는 스크립트
{
    [SerializeField] GameObject ShieldObj;

    public float SkillTimer()
    {
        WandShield ws = ShieldObj.GetComponent<WandShield>();
        return ws.CoolTime();
    }
    public void SkillEffect(GameObject user)
    {
        if (ShieldObj != null)
        {
            Debug.Log("보호막.");
            GameObject.Instantiate(ShieldObj, user.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("보호막 없음");
        }
    }
}
