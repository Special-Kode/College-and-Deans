using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    DungeonGeneratorManager dungeonGenerator;
    EnemyGenerator enemyGenerator;
    Pathfinding pathfinding;

    public int LevelNum = 0;
    public int LevelStage = 1;

    [SerializeField] private int maxLevelStages = 5;

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

    void Start()
    {
        dungeonGenerator = FindObjectOfType<DungeonGeneratorManager>();
        enemyGenerator = FindObjectOfType<EnemyGenerator>();
    }

    /**
    public void EnterRoom(RoomBehaviour room)
    {
        if(room.roomInfo.roomType == RoomInfo.RoomType.Enemies && !room.hasSpawned)
        {
            //Bloquear puertas
            foreach (var door in room.GetComponentsInChildren<DoorBehaviour>())
            {
                //door.EnableCollider(); //Enable door colliders
            }
            //Generar grid
            Vector2 originPosition = room.roomInfo.position + new Vector2(-11f, -7f);
            pathfinding = new Pathfinding(22, 14, 1f, originPosition);
            //Generar enemigos
            List<Transform> spawnPoints = new List<Transform>(room.SpawnPoints);
            enemyGenerator.SpawnEnemies("facil", spawnPoints, pathfinding, room);
            spawnPoints.Clear();
            //No volver a spawnear
            room.hasSpawned = true;
        }

        if(room.roomInfo.roomType == RoomInfo.RoomType.Boss && !room.hasSpawned)
        {
            //Bloquear puertas
            foreach (var door in room.GetComponentsInChildren<DoorBehaviour>())
            {
                //door.EnableCollider(); //Enable door colliders
            }
            //Generar grid
            Vector2 originPosition = room.roomInfo.position + new Vector2(-11f, -7f);
            pathfinding = new Pathfinding(22, 14, 1f, originPosition);
            //Generar enemigos
            List<Transform> spawnPoints = new List<Transform>(room.SpawnPoints);
            enemyGenerator.SpawnEnemies("boss", spawnPoints, pathfinding, room);
            spawnPoints.Clear();
            //No volver a spawnear
            room.hasSpawned = true;
        }

        room.hasBeenVisited = true;
    }
    //*/

    public void AddLevelStage()
    {
        if (LevelStage < maxLevelStages)
            LevelStage += 1;
        else
        {
            LevelStage = 1;
            LevelNum += 1;
        }
    }

    public Pathfinding GetPathfinding()
    {
        return pathfinding;
    }
}
