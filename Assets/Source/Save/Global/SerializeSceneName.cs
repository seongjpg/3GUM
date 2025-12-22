using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SerializeSceneName : MonoBehaviour
{
    [SerializeField] private GameObject globalMaanger;
    private GlobalAmt GA;

    private string sceneName01;


    private void Awake()
    {
        GA = globalMaanger.GetComponent<GlobalAmt>();

    }

    private void Update()
    {
        if (Input.GetKeyDown("u"))
            LoadSceneName();
    }

    public void SaveSceneName() // 외부에서 json 파일 변환을 필요로 할 때 호출
    {
        sceneName01 = GA.GetSceneName01();
        Debug.Log($"씬 이름 저장 - {sceneName01}");
        SerializeS sceneNameJsonData = new SerializeS(sceneName01);
        string sceneNameJson = ObjectToJson(sceneNameJsonData);
        CreateJsonFile($"{Application.persistentDataPath}/", "SceneName01", sceneNameJson);
    }

    public void LoadInitSceneName()
    {
        var sceneNameJson = LoadJsonFile<SerializeS>($"{Application.persistentDataPath}/", "InitSceneName");

        sceneNameJson.LoadCurSceneName(GA);
    }

    public void LoadSceneName()
    {
        var sceneNameJson = LoadJsonFile<SerializeS>($"{Application.persistentDataPath}/", "SceneName01");

        sceneNameJson.LoadCurSceneName(GA);

        Debug.Log($"{sceneName01} 불러오기!");
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
    public string sceneName01;
    public SerializeS()
    {

    }
    
    public SerializeS(string _sceneName)
    {
        this.sceneName01 = _sceneName;
    }

    public void LoadCurSceneName(GlobalAmt GA)
    {
        GA.SetSceneName01(sceneName01);
    }

}