using UnityEngine;

interface IWeaponBody
{
    float DMG(); // 대미지. 이는 스킬과 평타를 구분할 필요가 없다. 어차피 닿으면 피해 입는 구조는 똑같고, 이 인터페이스의 목적은 결국 대미지를 적에게 전달해주기 위함이니까.
}
public interface IWeaponEquip
{
    void SaveWeapon(GameObject user, GameObject prefab); // 주운 무기를 PlayerManager의 Weapon에 반영
}

interface IWeapon
{
    void SkillATK();
    void DefaultATK();
}

