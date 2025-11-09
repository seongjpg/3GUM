using UnityEngine;

public class Ab_Field_Script : MonoBehaviour
{

	[SerializeField] float durableTime = 10f;
	[SerializeField] public int healAmt = 3;
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
            GameObject player = collision.gameObject;
            PlayerManager pm = player.GetComponent<PlayerManager>();
            PlayerMovement pMov = player.GetComponent<PlayerMovement>();
            if (localTimer >= 1)
            {
                localTimer -= 1;
                pm.curHealth += healAmt;
                //ui에서의 작용도 해주어야 한다.
                if (pm.curHealth > pm.maxHealth)
                {
                    pm.curHealth = pm.maxHealth;
                }
                pMov.EarnHeal();
            }
        }
    }
}
