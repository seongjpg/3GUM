using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
	private GameObject GameManager;
	private GameManager GM;

	[SerializeField] private ScriptableObject WeaponAsset;
	[SerializeField] private ScriptableObject spellAsset;
	[SerializeField] private ScriptableObject passiveAsset;
	[SerializeField] private ScriptableObject activeAsset;

	private IWeaponEquip weapon;
	private IAffectToPlayer spell;
	private IPassiveEquip passive;
	private IActiveEquip active;

	private void Awake()
	{
		weapon = WeaponAsset as IWeaponEquip;

		spell = spellAsset as IAffectToPlayer;

		passive = passiveAsset as IPassiveEquip;

		active = activeAsset as IActiveEquip;

		GameManager = GameObject.FindWithTag("GameManager");
		GM = GameManager.GetComponent<GameManager>();
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		GameObject collided = collision.gameObject;
		if (collided.CompareTag("Player"))
		{
			if (this.CompareTag("Active")) // 액티브 아이템
			{
				SA(collided, this.gameObject);
			}
			if (this.CompareTag("Passive")) // 패시브 아이템
			{
				SP(collided, this.gameObject);
			}
			if (this.CompareTag("Weapon")) // 무기
			{
				SW(collided, this.gameObject);
            }
			if (this.CompareTag("OneTime")) // 일회용 아이템
			{
				SS(collided, this.gameObject);
			}

		}
		
	}

	public void SA(GameObject player, GameObject activeItem)
	{
        Debug.Log("액티브 아이템!");
        active.SaveActive(player, activeItem);
		GM.SetActivePath(activeItem);
		Destroy(this.gameObject);
    }
	public void SP(GameObject player, GameObject passiveItem)
	{
		Debug.Log("패시브 아이템!"); // 현재 방어구와 동일한 구조
        passive.SavePassive(player, passiveItem); // 최초 실행부
		GM.SetPassivePath(passiveItem);
        Destroy(this.gameObject);
    }
	public void SW(GameObject player, GameObject weaponItem)
    {
        Debug.Log("무기!"); // 현재 방어구와 동일한 구조
        weapon.SaveWeapon(player, weaponItem); // 최초 실행부
		GM.SetWeaponPath(weaponItem);
        Destroy(this.gameObject);
    }
	public void SS(GameObject player, GameObject spellItem)
	{
		Debug.Log("1회용 아이템");
        spell.SaveSpell(player, spellItem);
		GM.SetoneTimePath(spellItem);
        Destroy(this.gameObject);
    }


}
