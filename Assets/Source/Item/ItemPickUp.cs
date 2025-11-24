using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
	[SerializeField] private ScriptableObject shieldAsset;
	[SerializeField] private ScriptableObject spellAsset;
	[SerializeField] private ScriptableObject passiveAsset;
	//액티브의 스크립터블 오브젝트 적용해야 함. 실드에셋은 패스브로 편입시켜야 한다.
	private IShield shield;
	private IAffectToPlayer spell;
	private IPassiveEquip passive;

	private void Start()
	{
		shield = shieldAsset as IShield;

		spell = spellAsset as IAffectToPlayer;

		passive = passiveAsset as IPassiveEquip;
	}
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		GameObject collided = collision.gameObject;
		//Active
		if (collided.CompareTag("Player"))
		{
			if (this.CompareTag("Active")) // 액티브 아이템
			{
				Debug.Log("액티브 아이템!");
				spell.SaveSpell(collided, this.gameObject);
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
			}
			if (this.CompareTag("Shield")) // 방어구
			{
				Debug.Log("방어구!");
				shield.Upgrade(collided); // 사실 스펠과 동일한 구조로 만들어져야 한다.
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
