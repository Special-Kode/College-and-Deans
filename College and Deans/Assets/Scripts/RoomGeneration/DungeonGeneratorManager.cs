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

    public bool useDefaultRoomSet = false;
    public int defaultRoomSetNum = 0;

    private bool lootSpawned;
    private bool cafeSpawned;

    private GameManager gameManager;

    [Header("Private Serialized Stuff")]
    [SerializeField] private Vector2 currentPos = Vector2.zero;

    [SerializeField] private List<RoomInfo> roomInfoList; //Saves useful room info for rearrangement
    [SerializeField] private List<GameObject> roomList; //Saves created rooms for rearrangement
    [SerializeField] private List<RoomBehaviour> roomBehaviourList; //Saves created rooms for rearrangement
    [SerializeField] private List<Vector2> positions;

    /*
    private void Awake()
    {/*
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    */

    // Start is called before the first frame update
    void Start()
    {
        lootSpawned = false;
        cafeSpawned = false;
        gameManager = FindObjectOfType<GameManager>();

        positions = new List<Vector2>();
        roomInfoList = new List<RoomInfo>();
        GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Level generation call, dependent of generation method
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

    // Procedural level generation, it generates num rooms
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
                    SetLastRoom(roomInfo);
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

    // Randomly sets the type of the room, excluding spawn and boss room type
    void SetRandomRoom(RoomInfo roomInfo)
    {
        float rnd = UnityEngine.Random.Range(0.0f, 1.0f);

        if (rnd < 0.1f && !cafeSpawned)
        {
            roomInfo.roomType = RoomInfo.RoomType.Cafe;
            cafeSpawned = true;
        }
        else if (rnd < 0.4f && !lootSpawned)
        {
            roomInfo.roomType = RoomInfo.RoomType.ModLoot;
            lootSpawned = true;
        }
        else if (rnd < 0.7f && !lootSpawned)
        {
            roomInfo.roomType = RoomInfo.RoomType.EnhLoot;
            lootSpawned = true;
        }
        else
        {
            roomInfo.roomType = RoomInfo.RoomType.Enemies;
        }
    }

    void SetLastRoom(RoomInfo roomInfo)
    {
        if(gameManager.StageNum != gameManager.MaxLevelStages)
        {
            //TODO change functionality when possible
            roomInfo.roomType = RoomInfo.RoomType.Stairs;
        }
        else
        {
            roomInfo.roomType = RoomInfo.RoomType.Boss;
        }
    }

    // Check adjacent rooms for every room to instantiate and instantiates it dependent of the doors needed
    void RearrangeLevel()
    {
        foreach(var roomInfo in roomInfoList)
        {
            Debug.Log(roomInfo.roomType); //Possibly removed, but used for fast room type debugging
            roomInfo.CheckAdjacentRooms(positions, MoveAmount);

            string toConcat = "";
            if ((roomInfo.adjacentRooms & RoomInfo.AdjacentRooms.North) != 0)
            {
                toConcat += "T";
            }
            if ((roomInfo.adjacentRooms & RoomInfo.AdjacentRooms.East) != 0)
            {
                toConcat += "R";
            }
            if ((roomInfo.adjacentRooms & RoomInfo.AdjacentRooms.South) != 0)
            {
                toConcat += "B";
            }
            if ((roomInfo.adjacentRooms & RoomInfo.AdjacentRooms.West) != 0)
            {
                toConcat += "L";
            }

            string roomName = GetRoomName(roomInfo, toConcat);
            var resource = Resources.Load<RoomBehaviour>(roomName);

            if(resource == null)
            {
                roomName = GetRoomNameSecure(toConcat);

                resource = Resources.Load<RoomBehaviour>(roomName);
            }

            GameObject roomInstance = (resource as RoomBehaviour).gameObject;
            GameObject room = Instantiate(roomInstance, roomInfo.position, Quaternion.identity);

            var minimapResource = Resources.Load<MinimapRoomBehaviour>("Minimap/Minimap_" + toConcat);
            Vector3 minimapPos = roomInfo.position;
            minimapPos.z = 1;
            GameObject minimapInstance = (minimapResource as MinimapRoomBehaviour).gameObject;
            GameObject minimapRoom = Instantiate(minimapInstance, minimapPos, Quaternion.identity);
            minimapRoom.GetComponent<MinimapRoomBehaviour>().roomInfo = roomInfo;

            room.GetComponent<RoomBehaviour>().minimapRoom = minimapRoom.GetComponent<MinimapRoomBehaviour>();

            roomList.Add(room);

            if (room.GetComponent<RoomBehaviour>() != null)
            {
                room.GetComponent<RoomBehaviour>().roomInfo = roomInfo;
                room.GetComponent<RoomBehaviour>().SetNavMesh();
                room.gameObject.transform.Find("Grid").gameObject.transform.Find("Solids").gameObject.layer = 9;
                room.gameObject.transform.Find("Grid").gameObject.transform.Find("Solids").GetComponent<TilemapRenderer>().sortingOrder=1;
                if (room.gameObject.transform.Find("Grid").gameObject.transform.Find("Holes")!=null)
                    room.gameObject.transform.Find("Grid").gameObject.transform.Find("Holes").gameObject.layer = 10;
                minimapRoom.GetComponent<MinimapRoomBehaviour>().room = room.GetComponent<RoomBehaviour>();
            }
        }

        foreach (var room in roomList)
        {
            room.GetComponent<RoomBehaviour>().SetAdjacentRooms(roomList, MoveAmount);
        }

        // Sets the spawn room for the current room
        Camera.main.GetComponent<CameraBetweenRooms>().CurrentRoom = roomList[0];
        roomList[0].GetComponent<RoomBehaviour>().hasBeenVisited = true;
    }

    string GetRoomName(RoomInfo roomInfo, string toConcat)
    {
        //TODO change room probability
        int numRoom = !useDefaultRoomSet ? UnityEngine.Random.Range(0, 8) : defaultRoomSetNum;

        string roomName = "Rooms/Room_0" + numRoom.ToString() + "/Room_" + toConcat + "_0" + numRoom.ToString();

        if (roomInfo.roomType == RoomInfo.RoomType.Spawn)
            roomName = "Rooms/Room_Start/Start_" + toConcat;

        /**
        if (roomInfo.roomType == RoomInfo.RoomType.Cafe)
            roomName = "Rooms/Room_Mod/Room_Mod_" + toConcat;
        //*/

        if (roomInfo.roomType == RoomInfo.RoomType.ModLoot)
            roomName = "Rooms/Room_Mod/Room_Mod_" + toConcat;

        if (roomInfo.roomType == RoomInfo.RoomType.EnhLoot)
            roomName = "Rooms/Room_Pow/Room_Pow_" + toConcat;

        if (roomInfo.roomType == RoomInfo.RoomType.Stairs)
            roomName = "Rooms/Room_Stairs/Room_Stairs_" + toConcat;

        //TODO change boss room generation
        if (roomInfo.roomType == RoomInfo.RoomType.Boss)
            roomName = "Rooms/Room_00/Room_" + toConcat + "_00";

        return roomName;
    }

    string GetRoomNameSecure(string toConcat)
    {
        int numRoom = UnityEngine.Random.Range(0, 6);

        string roomName = "Rooms/Room_0" + numRoom.ToString() + "/Room_" + toConcat + "_0" + numRoom.ToString();

        return roomName;
    }
}