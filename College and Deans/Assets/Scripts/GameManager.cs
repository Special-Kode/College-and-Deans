using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    DungeonGeneratorManager dungeonGenerator;
    EnemyGenerator enemyGenerator;
    Pathfinding pathfinding;

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

    public void EnterRoom(RoomBehaviour room)
    {
        if(room.roomInfo.roomType == RoomInfo.RoomType.Enemies && !room.hasSpawned)
        {
            //Bloquear puertas
            foreach (var door in room.GetComponentsInChildren<DoorBehaviour>())
            {
                door.EnableCollider(); //Enable door colliders
            }
            //Generar grid
            Vector2 originPosition = room.roomInfo.position + new Vector2(-11f, -7f);
            pathfinding = new Pathfinding(22, 14, 1f, originPosition);
            //Generar enemigos
            List<Transform> spawnPoints = new List<Transform>(room.SpawnPoints);
            enemyGenerator.SpawnEnemies("facil", spawnPoints);
            spawnPoints.Clear();
            //No volver a spawnear
            room.hasSpawned = true;
        }

        if(room.roomInfo.roomType == RoomInfo.RoomType.Boss && !room.hasSpawned)
        {
            //Bloquear puertas
            foreach (var door in room.GetComponentsInChildren<DoorBehaviour>())
            {
                door.EnableCollider(); //Enable door colliders
            }
            //Generar grid
            Vector2 originPosition = room.roomInfo.position + new Vector2(-11f, -7f);
            pathfinding = new Pathfinding(22, 14, 1f, originPosition);
            //Generar enemigos
            List<Transform> spawnPoints = new List<Transform>(room.SpawnPoints);
            enemyGenerator.SpawnEnemies("boss", spawnPoints);
            spawnPoints.Clear();
            //No volver a spawnear
            room.hasSpawned = true;
        }
    }

    public Pathfinding GetPathfinding()
    {
        return pathfinding;
    }
}
