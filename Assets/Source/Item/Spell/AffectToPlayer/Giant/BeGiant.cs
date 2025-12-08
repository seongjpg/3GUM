using UnityEngine;
[CreateAssetMenu(menuName ="Spell/Effect/ApplyToPlayer/Giant")]
public class BeGiant : ScriptableObject, ISpellEffect
{
    [SerializeField] float durable = 10f;
    float timer;
    public void Effect(GameObject user, GameObject prefab) 
    {
        user.transform.localScale *= 2;
        Debug.Log("효과 적용");
        if (timer < durable) {
            timer += Time.fixedDeltaTime;
        }
        user.transform.localScale /= 2;
        Debug.Log("효과 해제");
        return;
    }

}
