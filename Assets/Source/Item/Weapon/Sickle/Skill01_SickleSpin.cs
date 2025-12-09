using UnityEngine;

[CreateAssetMenu(menuName = ("Weapon/Sickle/SickleSpin"))]
public class Skill01_SickleSpin : ScriptableObject, ISkill
{
    [SerializeField] GameObject SickleSpinObj;

    public float SkillTimer()
    {
        SickleSpin spin = SickleSpinObj.GetComponent<SickleSpin>();
        return spin.CoolTime();
    }
    public void SkillEffect(GameObject user)
    {
        if (SickleSpinObj != null)
        {
            Debug.Log("검기 발사.");
            GameObject.Instantiate(SickleSpinObj, user.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("검기가 없음");
        }
    }
}
