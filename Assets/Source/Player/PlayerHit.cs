using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private PlayerManager PlayerEntity;
    private PlayerVisualHandler PVH;

    void Awake()
    {
        PlayerEntity = GetComponentInParent<PlayerManager>();
        if (PlayerEntity == null)
            Debug.LogError("EntityParent not found in parents of " + gameObject.name);
        PVH = GetComponent<PlayerVisualHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;
        //적과 충돌시
        if (collided.CompareTag("Enemy"))
        {
            if (PlayerEntity.isInvicible)
            {
                Debug.Log("무적!");
                return;
            }
            else
            {

                Debug.Log("적과 접촉!");
                MobManager_Legacy mm = collided.GetComponent<MobManager_Legacy>();
                // 피해 처리 (이펙트, 데미지 등)
                if (Random.value > PlayerEntity.hitRate)
                {
                    Debug.Log("피해 방어!");
                }
                // 피해 처리 (이펙트, 데미지 등)
                else
                {
                    Debug.Log($"{mm.damage}만큼의 피해!");
                    PlayerEntity.curHealth -= mm.damage;
                    PVH.Blink(1f);
                    PlayerEntity.BeInvicible(1f);
                }
            }
        }

        //아이템 드랍(여기서 안할듯?)
        if (collided.CompareTag("Active") || collided.CompareTag("Passive") ||
            collided.CompareTag("Weapon") || collided.CompareTag("Shield") ||
            collided.CompareTag("OneTime"))
        {
            string name = collision.gameObject.name;
            PlayerEntity.equipdItemName = name;
            PlayerEntity.equipdItemType = collided.tag;
            Debug.Log($"아이템 탐지 : {name}");
        }

        //다음 스테이지 이동 차원문
        if (collided.CompareTag("Gate"))
        {
            Debug.Log("차원문 이동..");
        }
    }
}
