using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


[System.Serializable]
public struct Object {
    public string prefabPath;
    public Vector3 position;
}


public class SerializeObject : MonoBehaviour
{
	private int objectLayer = 6; // 리펙토링 시 레이어 번호 수정 가능성 높음
    public List<Object> objects = new List<Object>() { };



	private void Update()
	{
		if (Input.GetKeyDown("w"))
		{
			Debug.Log("json 직렬화(오브젝트)");
			LoadSceneObject(); // objects 리스트 채우기
			SaveObjectData();
		}
		if (Input.GetKeyDown("r"))
		{
			Debug.Log("json 직렬화 파일 해제(오브젝트)");
			LoadObjectData();
		}
	}


	void SaveObjectData() // 외부에서 json 파일 변환을 필요로 할 때 호출
	{
		SerializeObj objectJsonData = new SerializeObj(objects);
		string objectJson = ObjectToJson(objectJsonData);
		CreateJsonFile($"{Application.persistentDataPath}/", "objects", objectJson);
	}

	void LoadObjectData()
	{
		var objectJson = LoadJsonFile<SerializeObj>($"{Application.persistentDataPath}/", "objects");
		objectJson.SummonPrefab();
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

	public void LoadSceneObject()
	{
		List<GameObject> objs = new List<GameObject>();
		foreach (GameObject g in FindObjectsByType<GameObject>(FindObjectsSortMode.None))
		{
			if (g.layer == objectLayer)
			{
				objs.Add(g);
			}
		}
		AddObjectToList(objs);
	}

	public void AddObjectToList(List<GameObject> objs)
	{
		foreach (GameObject g in objs)
		{
			//Debug.Log($"{g.name}");
			Object obj = new Object();
			obj.prefabPath = g.GetComponent<AddressableTag>().AddressKey;
			obj.position = g.transform.position;
			objects.Add(obj);
		}
		//PrintObjects();
	}

	public void PrintObjects()
	{
		foreach(Object obj in objects)
		{
			Debug.Log($"path : {obj.prefabPath}\nposition : {obj.position}\n");
		}
	}
}


[System.Serializable]
public class SerializeObj {

	public List<Object> objects;
	public SerializeObj()
	{

	}

	public SerializeObj(List<Object> _objects)
	{
		this.objects = _objects;
	}

	public async void SummonPrefab()
	{
		foreach (var entry in objects)
		{
			var handle = Addressables.LoadAssetAsync<GameObject>(entry.prefabPath);
			await handle.Task;
			if (handle.Status == AsyncOperationStatus.Succeeded)
				UnityEngine.Object.Instantiate(handle.Result, entry.position, Quaternion.identity);
			else
				Debug.LogError($"Addressables 로드 실패: {entry.prefabPath}");
		}
	}
}