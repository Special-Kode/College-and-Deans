using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextBehaviour : MonoBehaviour
{
    [SerializeField] private Text levelText;
    [SerializeField] private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        levelText = GameObject.Find("Level").GetComponent<Text>();
        gameManager = FindObjectOfType<GameManager>();

        PrintLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PrintLevel()
    {
        levelText.text = "LEVEL " + gameManager.LevelNum + "-" + gameManager.StageNum;
    }
}
