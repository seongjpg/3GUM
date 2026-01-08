using UnityEngine;

[CreateAssetMenu(menuName = ("Weapon/Mirror/MirrorForce"))]
public class Skill01_MirrorForce : ScriptableObject, ISkill // 스킬1을 눌렀을 때 보호막이 나타나는 스크립트
{
    [SerializeField] GameObject mirrorForceObj;

    public float SkillTimer()
    {
        MirrorForce mf = mirrorForceObj.GetComponent<MirrorForce>();
        return mf.CoolTime();
    }
    public void SkillEffect(GameObject user)
    {
        if (mirrorForceObj != null)
        {
            Debug.Log("거울의 힘.");
            GameObject.Instantiate(mirrorForceObj, user.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("없음");
        }
    }
}
