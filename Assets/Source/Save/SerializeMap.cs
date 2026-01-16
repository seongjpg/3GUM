using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SerializeMap : MonoBehaviour {
	public int[,] tileArr;
	public int xLen;
	public int yLen;
	
	public string sceneName;

	private void Update()
	{
		/*
		if (Input.GetKeyDown("w"))
		{
			Debug.Log("json 직렬화(map)");
			SaveMapData();
		}
		if (Input.GetKeyDown("r"))
		{
			Debug.Log("json 직렬화 파일 해제(map)");
			LoadMapData();
		}
		*/
	}


	public void SaveMapData() // 외부에서 json 파일 변환을 필요로 할 때 호출
	{
		GameObject mapGen = GameObject.FindWithTag("MapGen");
		MapGenerator mg = mapGen.GetComponent<MapGenerator>();
		xLen = mg.mapSize.x; yLen = mg.mapSize.y;
		tileArr = new int[xLen, yLen];
		MakeArr(mg, xLen, yLen);
		//더 저장할 요소가 필요한 경우 여기에 반영

		SerializeM mapJsonData = new SerializeM(tileArr, xLen, yLen);
		string mapJson = ObjectToJson(mapJsonData);
		CreateJsonFile($"{Application.persistentDataPath}/", "map", mapJson);
	}

	public void ClearMapData() // 맵 json 파일을 초기화할 때 호출
	{
		xLen = 0; yLen = 0;
		tileArr = new int[xLen, yLen];

        SerializeM mapJsonData = new SerializeM(tileArr, xLen, yLen);
        string mapJson = ObjectToJson(mapJsonData);
        CreateJsonFile($"{Application.persistentDataPath}/", "map", mapJson);
    }

	public void LoadMapData()
	{
		var mapJson = LoadJsonFile<SerializeM>($"{Application.persistentDataPath}/", "map");
		mapJson.ApplyMap();
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


	void MakeArr(MapGenerator mg, int x, int y)
	{
		/*
		Tilemap tileMap = mg.tileMap;
		tileArr = new int[x, y];
		for (int i = 0; i < x; i++)
		{
			for (int j = 0; j < y; j++)
			{
				Vector3Int tilePos = new Vector3Int(i, j);
				TileBase tile = tileMap.GetTile(tilePos);
				switch (tile.name)
				{
					case "metal_0": // roomTile 의 타일 이름

						tileArr[i, j] = 1;
						break;
					case "wallTile": //wallTile의 타일 이름
						tileArr[i, j] = 2;
						break;
					case "black_metal_0": // outTile의 타일 이름
						tileArr[i, j] = 3;
						break;
				}
			}
		}
		*/
	}

}



[System.Serializable]
public class SerializeM {
	public int[] flatTiles;
	public string sceneName;
	public int xLen;
	public int yLen;

	/*
	*/

	public SerializeM()
	{

	}

	public SerializeM(int[,] _tileArr, int _xLen, int _yLen)
	{
		this.xLen = _xLen;
		this.yLen = _yLen;
		flatTiles = new int[xLen * yLen];
		for (int x = 0; x < xLen; x++)
		{
			for (int y = 0; y < yLen; y++)
			{
				flatTiles[y * xLen + x] = _tileArr[x, y];
			}
		}
		//추가할 항목이 생기면 여기에서 추가.
	}

	public void ApplyMap()
	{
		GameObject mapGen = GameObject.FindWithTag("MapGen");
		MapGenerator mg = mapGen.GetComponent<MapGenerator>();
		int[,] tileArr = To2DArr();
		Debug.Log(tileArr[0, 0]);
		for (int i = 0; i < xLen; i++)
		{
			for (int j = 0; j < yLen; j++)
			{
				switch(tileArr[i, j])
				{
					case 1:
						mg.tileMap.SetTile(new Vector3Int(i, j), mg.roomTile);
						break;
					case 2:
						mg.tileMap.SetTile(new Vector3Int(i, j), mg.wallTile);
						break;
					case 3:
						mg.tileMap.SetTile(new Vector3Int(i, j), mg.outTile);
						break;
				}
			}
		}
		mg.tileMap.SetTile(new Vector3Int(-1, -1), mg.roomTile);
	}

	public int[,] To2DArr()
	{
		int[,] arr = new int[xLen, yLen];
		for (int x = 0; x < xLen; x++)
		{
			for (int y = 0; y < yLen; y++)
			{
				arr[x, y] = flatTiles[y * xLen + x];
			}
		}
		return arr;
	}
}