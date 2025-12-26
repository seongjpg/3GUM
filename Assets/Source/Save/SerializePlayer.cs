using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SerializePlayer : MonoBehaviour
{
	private GameObject gameManager;
	private GameManager GM;
	//플레이어블 캐릭터. AddressableKey를 사용하여 경로를 저장하는 것.

	//캐릭터의 현재 진행 상황. curHealth와 pos만을 저장하면 된다., 아이템을 재적용하는 시기에 복구할 수 있다.
	private int curHealth; // 현재 체력
	private Vector3 pos;


	//보유 아이템을 가져온 뒤에 그와 관련된 변수들을 반영해주어야 한다.
	//중요한 건, 패시브와 스펠은 플레이어에게 객체를 종속시켜야 하고, 그 외에는 그저 반영을 해주어야 한다(how?)
	/*
	무기
	스킬1
	스킬2(액티브)	
	스펠
	패시브 리스트
	 */
	private string playablePath;

	private string weaponPath;
	private string activePath;
	private string oneTimePath;
    private List<string> passivePath = new List<string>();

	//직렬화 해제시, pos, 남은 체력, 보유 아이템, 플레이어프리펩를 전부 따로 불러오는 편이 좋을 것.


    private void Awake()
    {
		gameManager = GameObject.FindWithTag("GameManager");
		GM = gameManager.GetComponent<GameManager>();
    }
    private void Start()
    {
        Debug.Log("json 직렬화 파일 해제");
        LoadFromSaveData();
    }

    /*
    private void Update()
	{
		if (Input.GetKeyDown("p"))
		{
			Debug.Log("json 직렬화");
			SavePlayerData();
		}
		if (Input.GetKeyDown("o"))
		{
			Debug.Log("json 직렬화 파일 해제");
			LoadFromSaveData();
		}
	}
	*/

	public void SavePlayerData() // 외부에서 json 파일 변환을 필요로 할 때 호출
	{
		GameObject player = GameObject.FindWithTag("Player");
		PlayerManager pm = player.GetComponent<PlayerManager>();

		//더 저장할 요소가 필요한 경우 여기에 반영
		playablePath = GM.playablePath;
		curHealth = pm.curHealth;
		pos = player.transform.position;
		weaponPath = GM.weaponPath;
		activePath = GM.activePath;
		oneTimePath = GM.oneTimePath;
		passivePath = GM.passivePath;

		SerializeP playerJsonData = new SerializeP(playablePath, curHealth, pos, weaponPath, activePath, oneTimePath, passivePath);
		Debug.Log($"weaponPath : {weaponPath}");
		string playerJson = ObjectToJson(playerJsonData);
		CreateJsonFile($"{Application.persistentDataPath}/", "user", playerJson);
	}

    public void SavePlayerDataWithoutPos() // 외부에서 json 파일 변환을 필요로 할 때 호출
    {
        GameObject player = GameObject.FindWithTag("Player");
        PlayerManager pm = player.GetComponent<PlayerManager>();

        //더 저장할 요소가 필요한 경우 여기에 반영
        playablePath = GM.playablePath;
        curHealth = pm.curHealth;
        weaponPath = GM.weaponPath;
        activePath = GM.activePath;
        oneTimePath = GM.oneTimePath;
        passivePath = GM.passivePath;

        SerializeP playerJsonData = new SerializeP(playablePath, curHealth, pos, weaponPath, activePath, oneTimePath, passivePath);
        Debug.Log($"weaponPath : {weaponPath}");
        string playerJson = ObjectToJson(playerJsonData);
        CreateJsonFile($"{Application.persistentDataPath}/", "user", playerJson);
    }

    public void LoadFromSaveData() // 세이브 파일 로드시 사용한다.
	{
		var playerJson =  LoadJsonFile<SerializeP>($"{Application.persistentDataPath}/", "user");
		//playerJson.LoadPlayable(); // 우선 플레이어가 소환되어야 함.
		GameObject player = GameObject.FindWithTag("Player");
		PlayerManager pm = player.gameObject.GetComponent<PlayerManager>();
		playerJson.LoadPlayerHealth(pm);
		playerJson.LoadPos(player);
		playerJson.LoadWeapon(player);
		playerJson.LoadActive(player);
		playerJson.LoadOneTime(player);
		playerJson.LoadPassive(player);
	}


    public void LoadInit() // 초기 로드 및 씬 이동시 사용한다.
    {
        var playerJson = LoadJsonFile<SerializeP>($"{Application.persistentDataPath}/", "user"); // << 여기를 초기 파일로 변경해주어야 한다.
        //playerJson.LoadPlayable(); // 우선 플레이어가 소환되어야 함.
        GameObject player = GameObject.FindWithTag("Player");
        PlayerManager pm = player.gameObject.GetComponent<PlayerManager>();
        playerJson.LoadPlayerHealth(pm);
        playerJson.LoadWeapon(player);
        playerJson.LoadActive(player);
        playerJson.LoadOneTime(player);
        playerJson.LoadPassive(player);
    }
    public void LoadNextScene() // 초기 로드 및 씬 이동시 사용한다.
	{
        var playerJson = LoadJsonFile<SerializeP>($"{Application.persistentDataPath}/", "user");
        //playerJson.LoadPlayable(); // 우선 플레이어가 소환되어야 함.
        GameObject player = GameObject.FindWithTag("Player");
        PlayerManager pm = player.gameObject.GetComponent<PlayerManager>();
        playerJson.LoadPlayerHealth(pm);
        playerJson.LoadWeapon(player);
        playerJson.LoadActive(player);
        playerJson.LoadOneTime(player);
        playerJson.LoadPassive(player);
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
	public int curHealth; // 현재 체력
	public Vector3 pos; // 플레이어 위치


    /*
	public GameObject oneTime;
	public IUseSpell oneTimeSpell;

	public List<GameObject> passive = new List<GameObject>();
	public List<IApplyEffect> applyPassives = new List<IApplyEffect>();
	*/

    public string playablePath;

    public string weaponPath;
    public string activePath;
    public string oneTimePath;
    public List<string> passivePath = new List<string>();

    public SerializeP()
	{

	}

	public SerializeP(string _playablePath, int _curHealth, Vector3 _pos, string _weaponPath, string _activePath, string _oneTimePath, List<string> _passivePath)
	{
		this.playablePath = _playablePath;
		this.curHealth = _curHealth;
		this.pos = _pos;
		this.weaponPath = _weaponPath;
		this.activePath = _activePath;
		this.oneTimePath = _oneTimePath;
		this.passivePath = _passivePath;
		
		Debug.Log(weaponPath);
		//추가할 항목이 생기면 여기에서 추가.
	}


	public void LoadPlayable()
	{
        Addressables.LoadAssetAsync<GameObject>(playablePath).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject player = GameObject.Instantiate(handle.Result, Vector3.zero, Quaternion.identity);
                Debug.Log($"Loaded prefab by key: {playablePath}");
            }
            else
            {
                Debug.LogError($"Failed to load prefab with key: {playablePath}");
            }
        };
    }

	public void LoadPlayerHealth(PlayerManager pm)
	{
		pm.curHealth = curHealth;
	}

	public void LoadPos(GameObject player)
	{
        if (pos != null)
        {
            player.transform.position = pos;
        }
    }

	public void LoadWeapon(GameObject player)
	{
		Debug.Log($"loaded weaponPath : {weaponPath}");
        Addressables.LoadAssetAsync<GameObject>(weaponPath).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject weapon = GameObject.Instantiate(handle.Result, Vector3.zero, Quaternion.identity);
				ItemPickUp IP = weapon.GetComponent<ItemPickUp>();
				if (IP != null)
				{
					IP.SW(player, weapon);
				}
				else
				{
					Debug.Log("ItemPickUp 스크립트 존재하지 않음?!");
				}
					Debug.Log($"Loaded prefab by key: {weaponPath}");
            }
            else
            {
                Debug.LogError($"Failed to load prefab with key: {weaponPath}");
            }
        };
    }

    public void LoadActive(GameObject player)
    {
        Addressables.LoadAssetAsync<GameObject>(activePath).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject active = GameObject.Instantiate(handle.Result, Vector3.zero, Quaternion.identity);
                ItemPickUp IP = active.GetComponent<ItemPickUp>();
                if (IP != null)
                {
                    IP.SA(player, active);
                }
                else
                {
                    Debug.Log("ItemPickUp 스크립트 존재하지 않음?!");
                }
                Debug.Log($"Loaded prefab by key: {activePath}");
            }
            else
            {
                Debug.LogError($"Failed to load prefab with key: {activePath}");
            }
        };
    }

    public void LoadOneTime(GameObject player)
    {
        Addressables.LoadAssetAsync<GameObject>(oneTimePath).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject oneTime = GameObject.Instantiate(handle.Result, Vector3.zero, Quaternion.identity);
                ItemPickUp IP = oneTime.GetComponent<ItemPickUp>();
                if (IP != null)
                {
                    IP.SS(player, oneTime);
                }
                else
                {
                    Debug.Log("ItemPickUp 스크립트 존재하지 않음?!");
                }
                Debug.Log($"Loaded prefab by key: {oneTimePath}");
            }
            else
            {
                Debug.LogError($"Failed to load prefab with key: {oneTimePath}");
            }
        };
    }

    public void LoadPassive(GameObject player)
    {
		foreach (string eachPath in passivePath)
		{
            Addressables.LoadAssetAsync<GameObject>(eachPath).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    GameObject passive = GameObject.Instantiate(handle.Result, Vector3.zero, Quaternion.identity);
                    ItemPickUp IP = passive.GetComponent<ItemPickUp>();
                    if (IP != null)
                    {
                        IP.SP(player, passive);
                    }
                    else
                    {
                        Debug.Log("ItemPickUp 스크립트 존재하지 않음?!");
                    }
                    Debug.Log($"Loaded prefab by key: {passivePath}");
                }
                else
                {
                    Debug.LogError($"Failed to load prefab with key: {passivePath}");
                }
            };
        }
        
    }

}