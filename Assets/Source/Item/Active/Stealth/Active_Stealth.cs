using UnityEngine;

[CreateAssetMenu(menuName = ("Active/Stealth"))]
public class Active_Stealth : ScriptableObject, ISkill
{
    [SerializeField] GameObject Stealth_Obj;

    public float SkillTimer()
    {
        Stealth_Effect active = Stealth_Obj.GetComponent<Stealth_Effect>();
        return active.CoolTime();
    }
    public void SkillEffect(GameObject user)
    {
        if (Stealth_Obj != null)
        {
            Debug.Log("액티브 효과 발동");
            GameObject.Instantiate(Stealth_Obj, user.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("검기가 없음");
        }
    }

}
