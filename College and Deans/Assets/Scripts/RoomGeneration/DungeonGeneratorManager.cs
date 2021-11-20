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

    [SerializeField] private bool lootSpawned;
    [SerializeField] private bool cafeSpawned;

    [SerializeField] private GameManager gameManager;

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
        int rand = UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(RoomInfo.RoomType)).Length - 3);
        switch (rand)
        {
            case 0:
                roomInfo.roomType = RoomInfo.RoomType.Enemies;
                break;
            case 1:
                roomInfo.roomType = cafeSpawned ? RoomInfo.RoomType.Enemies : RoomInfo.RoomType.Cafe;
                cafeSpawned = true;
                lootSpawned = true;
                break;
            case 2:
                roomInfo.roomType = lootSpawned ? RoomInfo.RoomType.Enemies : RoomInfo.RoomType.Loot;
                cafeSpawned = true;
                lootSpawned = true;
                break;
        }
    }

    void SetLastRoom(RoomInfo roomInfo)
    {
        if(gameManager.StageNum != gameManager.MaxLevelStages)
        {
            //TODO change functionality when possible
            roomInfo.roomType = RoomInfo.RoomType.Boss;
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

            //TODO change room probability
            int numRoom = UnityEngine.Random.Range(0, 7);

            string roomName = "Rooms/Room_0" + numRoom.ToString() + "/Room_" + toConcat + "_0" + numRoom.ToString();

            if (roomInfo.roomType == RoomInfo.RoomType.Spawn)
                roomName = "Rooms/Room_Start/Start_" + toConcat;

            var resource = Resources.Load<RoomBehaviour>(roomName);

            if(resource == null)
            {
                numRoom = UnityEngine.Random.Range(0, 4);

                roomName = "Rooms/Room_0" + numRoom.ToString() + "/Room_" + toConcat + "_0" + numRoom.ToString();

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

            roomList.Add(room);

            if (room.GetComponent<RoomBehaviour>() != null)
            {
                room.GetComponent<RoomBehaviour>().roomInfo = roomInfo;
                room.GetComponent<RoomBehaviour>().SetNavMesh();
                room.gameObject.transform.Find("Grid").gameObject.transform.Find("Solids").gameObject.layer = 9;
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
}