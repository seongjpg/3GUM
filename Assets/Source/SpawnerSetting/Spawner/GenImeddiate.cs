using System.Collections.Generic;
using UnityEngine;


// Interface와 ScriptableObject를 활용하여 만든 것은 결국 CreateAssetMenu를 사용하여, 인스펙터에서 에셋화하여 사용할 수 있게 만들어야 한다.
[CreateAssetMenu(menuName = "Spawner/GenImmediate")]

//genMob을 달고있는 스포너는, 플레이어가 트리거에 감지되는 순간 스포너를 소환한다.
public class GenImmediate : ScriptableObject, IGetArea, ISpawnRealTime
{
    Dictionary<Vector3, GameObject> objDic = new();


    public RoomData GetRoomSize() // 안씀
    {
        RoomData rd = new RoomData();
        return rd;
    }

    public RectInt SetRoom(RectInt rect) // 방의 크기를 정하여 SetLoc에서 활용
    {
        return rect;
    }

    //아이템을 소환하는 부분. 현재는 여러 몬스터가 한꺼번에 소환된다. 이는 수정하여 순차적으로 소환되도록 설정 하면 된다.
    public Dictionary<Vector3, GameObject> SpawnObj(int limit, RectInt room, List<SpanwerPoolAssets.Elem_And_Prop> objPool)
    {
        int amt = Random.Range(limit / 2, limit);
        GameObject player = GameObject.FindWithTag("Player");
        for (int rep = 0; rep < amt; rep++)
        {
            int mom = 0; List<int> sector = new List<int>();
            foreach (SpanwerPoolAssets.Elem_And_Prop eap in objPool)
            {
                mom += eap.prop; // mom은 모든 가중치를 더한 값이 된다.
                sector.Add(mom); // sector의 각 윈소는 objPool에 들어있는 값을 반영하여 설정되었다.
                /*
				 가령 m1, m2, m3가 각각 19, 20, 30의 가중치를 가지면, sector의 각 원소는 19, 39, 69가 된다.
				 */
            }
            int output = Random.Range(0, mom); // output은 0~69까지의 값 중 임의의 값을 갖게 된다.
            for (int i = 0; i < sector.Count; i++)
            {
                /*
				 시간지연 방식이 적용 안된 방식이긴 한데, 아이템의 경우 맵이 생성되는 시기에 오브젝트를 소환하는 것이므로 괜찮다.
				 */

                if (output <= sector[i]) // output이 만일 30이였다면, 19보다는 크고, 39보다는 작거나 같기에, m2가 소환된다.
                {
                    float time = 0f;
                    float spawnCycle = 1;
                    while (time <= spawnCycle)
                    {
                        time += Time.deltaTime;
                    }
                    time = 0f;
                    Vector3 summonPos = SetLoc(room, player.transform.position);
                    if (!objDic.ContainsKey(summonPos))
                    {
                        GameObject obj = Instantiate(objPool[i].elem, summonPos, Quaternion.identity);
                        objDic[summonPos] = obj;
                        //Debug.Log(objDic[summonPos], objDic[summonPos]);
                    }
                    //맵에 존재하는 몬스터 카운터에 반영해야 한다
                }
            }
            
        }
        return objDic;
    }



    //SetLoc에서는 room의 값을 이용하여, player의 위치에서 일정 거리 떨어진 위치에서만 몬스터가 소환될 수 있도록 위치를 랜덤으로 지정한다.
    public Vector3 SetLoc(RectInt room, Vector3 playerPos)
    {
        Vector3 loc = new Vector3(Random.Range(room.x, room.x + room.width) + 0.5f, Random.Range(room.y, room.y + room.height) + 0.5f);
        while (Vector3.Distance(loc, playerPos) < 5.0f)
        {
            loc = new Vector3(Random.Range(room.x, room.x + room.width)+0.5f, Random.Range(room.y, room.y + room.height)+0.5f);
        }
        //Debug.Log($"소환 위치 : {room.x}, {room.y}, {room.width}, {room.height}");
        return loc;
    }
}