using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class MobManager : MonoBehaviour
{
    [SerializeField] MobStat mobStat;

    public float maxHealth;
    public float curHealth;
    public float speed;
    public int damage;
    public bool hasRangeAttack;
    public List<GameObject> projectTile;

    void Start()
    {
        // 스탯 초기화를 직접 처리
        maxHealth = mobStat.maxHealth;
        curHealth = mobStat.curHealth;
        damage = mobStat.damage;
        speed = mobStat.speed;
        hasRangeAttack = mobStat.hasRangeAttack;
        projectTile = mobStat.projectTile;
    }

    // 일시적 속도 변경
    public void TempSpeedChange(float multiplier, float duration)
    {
        StartCoroutine(TempStatRoutine(() => speed, v => speed = v, multiplier, duration));
    }

    // 일시적 공격력 변경
    public void TempDamageChange(int multiplier, float duration)
    {
        StartCoroutine(TempStatRoutine(() => damage, v => damage = (int)v, multiplier, duration));
    }

    private IEnumerator TempStatRoutine(System.Func<float> getter, System.Action<float> setter, float multiplier, float duration)
    {
        float original = getter();
        setter(original * multiplier);
        yield return new WaitForSeconds(duration);
        setter(original);
    }
}