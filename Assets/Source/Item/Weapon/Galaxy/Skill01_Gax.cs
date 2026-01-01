using UnityEngine;

[CreateAssetMenu(menuName =("Weapon/Galaxy/Gax"))]
public class Skill01_Gax : ScriptableObject, ISkill // 스킬1을 눌렀을 때 보호막이 나타나는 스크립트
{
    [SerializeField] GameObject GaxObj;

    public float SkillTimer()
    {
        Gax gax = GaxObj.GetComponent<Gax>();
        return gax.CoolTime();
    }
    public void SkillEffect(GameObject user)
    {
        if (GaxObj != null)
        {
            Debug.Log("gax.");
            GameObject.Instantiate(GaxObj, user.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("gax 없음");
        }
    }
}
