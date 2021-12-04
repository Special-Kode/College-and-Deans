using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoomBehaviour : MonoBehaviour
{
    [Header("Spanish tutorial")]
    public GameObject mob_move_s;
    public GameObject mob_attack_s;
    public GameObject mob_dash_s;
    public GameObject pc_move_s;
    public GameObject pc_attack_s;
    public GameObject pc_dash_s;
    public GameObject board_s;

    [Header("English tutorial")]
    public GameObject mob_move_en;
    public GameObject mob_attack_en;
    public GameObject mob_dash_en;
    public GameObject pc_move_en;
    public GameObject pc_attack_en;
    public GameObject pc_dash_en;
    public GameObject board_en;

    // Start is called before the first frame update
    void Start()
    {
        SpawnStartProps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnStartProps()
    {
        Transform m_move = GameObject.Find("m_move").transform;
        Transform m_attack = GameObject.Find("m_attack").transform;
        Transform m_dash = GameObject.Find("m_dash").transform;

        Transform pc_move = GameObject.Find("pc_move").transform;
        Transform pc_attack = GameObject.Find("pc_attack").transform;
        Transform pc_dash = GameObject.Find("pc_dash").transform;

        Transform board = GameObject.Find("starting_board").transform;

        string language = PlayerPrefs.GetString("language", "e");

        Instantiate(language == "s" ? mob_move_s : mob_move_en, m_move);
        Instantiate(language == "s" ? mob_attack_s : mob_attack_en, m_attack);
        Instantiate(language == "s" ? mob_dash_s : mob_dash_en, m_dash);
        
        Instantiate(language == "s" ? pc_move_s : pc_move_en, pc_move);
        Instantiate(language == "s" ? pc_attack_s : pc_attack_en, pc_attack);
        Instantiate(language == "s" ? pc_dash_s : pc_dash_en, pc_dash);

        Instantiate(language == "s" ? board_s : board_en, board);
    }
}
