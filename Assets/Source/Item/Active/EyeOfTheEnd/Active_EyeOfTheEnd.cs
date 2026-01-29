using UnityEngine;

[CreateAssetMenu(menuName = ("Active/EyeOfTheEnd"))]
public class Active_EyeOfTheEnd : ScriptableObject, ISkill
{
    [SerializeField] GameObject Eye_Obj;

    public float SkillTimer()
    {
        EyeEffect active = Eye_Obj.GetComponent<EyeEffect>();
        return active.CoolTime();
    }
    public void SkillEffect(GameObject user)
    {
        if (Eye_Obj != null)
        {
            Debug.Log("액티브 효과 발동");
            GameObject.Instantiate(Eye_Obj, user.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("소환할 프리펩이 없음");
        }
    }

}
