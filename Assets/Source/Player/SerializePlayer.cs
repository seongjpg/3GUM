using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class SerializePlayer : MonoBehaviour
{
	private int maxHealth; // 최대 체력
	private int curHealth; // 현재 체력
	private float maxSpeed;
	private float speed; // 이동 속도
	private float hitRate; // 피격 확률(0-1범위)
	private float damage; // 공격력
	private float fireRate; // 연사 속도

	/*
	public GameObject oneTime;
	public IUseSpell oneTimeSpell;

	public List<GameObject> passive = new List<GameObject>();
	public List<IApplyEffect> applyPassives = new List<IApplyEffect>();
	*/

	private void Update()
	{
		if (Input.GetKeyDown("w"))
		{
			Debug.Log("json 직렬화");
			SavePlayerData();
		}
		if (Input.GetKeyDown("r"))
		{
			Debug.Log("json 직렬화 파일 해제");
			LoadPlayerData();
		}
	}
	

	void SavePlayerData() // 외부에서 json 파일 변환을 필요로 할 때 호출
	{
		GameObject player = GameObject.FindWithTag("Player");
		PlayerManager pm = player.GetComponent<PlayerManager>();
		maxHealth = pm.maxHealth;
		curHealth = pm.curHealth;
		maxSpeed = pm.maxSpeed;
		speed = pm.speed;
		hitRate = pm.hitRate;
		damage = pm.damage;
		fireRate = pm.fireRate;
		Vector3 pos = player.transform.position;
		//더 저장할 요소가 필요한 경우 여기에 반영

		SerializeP playerJsonData = new SerializeP(maxHealth, curHealth, maxSpeed, speed, hitRate, damage, fireRate, pos);
		string playerJson = ObjectToJson(playerJsonData);
		CreateJsonFile($"{Application.persistentDataPath}/", "user", playerJson);
	}

	void LoadPlayerData()
	{
		var playerJson =  LoadJsonFile<SerializeP>($"{Application.persistentDataPath}/", "user");
		playerJson.ApplyStatus();
	}

	string ObjectToJson(object obj)
	{
		return JsonUtility.ToJson(obj);
	}

	T JsonToObject<T>(string jsonData)
	{
		return JsonUtility.FromJson<T>(jsonData);
	}

	void CreateJsonFile(string createPath, string fileName, string jsonData)
	{
		FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", createPath, fileName), FileMode.Create);
		byte[] data = Encoding.UTF8.GetBytes(jsonData);
		fileStream.Write(data, 0, data.Length);
		fileStream.Close();
	}

	T LoadJsonFile<T>(string loadPath, string fileName)
	{
		FileStream fileStream = new FileStream(string.Format("{0}/{1}.json", loadPath, fileName), FileMode.Open);
		byte[] data = new byte[fileStream.Length];
		fileStream.Read(data, 0, data.Length);
		fileStream.Close();
		string jsonData = Encoding.UTF8.GetString(data);
		return JsonUtility.FromJson<T>(jsonData);
	}

}



[System.Serializable]
public class SerializeP {
	public int maxHealth; // 최대 체력
	public int curHealth; // 현재 체력
	public float maxSpeed;
	public float speed; // 이동 속도
	public float hitRate; // 피격 확률(0-1범위)
	public float damage; // 공격력
	public float fireRate; // 연사 속도
	public Vector3 pos; // 플레이어 위치


	/*
	public GameObject oneTime;
	public IUseSpell oneTimeSpell;

	public List<GameObject> passive = new List<GameObject>();
	public List<IApplyEffect> applyPassives = new List<IApplyEffect>();
	*/

	public string equipdItemName;
	public string equipdItemType;
	
	public SerializeP()
	{

	}

	public SerializeP(int maxHealth, int curHealth, float maxSpeed, float speed, float hitRate, float damage, float fireRate, Vector3 pos)
	{
		this.maxHealth = maxHealth;
		this.curHealth = curHealth;
		this.maxSpeed = maxSpeed;
		this.speed = speed;
		this.hitRate = hitRate;
		this.damage = damage;
		this.fireRate = fireRate;
		this.pos = pos;

		//추가할 항목이 생기면 여기에서 추가.
	}

	public void ApplyStatus()
	{
		GameObject player = GameObject.FindWithTag("Player");
		PlayerManager pm = player.GetComponent<PlayerManager>();
		pm.maxHealth = maxHealth;
		pm.curHealth = curHealth;
		pm.maxSpeed = maxSpeed;
		pm.speed = speed;
		pm.hitRate = hitRate;
		pm.damage = damage;
		pm.fireRate = fireRate;
		player.transform.position = pos;
	}
}