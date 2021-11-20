using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    //TODO: Hacer que DungeonGenerator pueda llamar a este objeto cuando quiera

    private Dictionary<int, List<Enemy>> setFacil = new Dictionary<int, List<Enemy>>();
    private int numSetsFacil = 6;
    [SerializeField] private Enemy enemyMelee1;
    [SerializeField] private Enemy enemyMelee2;
    [SerializeField] private Enemy enemyDist1;
    [SerializeField] private Enemy enemyDist2;
    [SerializeField] private Enemy boss0;
    [SerializeField] private Enemy boss1;

    public List<Transform> spawns;

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

    private void Start() 
    {
        CreacionSets();
        var rooms = FindObjectsOfType<RoomBehaviour>();
    }

    //Creacion de los diferentes set de enemigos
    //TODO: Mejorar creaci�n de sets
    private void CreacionSets()
    {
        List<Enemy> set0 = new List<Enemy>();
        List<Enemy> set1 = new List<Enemy>();  
        List<Enemy> set2 = new List<Enemy>();
        List<Enemy> set3 = new List<Enemy>();  
        List<Enemy> set4 = new List<Enemy>();
        List<Enemy> set5 = new List<Enemy>();

        //Creacion set 0
        for (int i = 0; i < 4; i++)
        {
            set0.Add(enemyMelee1);
        }

        //Creacion set 1
        for (int i = 0; i < 3; i++)
        {
            set1.Add(enemyDist2);
        }
        set1.Add(enemyMelee2);

        //Creacion set 2
        set2.Add(enemyMelee1);
        set2.Add(enemyMelee2);
        set2.Add(enemyDist1);
        set2.Add(enemyDist2);

        //Creacion set 3
        set3.Add(enemyMelee2);
        set3.Add(enemyMelee2);
        set3.Add(enemyMelee1);
        set3.Add(enemyMelee1);

        //Creacion set 4
        for (int i = 0; i < 4; i++)
        {
            set4.Add(enemyDist1);
        }

        //Creacion set 5
        set5.Add(enemyDist1);
        set5.Add(enemyDist1);
        set5.Add(enemyDist2);
        set5.Add(enemyDist2);

        //Insercion de los sets en el diccionario
        setFacil.Add(0, set0);
        setFacil.Add(1, set1);
        setFacil.Add(2, set2);
        setFacil.Add(3, set3);
        setFacil.Add(4, set4);
        setFacil.Add(5, set5);
    }
    
    //Metodo para generar los enemigos de cada sala:
    //Recibe la dificultad de la sala y una lista con los lugares donde pueden hacer spawn los enemigos
    public void SpawnEnemies(string tipoSala, List<Transform> spawns, Pathfinding pathfinding, RoomBehaviour room)
    {
        if (spawns.Count == 0) return;

        switch(tipoSala)
        {
            case "facil":
                int random = Random.Range(0, numSetsFacil);
                List<Enemy> setRandom, setToCopy;
                setFacil.TryGetValue(random, out setToCopy);

                setRandom = new List<Enemy>();
                setRandom.AddRange(setToCopy);

                do
                {
                    Enemy temp = setRandom[0];
                    Transform spawnPosition = spawns[Random.Range(0, spawns.Count)];

                    var enemy = Instantiate(temp, spawnPosition.position, Quaternion.identity);
                    enemy.EnemyPathfinding.SetPathfinding(pathfinding);
                    enemy.Room = room;
                    room.EnemyAmount += 1;

                    spawns.Remove(spawnPosition);
                    setRandom.Remove(temp);
                } while (setRandom.Count != 0);

                break;
            case "medio": 
                break;
            case "dificil":
                break;
            case "boss":
                int level = FindObjectOfType<GameManager>().LevelNum;
                Enemy bossToInstantiate, boss;

                switch (level)
                {
                    case 1:
                        bossToInstantiate = boss0;
                        break;
                    case 2:
                        bossToInstantiate = boss1;
                        break;
                    default:
                        bossToInstantiate = boss0;
                        break;
                }

                boss = Instantiate(bossToInstantiate, spawns[0].position, Quaternion.identity);
                boss.EnemyPathfinding.SetPathfinding(pathfinding);
                boss.Room = room;
                room.EnemyAmount += 1;
                break;
        }
    }
}
