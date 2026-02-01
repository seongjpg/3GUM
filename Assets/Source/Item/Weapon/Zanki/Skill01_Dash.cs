using UnityEngine;

[CreateAssetMenu(menuName = ("Weapon/TimeLeft/Dash"))]
public class Skill01_Dash : ScriptableObject, ISkill
{
    [SerializeField] GameObject dashObj; // 대미지 처리를 주관해줌.

    public float SkillTimer()
    {
        Dash dash = dashObj.GetComponent<Dash>();
        return dash.CoolTime();
    }
    public void SkillEffect(GameObject user)
    {
        
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && dashObj != null)
        {
            GameObject.Instantiate(dashObj, player.transform.position, Quaternion.identity);

        }
        else
        {
            Debug.Log("플레이어 감지 불가");
        }
    }
}
