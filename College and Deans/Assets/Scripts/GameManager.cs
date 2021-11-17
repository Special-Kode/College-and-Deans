using System.Reflection;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    DungeonGeneratorManager dungeonGenerator;
    EnemyGenerator enemyGenerator;
    Pathfinding pathfinding;

    public int LevelNum = 1;
    public int StageNum = 1;

    public int MaxLevelStages 
    { 
        get { return maxLevelStages; } 
        private set { maxLevelStages = value; } 
    }

    public int MaxGameLevels
    {
        get { return maxGameLevels; }
        private set { maxGameLevels = value; }
    }

    [SerializeField] private int maxGameLevels = 3;
    [SerializeField] private int maxLevelStages = 5;

    private void Awake() 
    {
        MaxLevelStages = maxLevelStages;
        MaxGameLevels = maxGameLevels;

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

#if UNITY_EDITOR
    void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
#endif

    public void NextLevelOrStage()
    {
#if UNITY_EDITOR
        ClearLog();
#endif

        if (StageNum < MaxLevelStages)
            StageNum += 1;
        else
            NextLevel();
    }

    void NextLevel()
    {
        if (LevelNum == MaxGameLevels)
        {
            ResetGame();
        }
        else
        {
            StageNum = 1;
            LevelNum += 1;
        }
    }

    void ResetGame()
    {
        LevelNum = 1;
        StageNum = 1;
    }

    public Pathfinding GetPathfinding()
    {
        return pathfinding;
    }
}
