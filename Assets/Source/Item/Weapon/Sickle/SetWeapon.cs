using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/Important/SetWeapon")]
public class SetWeapon : ScriptableObject, IWeaponEquip
{

    [SerializeField] GameObject weaponObj;
    [SerializeField] ScriptableObject skill_01;

    //PlayerMovement에서 여기있는 세 함수를 호출하여 사용하게끔 수정한다. Sword에서 구조 변경이 완료되었으면, 이를 KenKi에서도 수정해줄것.

    public void SaveWeapon(GameObject user, GameObject prefab)
    {
        if (user != null && prefab != null)
        {
            PlayerManager pm = user.GetComponent<PlayerManager>();
            PlayerMovement pMov = user.GetComponent<PlayerMovement>();

            pMov.playerWeapon = weaponObj;
            pMov.skill01 = skill_01 as ISkill;
            pm.CoolTime_01 = pMov.skill01.SkillTimer();
        }
        else
        {
            Debug.Log("플레이어 존재 안한다!(by Shield)");
        }
    }

}
