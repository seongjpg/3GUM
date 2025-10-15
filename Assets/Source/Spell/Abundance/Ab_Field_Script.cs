using UnityEngine;

public class Ab_Field_Script : MonoBehaviour
{
	[SerializeField] float durableTime = 10f;
	[SerializeField] float healAmt = 3;
	private float timer = 0;
	private float localTimer = 0;
	private void Update()
	{
		timer += Time.deltaTime;
		if (timer >= durableTime)
		{
			Destroy(gameObject);
		}
	}
	private void OnTriggerStay2D(Collider2D collision)
	{
		
		if (collision.CompareTag("Player"))
		{	
			localTimer += Time.deltaTime;
			PlayerManager pm = collision.GetComponent<PlayerManager>();
			if (localTimer >= 1)
			{
				pm.curHealth += healAmt;
				if (pm.curHealth > pm.maxHealth)
				{
					pm.curHealth = pm.maxHealth;
				}
				localTimer = 0;
			}
        }
	}
}
