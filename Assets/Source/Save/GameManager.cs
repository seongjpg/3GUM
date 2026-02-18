using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject mapGenerator;
    private GameObject GlobalManager;
    private GameObject sceneManager;
    private GlobalAmt GA;
    private MapGenerator MG;

    private bool generateMap = true;


    [SerializeField] private SerializePlayer SP;
    [SerializeField] private SerializeObject SO;
    [SerializeField] private SerializeMap SM;
    [SerializeField] private GlobalSceneControl GSC;
    [SerializeField] private SerializeSceneName SSN;

    public string playablePath;
    
    public string weaponPath;
    public string activePath;
    public string oneTimePath;
    public List<string> passivePath = new List<string>();


    void Awake()
    {
        mapGenerator = GameObject.FindWithTag("MapGen");
        GlobalManager = GameObject.Find("GlobalManager");
        sceneManager = GameObject.Find("SceneManager");
        MG = mapGenerator.GetComponent<MapGenerator>();
        GA = GlobalAmt.Instance;
        GSC = sceneManager.GetComponent<GlobalSceneControl>();
        SSN = sceneManager.GetComponent<SerializeSceneName>();
    }

    private void Start()
    {
        Debug.Log($"GameManager Start - isInit: {GA.isInitFile01}, goToNext: {GA.goToNextScene}");
        //오브젝트 제작 시에는 꺼주고 해야 함.
        if (GA.isInitFile01)
        {
            GA.isInitFile01 = false;
            LoadFromInitSaveFile();
        }
        else
        {
            if (GA.goToNextScene) // 새로운 스테이지 -> 맵은 생성해야 함. GA.gotoNextScene == true; generateMap = false;
            {
                LoadNextScene();
            }
            else  // 로드 -> GA.gotoNextScene == false, generateMap = false;
            {
                LoadFromSaveFile(); 
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            LoadFromInitSaveFile();
        }
    }


    public void SetPlayablePath(GameObject player)
    {
        playablePath = player.GetComponent<AddressableTag>().AddressKey;
    }
    public string GetPlayablePath()
    {
        return playablePath;
    }
    public void SetWeaponPath(GameObject weapon) 
    {
        weaponPath= weapon.GetComponent<AddressableTag>().AddressKey;
    }
    public string GetWeaponPath()
    {
        return weaponPath;
    }

    public void SetActivePath(GameObject active)
    {
        activePath = active.GetComponent<AddressableTag>().AddressKey;
    }
    public string GetActivePath()
    {
        return activePath;
    }

    public void SetoneTimePath(GameObject oneTime)
    {
        oneTimePath = oneTime.GetComponent<AddressableTag>().AddressKey;
    }
    public string GetoneTImePath()
    {
        return oneTimePath;
    }
    public void RemoveOneTimePath()
    {
        oneTimePath = null;
    }

    public void SetPassivePath(GameObject passive)
    {
            passivePath.Add(passive.GetComponent<AddressableTag>().AddressKey);        
    }
    public List<string> GetPassivePath()
    {
        return passivePath;
    }



    public void LoadFromInitSaveFile() // 초기화 후 로드. 이거는 인게임 내에서 초기화를 하고 싶을 때에도 사용 가능.
    {
        Debug.Log("초기화파일 사용");
        generateMap = true;
        SM.ClearMapData(); // 맵 데이터 초기화
        SO.ClearObjectData(); // 오브젝트 데이터 초기화
        SP.ClearPlayerData(); // 플레이어 데이터 초기화
       
        SaveInGame();
    }
    public void LoadFromSaveFile() // 세이브 파일을 로드할 때 사용.
    {
        Debug.Log("세이브 파일 불러오기");
        generateMap = false;
        GA.goToNextScene = false;
        //씬 이름 불러오기, 씬 로드
        SM.LoadMapData();
        SO.LoadObjectData(); // 여기서 기존 장애물 파괴도 동시에 진행해주어야 한다.
        SP.LoadFromSaveData();

        SaveInGame();
    }
    public void LoadNextScene() // 다음 씬에 진입했을 때 사용. 여기선 플레이어의 위치는 필요없고, 씬의 이름도 바뀌어야 한다. 그 뒤에 SaveInGame적용.
    {
        Debug.Log("다음 씬 진입");
        generateMap = true;
        GA.goToNextScene = true;
        SP.LoadFromSaveData();

        SaveInGame();
    }
    public void SaveInGame() // 인게임에서 저장이 일어날 때(ex-게임 강제종료시, pause버튼 입력시, 아이템 먹을 때마다, 방 진입시 등등...)
    {
        SM.SaveMapData();
        SO.LoadObjSave();
        SP.SavePlayerData();
        GSC.SaveScene(SceneManager.GetActiveScene().name);
    }

    public void SaveForNextScene() // 다음 씬으로 넘어갈 때 호출되는 함수
    {
        generateMap = true;
        GA.goToNextScene = true;
        SP.SavePlayerData(); // 어차피 로드할 때 pos를 제외하고 로드하기에 따로 세이브를 할 필요는 없음.
        SO.ClearObjectData();
    }
    
    public bool ShouldGenerateMap()
    {
        bool rtn = generateMap || GA.goToNextScene;
        GA.goToNextScene = false;
        Debug.Log($"rtn : {rtn}, generateMap : {generateMap}, GA.gotoNextScene : {GA.goToNextScene}");
        return rtn;
    }

    public void ChangeGenerateMap()
    {
        generateMap = false;
    }

}
