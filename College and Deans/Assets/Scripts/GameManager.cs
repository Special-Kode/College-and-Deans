using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    DungeonGeneratorManager dungeonGenerator;
    EnemyGenerator enemyGenerator;
    Pathfinding pathfinding;

    void Start()
    {
        dungeonGenerator = FindObjectOfType<DungeonGeneratorManager>();
        enemyGenerator = FindObjectOfType<EnemyGenerator>();
    }

    public void EnterRoom(RoomBehaviour room)
    {
        if(room.roomInfo.roomType == RoomInfo.RoomType.Enemies)
        {
            //Generar grid
            Vector2 originPosition = room.roomInfo.position + new Vector2(-11f, -7f);
            pathfinding = new Pathfinding(22, 14, 1f, originPosition);
            //Generar enemigos
            List<Transform> spawnPoints = new List<Transform>(room.SpawnPoints);
            enemyGenerator.SpawnEnemies("facil", spawnPoints);
            spawnPoints.Clear();
        }
    }

    public Pathfinding GetPathfinding()
    {
        return pathfinding;
    }
}
