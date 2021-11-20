using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class RoomBehaviour : MonoBehaviour
{

    EnemyGenerator enemyGenerator;
    Pathfinding pathfinding;

    [Header("Adjacent Rooms")]
    public RoomBehaviour topRoom;
    public RoomBehaviour rightRoom;
    public RoomBehaviour bottomRoom;
    public RoomBehaviour leftRoom;

    [Header("Room Information")]
    public RoomInfo roomInfo;
    public Transform[] SpawnPoints;
    public bool hasBeenVisited;
    public bool hasSpawned;
    public NavMeshSurface2d navMeshSurface;
    public int EnemyAmount;

    // Start is called before the first frame update
    void Start()
    {
        enemyGenerator = FindObjectOfType<EnemyGenerator>();

        foreach (var door in GetComponentsInChildren<DoorBehaviour>())
        {
            door.DisableCollider(); //Disable door colliders
            door.SetAdjacentRoom();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UnlockDoors();
    }

    public void SetAdjacentRooms(List<GameObject> rooms, Vector2 moveAmount)
    {
        foreach(var room in rooms)
        {
            if(room.GetComponent<RoomBehaviour>() != null)
            {
                if (room.transform.position == transform.position + Vector3.up * moveAmount.y)
                    topRoom = room.GetComponent<RoomBehaviour>();
                if (room.transform.position == transform.position + Vector3.down * moveAmount.y)
                    bottomRoom = room.GetComponent<RoomBehaviour>();
                if (room.transform.position == transform.position + Vector3.right * moveAmount.x)
                    rightRoom = room.GetComponent<RoomBehaviour>();
                if (room.transform.position == transform.position + Vector3.left * moveAmount.x)
                    leftRoom = room.GetComponent<RoomBehaviour>();
            }
        }
    }

    void UnlockDoors()
    {
        if(EnemyAmount <= 0 && hasSpawned)
        {
            foreach (var door in GetComponentsInChildren<DoorBehaviour>())
            {
                door.DisableCollider(); //Disable door colliders
            }
        }
    }

    void LockDoors()
    {
        foreach (var door in GetComponentsInChildren<DoorBehaviour>())
        {
            door.EnableCollider(); //Enable door colliders
        }
    }

    public void EnterRoom()
    {
        if (hasBeenVisited) return;

        hasBeenVisited = true;

        if (roomInfo.roomType == RoomInfo.RoomType.Enemies || roomInfo.roomType == RoomInfo.RoomType.Boss)
        {
            //Bloquear las puertas
            if (!hasSpawned)
            {
                LockDoors();
            }

            //Generar grid
            Vector2 originPosition = roomInfo.position + new Vector2(-11f, -7f);
            pathfinding = new Pathfinding(22, 14, 1f, originPosition);

            List<Transform> spawnPoints = new List<Transform>(SpawnPoints);
            
            switch (roomInfo.roomType)
            {
                case RoomInfo.RoomType.Enemies: //Genera enemigos
                    enemyGenerator.SpawnEnemies("facil", spawnPoints, pathfinding, this);
                    break;
                case RoomInfo.RoomType.Boss: //Genera al boss
                    enemyGenerator.SpawnEnemies("boss", spawnPoints, pathfinding, this);
                    break;
            }
            spawnPoints.Clear();

            //No volver a spawnear
            hasSpawned = true;
        }

        if (roomInfo.roomType == RoomInfo.RoomType.Loot)
        {
            var enhancerLoot = Resources.Load("ConsumableItems/Enhancer");
            Instantiate(enhancerLoot, SpawnPoints[0].position, Quaternion.identity);
        }

        if (roomInfo.roomType == RoomInfo.RoomType.Cafe)
        {
            var modifierLoot = Resources.Load("ConsumableItems/Modifier");
            Instantiate(modifierLoot, SpawnPoints[0].position, Quaternion.identity);
        }
    }

    public void SetNavMesh()
    {
        navMeshSurface = this.gameObject.GetComponentInChildren<NavMeshSurface2d>();
        navMeshSurface.collectObjects = CollectObjects2d.All;
        navMeshSurface.defaultArea = 1;
        // navMeshSurface.useGeometry = NavMeshCollectGeometry.PhysicsColliders;
        navMeshSurface.ignoreNavMeshAgent = false;
        navMeshSurface.gameObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
        navMeshSurface.BuildNavMesh();
    }
}
