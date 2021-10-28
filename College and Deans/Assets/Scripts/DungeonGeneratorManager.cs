using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class DungeonGeneratorManager : MonoBehaviour
{
    public enum RandomRoomNumberMethod
    {
        Fixed = 0, Random = 1
    };

    [Header("Generation Config")]
    public RandomRoomNumberMethod GenerationMethod; //Generates a fixed number of rooms or a random number of rooms

    public int FixedValue = 8;

    //Bounds of the random method
    public int RandomLowerBound = 5;
    public int RandomUpperBound = 10;

    public Vector2 MoveAmount = new Vector2(22, 22); //Distance between rooms

    //Room prefabs

    [Header("Room List")]
    //1 exit
    public GameObject T_Room;
    public GameObject R_Room;
    public GameObject B_Room;
    public GameObject L_Room;

    //2 exits
    public GameObject TR_Room;
    public GameObject TB_Room;
    public GameObject TL_Room;
    public GameObject RB_Room;
    public GameObject RL_Room;
    public GameObject BL_Room;

    //3 exits
    public GameObject TRB_Room;
    public GameObject TRL_Room;
    public GameObject TBL_Room;
    public GameObject RBL_Room;

    //4 exits
    public GameObject TRBL_Room;
    //public GameObject[] RoomList; //List of rooms for further use

    [Header("Private Serialized Stuff")]
    [SerializeField] private Vector2 currentPos = Vector2.zero;

    [SerializeField] private List<RoomInfo> roomInfoList; //TODO save useful room info for rearrangement
    [SerializeField] private List<GameObject> roomList; //TODO save useful room info for rearrangement
    [SerializeField] private List<Vector2> positions;

    private void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Vector2>();
        roomInfoList = new List<RoomInfo>();
        GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateLevel()
    {
        switch (GenerationMethod)
        {
            case RandomRoomNumberMethod.Fixed:
                GenerateProcLevel(FixedValue);
                break;
            case RandomRoomNumberMethod.Random:
                int random = UnityEngine.Random.Range(RandomLowerBound, RandomUpperBound + 1);
                GenerateProcLevel(random);
                break;
        }

        RearrangeLevel();
    }

    void GenerateProcLevel(int num)
    {
        for (int i = 0; i < num;)
        {
            int rand = UnityEngine.Random.Range(0, 4);

            if (!positions.Contains(currentPos))
            {
                var roomInfo = ScriptableObject.CreateInstance<RoomInfo>();
                if (i == 0)
                {
                    roomInfo.roomType = RoomInfo.RoomType.Spawn;
                }
                else if (i == num - 1)
                {
                    roomInfo.roomType = RoomInfo.RoomType.Boss;
                }
                else
                    SetRandomRoom(roomInfo);

                roomInfo.position = currentPos;
                roomInfoList.Add(roomInfo);
                positions.Add(currentPos);

                i++;
            }

            switch (rand)
            {
                case 0:
                    currentPos += Vector2.up * MoveAmount.y;
                    break;
                case 1:
                    currentPos += Vector2.down * MoveAmount.y;
                    break;
                case 2:
                    currentPos += Vector2.left * MoveAmount.x;
                    break;
                case 3:
                    currentPos += Vector2.right * MoveAmount.x;
                    break;
            }
        }
    }

    void SetRandomRoom(RoomInfo roomInfo)
    {
        int rand = UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(RoomInfo.RoomType)).Length - 2);
        switch (rand)
        {
            case 0:
                roomInfo.roomType = RoomInfo.RoomType.Enemies;
                break;
            case 1:
                roomInfo.roomType = RoomInfo.RoomType.Cafe;
                break;
            case 2:
                roomInfo.roomType = RoomInfo.RoomType.Loot;
                break;
        }
    }

    void RearrangeLevel()
    {
        foreach(var roomInfo in roomInfoList)
        {
            Debug.Log(roomInfo.roomType);
            roomInfo.CheckAdjacentRooms(positions, MoveAmount);
            GameObject room;

            #region 1_exit
            if (roomInfo.adjacentRooms == RoomInfo.AdjacentRooms.North)
            {
                room = Instantiate(T_Room, roomInfo.position, Quaternion.identity);
            }
            else if (roomInfo.adjacentRooms == RoomInfo.AdjacentRooms.East)
            {
                room = Instantiate(R_Room, roomInfo.position, Quaternion.identity);
            }
            else if (roomInfo.adjacentRooms == RoomInfo.AdjacentRooms.South)
            {
                room = Instantiate(B_Room, roomInfo.position, Quaternion.identity);
            }
            else if (roomInfo.adjacentRooms == RoomInfo.AdjacentRooms.West)
            {
                room = Instantiate(L_Room, roomInfo.position, Quaternion.identity);
            }
            #endregion

            #region 2_exits
            else if (roomInfo.adjacentRooms == (RoomInfo.AdjacentRooms.North | RoomInfo.AdjacentRooms.East))
            {
                room = Instantiate(TR_Room, roomInfo.position, Quaternion.identity);
            }
            else if (roomInfo.adjacentRooms == (RoomInfo.AdjacentRooms.North | RoomInfo.AdjacentRooms.South))
            {
                room = Instantiate(TB_Room, roomInfo.position, Quaternion.identity);
            }
            else if (roomInfo.adjacentRooms == (RoomInfo.AdjacentRooms.North | RoomInfo.AdjacentRooms.West))
            {
                room = Instantiate(TL_Room, roomInfo.position, Quaternion.identity);
            }
            else if (roomInfo.adjacentRooms == (RoomInfo.AdjacentRooms.East | RoomInfo.AdjacentRooms.South))
            {
                room = Instantiate(RB_Room, roomInfo.position, Quaternion.identity);
            }
            else if (roomInfo.adjacentRooms == (RoomInfo.AdjacentRooms.East | RoomInfo.AdjacentRooms.West))
            {
                room = Instantiate(RL_Room, roomInfo.position, Quaternion.identity);
            }
            else if (roomInfo.adjacentRooms == (RoomInfo.AdjacentRooms.South | RoomInfo.AdjacentRooms.West))
            {
                room = Instantiate(BL_Room, roomInfo.position, Quaternion.identity);
            }
            #endregion

            #region 3_exits
            else if (roomInfo.adjacentRooms == (RoomInfo.AdjacentRooms.North | RoomInfo.AdjacentRooms.East | RoomInfo.AdjacentRooms.South))
            {
                room = Instantiate(TRB_Room, roomInfo.position, Quaternion.identity);
            }
            else if (roomInfo.adjacentRooms == (RoomInfo.AdjacentRooms.North | RoomInfo.AdjacentRooms.East | RoomInfo.AdjacentRooms.West))
            {
                room = Instantiate(TRL_Room, roomInfo.position, Quaternion.identity);
            }
            else if (roomInfo.adjacentRooms == (RoomInfo.AdjacentRooms.North | RoomInfo.AdjacentRooms.South | RoomInfo.AdjacentRooms.West))
            {
                room = Instantiate(TBL_Room, roomInfo.position, Quaternion.identity);
            }
            else if (roomInfo.adjacentRooms == (RoomInfo.AdjacentRooms.East | RoomInfo.AdjacentRooms.South | RoomInfo.AdjacentRooms.West))
            {
                room = Instantiate(RBL_Room, roomInfo.position, Quaternion.identity);
            }
            #endregion

            else
            {
                room = Instantiate(TRBL_Room, roomInfo.position, Quaternion.identity);
            }

            roomList.Add(room);

            //TODO edit all prefabs with a RoomBehaviour
            if (room.GetComponent<RoomBehaviour>() != null)
            {
                room.GetComponent<RoomBehaviour>().roomInfo = roomInfo;
            }
            //room.GetComponent<RoomBehaviour>().roomInfo = roomInfo;
        }

        Camera.main.GetComponent<CameraBetweenRooms>().CurrentRoom = roomList[0];
    }
}