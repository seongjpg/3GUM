using UnityEngine;

[CreateAssetMenu(menuName = ("Active/SilverBullet"))]
public class SilverBullet: ScriptableObject, ISkill
{
    [SerializeField] GameObject BulletObj;

    public float SkillTimer()
    {
        SilverBullet_Bullet bullet = BulletObj.GetComponent<SilverBullet_Bullet>();
        return bullet.CoolTime();
    }
    public void SkillEffect(GameObject user)
    {
        if (BulletObj != null)
        {
            Debug.Log("액티브 효과 발동");
            GameObject.Instantiate(BulletObj, user.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("소환할 프리펩이 없음");
        }
    }

}
