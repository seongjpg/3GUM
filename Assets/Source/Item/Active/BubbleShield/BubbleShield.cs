using UnityEngine;

[CreateAssetMenu(menuName = ("Active/BubbleShield"))]
public class Active_BubbleShield: ScriptableObject, ISkill
{
    [SerializeField] GameObject Bubble_Obj;

    public float SkillTimer()
    {
        BubbleShield_Effect active = Bubble_Obj.GetComponent<BubbleShield_Effect>();
        return active.CoolTime();
    }
    public void SkillEffect(GameObject user)
    {
        if (Bubble_Obj != null)
        {
            Debug.Log("액티브 효과 발동");
            GameObject.Instantiate(Bubble_Obj, user.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("소환할 프리펩이 없음");
        }
    }

}
