using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{	
	[SerializeField] public MapCenter mc; // 각 방의 정보를 관리하는 스크립트.스포너가 맵에서 몬스터를 스폰하는 위치를 정할 수 있도록 하고, 활성화하는 역할 담당.
	public LayerMask obstacle;

	[SerializeField] public Vector2Int mapSize;
	[SerializeField] float minimumDevideRate; //공간이 나눠지는 최소 비율
	[SerializeField] float maximumDivideRate; //공간이 나눠지는 최대 비율
	[SerializeField] private GameObject line; //lineRenderer를 사용해서 공간이 나눠진걸 시작적으로 보여주기 위함
	[SerializeField] private GameObject map; //lineRenderer를 사용해서 첫 맵의 사이즈를 보여주기 위함
	[SerializeField] private GameObject roomLine; //lineRenderer를 사용해서 방의 사이즈를 보여주기 위함
	[SerializeField] private int maximumDepth; //트리의 깊이, 깊을수록 방을 더 자세히 나누게 됨

	[SerializeField] public Tilemap tileMap;
	[SerializeField] public Tile roomTile; //방을 구성하는 타일
	[SerializeField] public Tile roadTile;

    [SerializeField] public Tilemap wallTileMap;
    [SerializeField] public Tile wallTile; //방과 외부를 구분지어줄 벽 타일
	[SerializeField] public Tile upWall;
	[SerializeField] public Tile downWall;
	[SerializeField] public Tile rightWall;
	[SerializeField] public Tile leftWall;

	[SerializeField] public Tile edgeUR;
	[SerializeField] public Tile edgeUL;
	[SerializeField] public Tile edgeDR;
	[SerializeField] public Tile edgeDL;

    [SerializeField] public Tile innerEdgeUR;
    [SerializeField] public Tile innerEdgeUL;
    [SerializeField] public Tile innerEdgeDR;
    [SerializeField] public Tile innerEdgeDL;

    [SerializeField] public Tile outTile; //방 외부의 타일
	
	[SerializeField] GameObject bossGate;
	
	[SerializeField] GameObject ms; // 몬스터 스포너를 부착하는 부분. 추후에 여러 몬스터 스포너를 적용하여, 각 방마다 다른 몬스터 풀을 적용할 수 있도록 하는 것이 추후 목표.
	[SerializeField] GameObject itemSpawn; // 아이템 스포너 부착하는 부분. 여기도 몬스터 스포너처럼 여러 몬스터 풀 중 임의로 골라서 넣을지는 모르겠음
	[SerializeField] GameObject objSpawn; // 오브젝트 스포너 부착하는 부분. 기본적인 소환 시기는 아이템스포너와 동일하나, 아이템 스포너와 달리 규칙이 필요하여 소환 로직을 고민해봐야 함.

	private GameObject player;
	public Vector3 playerLoc = new Vector3(0, 0);
	public int seed; // 시드 적용을 위해 일단 작성


	private List<Node> nodes = new List<Node>();
	private List<RectInt> rooms = new List<RectInt>();

	private List<Vector2Int[]> centers = new List<Vector2Int[]>() { };

	public Dictionary<Vector3, GameObject> obstacleDic = new Dictionary<Vector3, GameObject>() { };

	private ObjectSpawner oSpawner;
	private bool destroyRoadExecuted = false;


	public int cnt = 1;

	private void Start()
	{
		SpawnPrefab();
	}

    private void Update()
    {
        if (oSpawner == null && !destroyRoadExecuted)
		{
			Debug.Log($"DestroyRoad실행, {centers.Count}, {obstacleDic.Count}");
			DestroyRoad();
			foreach (Vector3 key in obstacleDic.Keys)
			{
				Destroy(obstacleDic[key]);
			}
			centers = null;
			obstacleDic = null;
			destroyRoadExecuted = true;
		}
    }


    public void SpawnPrefab()
    {
		player = GameObject.FindWithTag("Player");
		FillBackground();//씬 로드 시 전부다 바깥타일로 덮음
		Node root = new Node(new RectInt(0, 0, mapSize.x, mapSize.y));
		Divide(root, 0);
		GenerateRoom(root, 0); Debug.Log($"roomdata 개수 : {mc.roomData.Count}");
		//GenerateLoad(root, 0);
		//FillWall(); //바깥과 방이 만나는 지점을 벽으로 칠해주는 함수

		int id = Random.Range(0, rooms.Count);
		Debug.Log(id);
		playerLoc = tileMap.GetCellCenterWorld(new Vector3Int(rooms[id].x + rooms[id].width/2,  rooms[id].y + rooms[id].height/2, 0));
		player.GetComponent<PlayerMovement>().transform.position = playerLoc;
		//플레이어 위치를 기반으로 보스룸 설정
		MakeBossRoom();
		//보스룸을 제외한 방을 특수 방으로 만들 여지 남겨놓기
		this.gameObject.transform.position = Vector3.zero;
	}

	void Divide(Node tree, int n)
	{
		if (n == maximumDepth)
		{
			nodes.Add(tree); // 원하는 깊이에 도달했을 경우 현재 노드를 노드 리스트에 저장(이는 보스룸 지정할 때 사용)
			//playerLoc = new Vector3((tree.nodeRect.x) / 2, (tree.nodeRect.y) / 2);

			return; //내가 원하는 깊이에 도달하면 더 나눠주지 않는다.
		}


		//그 외의 경우에는
		int maxLength = Mathf.Max(tree.nodeRect.width, tree.nodeRect.height);
		//가로와 세로중 더 긴것을 구한후, 가로가 길다면 위 좌, 우로 세로가 더 길다면 위, 아래로 나눠주게 될 것이다.
		int split = Mathf.RoundToInt(Random.Range(maxLength * minimumDevideRate, maxLength * maximumDivideRate));
		//나올 수 있는 최대 길이와 최소 길이중에서 랜덤으로 한 값을 선택
		if (tree.nodeRect.width >= tree.nodeRect.height) //가로가 더 길었던 경우에는 좌 우로 나누게 될 것이며, 이 경우에는 세로 길이는 변하지 않는다.
		{

			tree.leftNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y, split, tree.nodeRect.height));
			//왼쪽 노드에 대한 정보다 
			//위치는 좌측 하단 기준이므로 변하지 않으며, 가로 길이는 위에서 구한 랜덤값을 넣어준다.
			tree.rightNode = new Node(new RectInt(tree.nodeRect.x + split, tree.nodeRect.y, tree.nodeRect.width - split, tree.nodeRect.height));
			//우측 노드에 대한 정보다 
			//위치는 좌측 하단에서 오른쪽으로 가로 길이만큼 이동한 위치이며, 가로 길이는 기존 가로길이에서 새로 구한 가로값을 뺀 나머지 부분이 된다. 
		}
		else
		{
			tree.leftNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y, tree.nodeRect.width, split));
			tree.rightNode = new Node(new RectInt(tree.nodeRect.x, tree.nodeRect.y + split, tree.nodeRect.width, tree.nodeRect.height - split));
		}
		tree.leftNode.parNode = tree; //자식노드들의 부모노드를 나누기전 노드로 설정
		tree.rightNode.parNode = tree;
		Divide(tree.leftNode, n + 1); //왼쪽, 오른쪽 자식 노드들도 나눠준다.
		Divide(tree.rightNode, n + 1);//왼쪽, 오른쪽 자식 노드들도 나눠준다.
	}
	private RectInt GenerateRoom(Node tree, int n)
	{
		RectInt rect;
		if (n == maximumDepth) //해당 노드가 리프노드라면 방을 만들어 줄 것이다.
		{
			
			rect = tree.nodeRect;
			int width = Random.Range(rect.width / 2, rect.width - 1);
			//방의 가로 최소 크기는 노드의 가로길이의 절반, 최대 크기는 가로길이보다 1 작게 설정한 후 그 사이 값중 랜덤한 값을 구해준다.
			int height = Random.Range(rect.height / 2, rect.height - 1);
			//높이도 위와 같다.
			int x = rect.x + Random.Range(1, rect.width - width);
			//방의 x좌표이다. 만약 0이 된다면 붙어 있는 방과 합쳐지기 때문에,최솟값은 1로 해주고, 최댓값은 기존 노드의 가로에서 방의 가로길이를 빼 준 값이다.
			int y = rect.y + Random.Range(1, rect.height - height);
			//y좌표도 위와 같다.
			rect = new RectInt(x, y, width, height);
			rooms.Add(rect);
			FillRoom(rect);
		}
		else
		{
            tree.leftNode.roomRect = GenerateRoom(tree.leftNode, n + 1);
			tree.rightNode.roomRect = GenerateRoom(tree.rightNode, n + 1);
            Vector2Int[] arr = { tree.leftNode.center, tree.rightNode.center };
            centers.Add(arr);
            GenRoad(tree.leftNode.center, tree.rightNode.center); // maxDepth 전에는 GenLoad로 길을 이어준다.
            rect = tree.leftNode.roomRect;
		}
		return rect;
	}

	private void GenRoad(Vector2Int left, Vector2Int right) // 새로 제작하는 길 내는 스크립트
	{
		for (int i = Mathf.Min(left.x, right.x); i <= Mathf.Max(left.x, right.x); i++)
		{
			if (tileMap.GetTile(new Vector3Int(i, left.y)) != roomTile)
			{
                //tileMap.SetTile(new Vector3Int(i, left.y), roadTile);
				wallTileMap.SetTile(new Vector3Int(i, left.y), null);

                TileBase downSide = wallTileMap.GetTile(new Vector3Int(i, left.y - 1));
				TileBase upSide = wallTileMap.GetTile(new Vector3Int(i, left.y + 1));
				if (downSide == outTile)
				{

                    wallTileMap.SetTile(new Vector3Int(i, left.y - 1), downWall);
                }
				else if(downSide == rightWall) {
                    wallTileMap.SetTile(new Vector3Int(i, left.y - 1), innerEdgeDR);
				}
				else if (downSide == leftWall)
				{
                    wallTileMap.SetTile(new Vector3Int(i, left.y - 1), innerEdgeDL);
				}
                if (upSide == outTile)
                {

                    wallTileMap.SetTile(new Vector3Int(i, left.y + 1), upWall);
                }
                else if (upSide == rightWall)
                {
                    wallTileMap.SetTile(new Vector3Int(i, left.y + 1), innerEdgeUR);
                }
                else if (upSide == leftWall)
                {
                    wallTileMap.SetTile(new Vector3Int(i, left.y + 1), innerEdgeUL);
                }
            }
		}

		for (int j = Mathf.Min(left.y, right.y); j <= Mathf.Max(left.y, right.y); j++)
		{
            if (tileMap.GetTile(new Vector3Int(right.x, j)) != roomTile)
            {
                //tileMap.SetTile(new Vector3Int(right.x, j), roadTile);
                wallTileMap.SetTile(new Vector3Int(right.x, j), null);
                TileBase leftSide = wallTileMap.GetTile(new Vector3Int(right.x - 1, j));
				TileBase rightSide = wallTileMap.GetTile(new Vector3Int(right.x + 1, j));
                if (leftSide == outTile)
                {

                    wallTileMap.SetTile(new Vector3Int(right.x-1, j), leftWall);
                }
                else if (leftSide == downWall)
                {
                    wallTileMap.SetTile(new Vector3Int(right.x - 1, j), innerEdgeDL);
                }
                else if (leftSide == upWall)
                {
                    wallTileMap.SetTile(new Vector3Int(right.x - 1, j), innerEdgeUL);
                }
                if (rightSide == outTile)
                {

                    wallTileMap.SetTile(new Vector3Int(right.x + 1, j), rightWall);
                }
                else if (rightSide == downWall)
                {
                    wallTileMap.SetTile(new Vector3Int(right.x + 1, j), innerEdgeDR);
                }
                else if (rightSide == upWall)
                {
                    wallTileMap.SetTile(new Vector3Int(right.x + 1, j), innerEdgeUR);
                }
            }
		}
	}
    
    private void DestroyRoad() // 새로 제작하는 길 내는 스크립트
    {
        for (int k = 0; k < centers.Count; k++)
        {
            Vector2Int left = centers[k][0]; Vector2Int right = centers[k][1];

            for (int i = Mathf.Min(left.x, right.x); i <= Mathf.Max(left.x, right.x); i++)
            {
				if (obstacleDic.ContainsKey(new Vector3(i, left.y)))
				{
					Destroy(obstacleDic[new Vector3(i, left.y)]);
					Debug.Log($"{new Vector3(i, left.y)}의 구조물 파괴");
				}
            }

            for (int j = Mathf.Min(left.y, right.y); j <= Mathf.Max(left.y, right.y); j++)
            {
                if (obstacleDic.ContainsKey(new Vector3(right.x, j)))
                {
                    Destroy(obstacleDic[new Vector3(right.x, j)]);
                    Debug.Log($"{new Vector3(right.x, j)}의 구조물 파괴");
                }
            }
        }
    }

    void FillBackground() //배경을 채우는 함수, 씬 load시 가장 먼저 해준다.
	{
		for (int i = -10; i < mapSize.x + 10; i++) //바깥타일은 맵 가장자리에 가도 어색하지 않게
												   //맵 크기보다 넓게 채워준다.
		{
			for (int j = -10; j < mapSize.y + 10; j++)
			{
                wallTileMap.SetTile(new Vector3Int(i, j), outTile);
			}
		}
	}
	private void FillRoom(RectInt rect)
	{ //room의 rect정보를 받아서 tile을 set해주는 함수
		for (int i = rect.x; i < rect.x + rect.width; i++)
		{
			for (int j = rect.y; j < rect.y + rect.height; j++)
			{
				//tileMap.SetTile(new Vector3Int(i, j), roomTile);
                wallTileMap.SetTile(new Vector3Int(i, j), null);
            }
		}
		//벽 설정 스크립트(룸 생성과 동시에 생성되게끔 변경. 각 rect의 정보를 이용해 벽을 설치해주는 것으로 변경.)
		for (int i = rect.x-1; i <= rect.x + rect.width; i++)
		{
            wallTileMap.SetTile(new Vector3Int(i, rect.y + rect.height), upWall);
            wallTileMap.SetTile(new Vector3Int(i, rect.y - 1), downWall);
		}
		for (int j = rect.y-1; j <= rect.y + rect.height; j++)
		{
            wallTileMap.SetTile(new Vector3Int(rect.x + rect.width, j), rightWall);
            wallTileMap.SetTile(new Vector3Int(rect.x - 1, j), leftWall);
        }
        wallTileMap.SetTile(new Vector3Int(rect.x - 1, rect.y - 1), edgeDL);
        wallTileMap.SetTile(new Vector3Int(rect.x - 1, rect.y + rect.height), edgeUL);
        wallTileMap.SetTile(new Vector3Int(rect.x + rect.width, rect.y - 1), edgeDR);
        wallTileMap.SetTile(new Vector3Int(rect.x + rect.width, rect.y + rect.height), edgeUR);


		//FillRoom 함수에서 방을 만들면서, 동시에 플레이어의 방 입장 여부를 판별할 수 있는 박스콜라이더를 설치한다.
		//여기서 collider를 방 크기에 맞게 동적으로 생성하여 MapCenter에 저장한다.
		Vector3 offset = tileMap.GetCellCenterWorld(new Vector3Int(rect.x + rect.width/2, rect.y + rect.height/2)); // 타일맵 생성은 왼쪽 아랫쪽 좌표를 기준으로 생성되지만, 콜라이더는 그렇지 않으므로 고려하여야 한다.
		Vector2 size = new Vector2(rect.width, rect.height);
		BoxCollider2D bx = this.AddComponent<BoxCollider2D>();
		bx.offset = new Vector2(offset.x, offset.y);
		bx.size = size;

		bx.isTrigger = true;
		
		Transform tr = bx.transform;
		tr.position = offset;
		//Debug.Log($"박스 콜라이더의 오프셋 : {tr.position}");
		//FillRoom 함수에서 방을 만들며, 동시에 몬스터가 스폰되는 스포너와, 아이템 스포너를 생성한다.
		GameObject spawn = GameObject.Instantiate(ms, tr.position, Quaternion.identity);
		GameObject iSpawn = GameObject.Instantiate(itemSpawn, tr.position, Quaternion.identity);
		GameObject oSpawn = GameObject.Instantiate(objSpawn, tr.position, Quaternion.identity);

		MobSpawner spawner = spawn.GetComponent<MobSpawner>(); // 스포너에 부착된 MobSpawner 함수를 GetComponent를 활용하여 가져온다.
		ItemSpawner iSpawner = iSpawn.GetComponent<ItemSpawner>();
		oSpawner = oSpawn.GetComponent<ObjectSpawner>();


		spawner.SetRoom(rect); // MobSpawner에 있는 SetRoom 함수를 실행하여 room 값을 넣어준다.
		iSpawner.SetRoom(rect);
		oSpawner.SetRoom(rect);

		//오브젝트 스포너에서 소환한 장애물들의 위치와 정보를 받아야 한다.

        mc.roomData.Add(new RoomData(rect, bx, spawn)); // 활성화 할 몬스터 스포너를 판별하기 위한 roomData를 모아둔 List(mc)에 rect와, box collider2d, spawn을 엮은 struct를 add.

	}

	private void MakeBossRoom()
	{
		if (bossGate != null) // 보스게이트가 설정되지 않은 경우가 있음(보스룸은 그렇다)
		{

            RectInt farRoom = rooms[0];
            float lengthToBoss = new Vector3(Mathf.Abs(farRoom.center.x - playerLoc.x), Mathf.Abs(farRoom.center.y - playerLoc.y)).magnitude;
            foreach (RectInt rm in rooms)
            {
                float pToRm = new Vector3(Mathf.Abs(rm.center.x - playerLoc.x), Mathf.Abs(rm.center.y - playerLoc.y)).magnitude;
                if (lengthToBoss < pToRm)
                {
                    lengthToBoss = pToRm;
                    farRoom = rm;
                }
            }
            Vector3 bossRoomPos = tileMap.GetCellCenterWorld(new Vector3Int(farRoom.x + farRoom.width / 2, farRoom.y + farRoom.height / 2, 0));
            Debug.Log($"가장 먼 노드의 센터 : {bossRoomPos.x}, {bossRoomPos.y}");
            GameObject.Instantiate(bossGate, bossRoomPos, Quaternion.identity);
        }
	}


}
