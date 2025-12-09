using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
	[SerializeField] private ScriptableObject WeaponAsset;
	[SerializeField] private ScriptableObject spellAsset;
	[SerializeField] private ScriptableObject passiveAsset;
	[SerializeField] private ScriptableObject activeAsset;

	private IWeaponEquip weapon;
	private IAffectToPlayer spell;
	private IPassiveEquip passive;
	private IActiveEquip active;

	private void Start()
	{
		weapon = WeaponAsset as IWeaponEquip;

		spell = spellAsset as IAffectToPlayer;

		passive = passiveAsset as IPassiveEquip;

		active = activeAsset as IActiveEquip;
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		GameObject collided = collision.gameObject;
		if (collided.CompareTag("Player"))
		{
			if (this.CompareTag("Active")) // 액티브 아이템
			{
				Debug.Log("액티브 아이템!");
				active.SaveActive(collided, this.gameObject);
				Destroy(this.gameObject);
			}
			if (this.CompareTag("Passive")) // 패시브 아이템
			{
				Debug.Log("패시브 아이템!"); // 현재 방어구와 동일한 구조
				passive.SavePassive(collided, this.gameObject); // 최초 실행부
				Destroy(this.gameObject);
			}
			if (this.CompareTag("Weapon")) // 무기
			{
                Debug.Log("무기!"); // 현재 방어구와 동일한 구조
                weapon.SaveWeapon(collided, this.gameObject); // 최초 실행부
                Destroy(this.gameObject);
            }
			if (this.CompareTag("OneTime")) // 일회용 아이템
			{
				Debug.Log("1회용 아이템");
				spell.SaveSpell(collided, this.gameObject);
				Destroy(this.gameObject);
			}

		}
		
	}
}
