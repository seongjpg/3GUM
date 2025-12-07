using UnityEngine;

public class Strv_Field_Script : MonoBehaviour {
	[SerializeField] float durableTime = 10f;
	[SerializeField] float slowP = 0.3f;
	private float timer = 0;

	private void Update()
	{
		timer += Time.deltaTime;
		if (timer >= durableTime)
		{
			Destroy(gameObject);
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{

		if (collision.CompareTag("Enemy"))
		{
			MobManager mm = collision.GetComponent<MobManager>();
			mm.speed *= 1-slowP;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Enemy"))
		{
			MobManager mm = collision.GetComponent<MobManager>();
			mm.speed /= 1-slowP;
		}
	}
}
