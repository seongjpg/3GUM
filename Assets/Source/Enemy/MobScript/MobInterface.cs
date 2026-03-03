using UnityEngine;

public interface IMobStatus 
{
    public void GetMobStatus(MobManager MM, MobStat Ms);
    public void UpdateMobStatusPermanent(MobManager MM, string statName, float amt);
    public void UpdateMobStatusTemporarily(MobManager MM, string statName, float amt, float time);

}

public interface IMeleeAttack
{

}

public interface IRangedAttack
{
    public void GetProjectTile();

}
