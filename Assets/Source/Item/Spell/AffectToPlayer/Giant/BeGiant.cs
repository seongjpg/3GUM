using UnityEngine;
[CreateAssetMenu(menuName ="Spell/Effect/ApplyToPlayer/Giant")]
public class BeGiant : ScriptableObject, ISpellEffect
{
    [SerializeField] float durable = 10f;
    float timer;
    public void Effect(GameObject user, GameObject prefab) 
    {
        GameObject.Instantiate(prefab);
        return;
    }

}
