using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName="Weapon/Galaxy/Galaxy")]
public class Galaxy : ScriptableObject, IWeaponEquip // 완드의 장착을 담당하는 스크립트
{

    [SerializeField] GameObject galaxyObj;
    [SerializeField] ScriptableObject skill_shield;

    //PlayerMovement에서 여기있는 세 함수를 호출하여 사용하게끔 수정한다. Sword에서 구조 변경이 완료되었으면, 이를 KenKi에서도 수정해줄것.
   
    public void SaveWeapon(GameObject user, GameObject prefab)
    {
        if (user != null && prefab != null)
        {
            PlayerManager pm = user.GetComponent<PlayerManager>();
            PlayerMovement pMov = user.GetComponent <PlayerMovement>();
 
            pMov.playerWeapon = galaxyObj;
            pMov.skill01 = skill_shield as ISkill;
            pm.CoolTime_01 = pMov.skill01.SkillTimer();
        }
        else
        {
            Debug.Log("플레이어 존재 안한다!(by Shield)");
        }
    }

}
