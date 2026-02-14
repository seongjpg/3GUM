using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class SerializeMap : MonoBehaviour {
   
	// 타일 ↔ 인덱스 매핑을 딕셔너리를 사용하도록 변경 
    private Dictionary<TileBase, int> tileToIndex = new Dictionary<TileBase, int>();
    private Dictionary<int, TileBase> indexToTile = new Dictionary<int, TileBase>();
    
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


    public void SaveMapData()
    {
        GameObject mapGen = GameObject.FindWithTag("MapGen");
        MapGenerator mg = mapGen.GetComponent<MapGenerator>();
        xLen = mg.mapSize.x; yLen = mg.mapSize.y;
        tileArr = new int[xLen, yLen];
        MakeArr(mg, xLen, yLen);

        // tileToIndex 매핑도 함께 전달
        SerializeM mapJsonData = new SerializeM(tileArr, xLen, yLen, tileToIndex);
        string mapJson = ObjectToJson(mapJsonData);
        CreateJsonFile($"{Application.persistentDataPath}/", "map", mapJson);
    }

    public void ClearMapData() // 맵 json 파일을 초기화할 때 호출
	{
		xLen = 0; yLen = 0;
		tileArr = new int[xLen, yLen];

        SerializeM mapJsonData = new SerializeM(tileArr, xLen, yLen, tileToIndex);
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


    private void BuildTileMapping(Tilemap tileMap)
    {
        tileToIndex.Clear();
        indexToTile.Clear();

        // 타일맵에 사용된 모든 고유 타일 수집
        List<TileBase> uniqueTiles = new List<TileBase>();

        BoundsInt bounds = tileMap.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            TileBase tile = tileMap.GetTile(pos);
            if (tile != null && !tileToIndex.ContainsKey(tile))
            {
                int index = uniqueTiles.Count + 1; // 0은 빈 타일용으로 예약
                tileToIndex[tile] = index;
                indexToTile[index] = tile;
                uniqueTiles.Add(tile);
            }
        }

        Debug.Log($"타일 매핑 완료: {uniqueTiles.Count}종류 감지");
        foreach (var pair in tileToIndex)
        {
            Debug.Log($"  [{pair.Value}] {pair.Key.name}");
        }
    }

    void MakeArr(MapGenerator mg, int x, int y)
    {
        Tilemap tileMap = mg.tileMap;
        BuildTileMapping(tileMap); // 매핑 자동 생성

        tileArr = new int[x, y];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                Vector3Int tilePos = new Vector3Int(i, j);
                TileBase tile = tileMap.GetTile(tilePos);

                if (tile != null && tileToIndex.ContainsKey(tile))
                    tileArr[i, j] = tileToIndex[tile];
                else
                    tileArr[i, j] = 0; // 빈 타일
            }
        }
    }

}



[System.Serializable]
public class TileMapping
{
    public int index;
    public string tileName;
}

[System.Serializable]
public class SerializeM
{
    public int[] flatTiles;
    public int xLen;
    public int yLen;
    public List<TileMapping> tileMappings = new List<TileMapping>(); // 추가

    public SerializeM() { }

    public SerializeM(int[,] _tileArr, int _xLen, int _yLen, Dictionary<TileBase, int> tileToIndex)
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

        // 매핑 정보 저장
        foreach (var pair in tileToIndex)
        {
            tileMappings.Add(new TileMapping
            {
                index = pair.Value,
                tileName = pair.Key.name
            });
        }
    }

    public void ApplyMap()
    {
        Debug.Log("세이브파일 맵 데이터 로드!");
        GameObject mapGen = GameObject.FindWithTag("MapGen");
        MapGenerator mg = mapGen.GetComponent<MapGenerator>();

        // MapGenerator에 타일 목록을 배열이나 리스트로 등록해둔다고 가정
        Dictionary<string, TileBase> nameToTile = new Dictionary<string, TileBase>();
        foreach (TileBase tile in mg.allTiles)
        {
            nameToTile[tile.name] = tile;
        }

        int[,] tileArr = To2DArr();
        for (int i = 0; i < xLen; i++)
        {
            for (int j = 0; j < yLen; j++)
            {
                int index = tileArr[i, j];
                if (index == 0) continue;

                // 저장된 매핑에서 타일 이름 찾기
                TileMapping mapping = tileMappings.Find(m => m.index == index);
                if (mapping != null && nameToTile.ContainsKey(mapping.tileName))
                {
                    mg.tileMap.SetTile(new Vector3Int(i, j), nameToTile[mapping.tileName]);
                }
            }
        }
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