using UnityEngine;

[CreateAssetMenu(menuName = ("Active/TheHands"))]
public class Active_TheHands : ScriptableObject, ISkill
{
    [SerializeField] GameObject HandsObj;

    public float SkillTimer()
    {
        TheHands_Effect active = HandsObj.GetComponent<TheHands_Effect>();
        return active.CoolTime();
    }
    public void SkillEffect(GameObject user)
    {
        if (HandsObj != null)
        {
            Debug.Log("액티브 효과 발동");
            GameObject.Instantiate(HandsObj, user.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("소환할 프리펩이 없음");
        }
    }

}
