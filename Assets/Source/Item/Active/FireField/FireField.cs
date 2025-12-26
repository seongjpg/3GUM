using UnityEngine;

[CreateAssetMenu(menuName = ("Active/FireField"))]
public class Active_FireField : ScriptableObject, ISkill
{
    [SerializeField] GameObject FireField_Obj;

    public float SkillTimer()
    {
        FireField_Effect active = FireField_Obj.GetComponent<FireField_Effect>();
        return active.CoolTime();
    }
    public void SkillEffect(GameObject user)
    {
        if (FireField_Obj != null)
        {
            Debug.Log("액티브 효과 발동");
            GameObject.Instantiate(FireField_Obj, user.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("소환할 프리펩이 없음");
        }
    }

}
