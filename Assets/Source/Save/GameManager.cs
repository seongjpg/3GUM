using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class GameManager : MonoBehaviour
{


    [SerializeField] private SerializePlayer SP;
    [SerializeField] private SerializeObject SO;
    [SerializeField] private SerializeMap SM;
    [SerializeField] private GlobalSceneControl SCTRL;
    [SerializeField] private SerializeSceneName SSN;

    public string playablePath;
    
    public string weaponPath;
    public string activePath;
    public string oneTimePath;
    public List<string> passivePath = new List<string>();


    void Awake()
    {
    }

    private void Start()
    {
        //오브젝트 제작 시에는 꺼주고 해야 함.
        //LoadFromSaveFile();
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



    public void LoadFromInitSaveFile() // 초기 세이브 파일 사용. 이걸 사용 후 SaveInGame으로 저장을 해주어야 한다. 이거는 인게임 내에서 초기화를 하고 싶을 때에도 사용 가능.
    {
        SP.LoadInit();
        SCTRL.SaveScene();
        SP.SavePlayerData();
        SaveInGame();
    }
    public void LoadFromSaveFile() // 세이브 파일을 로드할 때 사용.
    {
        //SCTRL.LoadScene();
        //씬 이름 불러오기, 씬 로드
        SO.LoadObjectData();

        SP.LoadFromSaveData();
        SaveInGame();
    }
    public void SaveInGame() // 인게임에서 저장이 일어날 때(ex-게임 강제종료시, pause버튼 입력시, 아이템 먹을 때마다, 방 진입시 등등...)
    {
        SO.LoadSceneObject();
        SO.SaveObjectData();

        SP.SavePlayerData();
        SSN.SaveSceneName();
    }
    public void LoadNextScene() // 다음 씬으로 넘어갈 때 사용. 여기선 플레이어의 위치는 필요없고, 씬의 이름도 바뀌어야 한다. 그 뒤에 SaveInGame적용.
    {
        SP.LoadNextScene();
        SaveInGame();
    }
}
