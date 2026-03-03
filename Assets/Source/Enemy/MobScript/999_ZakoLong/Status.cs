using System;
using UnityEngine;
using System.Collections;

public class Status : MonoBehaviour, IMobStatus
{
    public void GetMobStatus(MobManager MM, MobStat Ms) 
    {
        MM.maxHealth = Ms.maxHealth;
        MM.curHealth = Ms.curHealth;
        MM.damage = Ms.damage;
        MM.hasRangeAttack = Ms.hasRangeAttack;
        MM.speed = Ms.speed;
        MM.projectTile = Ms.projectTile;
    }
    public void UpdateMobStatusPermanent(MobManager MM, string statName, float amt) 
    {
        switch (statName)
        {
            case "curHealth":
                MM.curHealth -= amt;
                return;
            case "damage":
                MM.damage *= Convert.ToInt32(amt);
                return;
            case "speed":
                MM.speed *= amt;
                return;
            default:
                Debug.Log("해당하는 값 존재하지 않음");
                return;
        }
    }
    public void UpdateMobStatusTemporarily(MobManager MM, string statName, float amt, float time)
    {
        StartCoroutine(TemporaryStatChange(MM, statName, amt, time));
    }

    private IEnumerator TemporaryStatChange(MobManager MM, string statName, float amt, float time)
    {
        // 원본 저장
        float original = 0f;
        switch (statName)
        {
            case "curHealth": original = MM.curHealth; break;
            case "damage": original = MM.damage; break;
            case "speed": original = MM.speed; break;
        }

        // 적용
        UpdateMobStatusPermanent(MM, statName, amt);

        yield return new WaitForSeconds(time);

        // 원본으로 복구
        switch (statName)
        {
            case "curHealth": MM.curHealth = original; break;
            case "damage": MM.damage = Convert.ToInt32(original); break;
            case "speed": MM.speed = original; break;
        }
    }
}
