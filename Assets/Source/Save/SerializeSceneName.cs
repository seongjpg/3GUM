using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SerializeSceneName : MonoBehaviour
{
    private GameObject gameManager;
    private GameManager GM;

    private string sceneName;


    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameManager");
        GM = gameManager.GetComponent<GameManager>();

        LoadSceneName();
        GM.SetSceneName(sceneName);

        SaveSceneName();
    }
    private void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            Debug.Log("json 직렬화");
            
        }
        if (Input.GetKeyDown("o"))
        {
            Debug.Log("json 직렬화 파일 해제");
        }
    }


    void SaveSceneName() // 외부에서 json 파일 변환을 필요로 할 때 호출
    {
        sceneName = GM.GetSceneName();
        SerializeS sceneNameJsonData = new SerializeS(sceneName);
        string sceneNameJson = ObjectToJson(sceneNameJsonData);
        CreateJsonFile($"{Application.persistentDataPath}/", "SceneName", sceneNameJson);
    }

    void LoadSceneName()
    {
        var sceneNameJson = LoadJsonFile<SerializeS>($"{Application.persistentDataPath}/", "SceneName");

        sceneNameJson.LoadCurSceneName(GM);
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
public class SerializeS
{
    public string sceneName;
    public SerializeS()
    {

    }
    
    public SerializeS(string _sceneName)
    {
        this.sceneName = _sceneName;
    }

    public void LoadCurSceneName(GameManager GM)
    {
        GM.SetSceneName(sceneName);
    }

}