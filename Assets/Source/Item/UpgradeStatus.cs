using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/UpgradeStatus")]

public class UpgradeStatus : ScriptableObject, IShield {
	[SerializeField] float speed;
	[SerializeField] int maxHealth;
	[SerializeField] float hitRate;
	[SerializeField] float damage;
	[SerializeField] float fireRate;

	public string description => $"Ω∫≈» ¡ı∞°! : {speed}";
	public void Upgrade(GameObject user)
	{
		PlayerManager pm = user.GetComponent<PlayerManager>();
		if (pm != null)
		{
			pm.speed += speed;
			pm.maxHealth += maxHealth;
			pm.hitRate *= hitRate;
			pm.damage += damage;
			pm.fireRate += fireRate;
		}
	}
}
