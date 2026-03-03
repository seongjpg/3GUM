using UnityEngine;
using System.Collections.Generic;


[CreateAssetMenu(menuName = "Mob/Important/Status")]

public class MobStat : ScriptableObject
{
    [SerializeField] public float maxHealth; // 최대 체력
    [SerializeField] public float curHealth; // 현재 체력
    [SerializeField] public float speed; // 이동 속도
    [SerializeField] public int damage; // 공격력
    [SerializeField] public bool hasRangeAttack; // 원거리 공격 가능 여부
    [SerializeField] public List<GameObject> projectTile; //원거리 공격 무기-리스트로 여러 원거리 투사체가 존재하는 상황에 대처
}
