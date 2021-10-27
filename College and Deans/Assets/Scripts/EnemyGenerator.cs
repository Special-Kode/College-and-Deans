using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    //TODO: Hacer que DungeonGenerator pueda llamar a este objeto cuando quiera

    private Dictionary<int, List<Enemy>> setFacil = new Dictionary<int, List<Enemy>>();
    private int numSetsFacil = 5;
    [SerializeField] private Enemy enemy0;
    [SerializeField] private Enemy enemy1;

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

        foreach (var room in rooms)
        {
            if(room.roomInfo.roomType == RoomInfo.RoomType.Enemies)
            {
                var spawnPoints = room.SpawnPoints;
                spawns.AddRange(spawnPoints);
                SpawnEnemies("facil", spawns);
                spawns.Clear();
            }
        }

        /**
        var oneroom = FindObjectOfType<RoomBehaviour>();
        if (oneroom != null)
        {
            var spawnPoints = oneroom.SpawnPoints;
            spawns.AddRange(spawnPoints);
            SpawnEnemies("facil", spawns);
        }
        //*/
    }

    //Creacion de los diferentes set de enemigos
    //TODO: Mejorar creación de sets
    private void CreacionSets()
    {
        List<Enemy> set0 = new List<Enemy>();
        List<Enemy> set1 = new List<Enemy>();  
        List<Enemy> set2 = new List<Enemy>();
        List<Enemy> set3 = new List<Enemy>();  
        List<Enemy> set4 = new List<Enemy>();

        //Creacion set 0
        for (int i = 0; i < 4; i++)
        {
            set0.Add(enemy0);
        }

        //Creacion set 1
        for (int i = 0; i < 3; i++)
        {
            set1.Add(enemy0);
        }
        set1.Add(enemy1);

        //Creacion set 2
        set2.Add(enemy0);
        set2.Add(enemy0);
        set2.Add(enemy1);
        set2.Add(enemy1);

        //Creacion set 3
        set3.Add(enemy0);
        set3.Add(enemy1);
        set3.Add(enemy1);
        set3.Add(enemy1);

        //Creacion set 4
        for (int i = 0; i < 4; i++)
        {
            set4.Add(enemy1);
        }

        //Insercion de los sets en el diccionario
        setFacil.Add(0, set0);
        setFacil.Add(1, set1);
        setFacil.Add(2, set2);
        setFacil.Add(3, set3);
        setFacil.Add(4, set4);
    }
    
    //Metodo para generar los enemigos de cada sala:
    //Recibe la dificultad de la sala y una lista con los lugares donde pueden hacer spawn los enemigos
    public void SpawnEnemies(string tipoSala, List<Transform> spawns)
    {
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
                    Instantiate(temp, spawnPosition);
                    temp.gameObject.tag = "Enemy";
                    temp.gameObject.AddComponent<Rigidbody2D>();
                    temp.gameObject.AddComponent<BoxCollider2D>();
                    spawns.Remove(spawnPosition);
                    setRandom.Remove(temp);
                } while (setRandom.Count != 0);

                break;
            case "medio": 
                break;
            case "dificil":
                break;
        }
    }
}
