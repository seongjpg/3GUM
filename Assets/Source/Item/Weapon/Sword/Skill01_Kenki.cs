using UnityEngine;

[CreateAssetMenu(menuName =("Weapon/Sword/Kenki"))]
public class Skill01_Kenki : ScriptableObject, ISkill
{
    [SerializeField] GameObject KenkiObj;

    public float SkillTimer()
    {
        Kenki kk = KenkiObj.GetComponent<Kenki>();
        return kk.CoolTime();
    }
    public void SkillEffect(GameObject user)
    {
        if (KenkiObj != null)
        {
            Debug.Log("검기 발사.");
            GameObject.Instantiate(KenkiObj, user.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("검기가 없음");
        }
    }
}
