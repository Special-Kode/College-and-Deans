using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsDisplay : MonoBehaviour
{
    public Text speed;
    public Text damage;
    public Text timescale;
    public Text resistance;

    StatsManager statsManager;

    // Start is called before the first frame update
    void Start()
    {
        statsManager = FindObjectOfType<StatsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStatsDisplay(speed, statsManager.SpeedStat);
        UpdateStatsDisplay(damage, statsManager.DamageStat);
        UpdateStatsDisplay(timescale, statsManager.TimeScaleStat);
        UpdateStatsDisplay(resistance, statsManager.ResistanceStat);
    }

    void UpdateStatsDisplay(Text text, float val)
    {
        int aux = (int)val;
        if (aux == val)
        {
            text.text = val.ToString();
            
        }
        else
        {
            text.text = val.ToString("0.0");
            if (text.text[text.text.Length - 1] == '0')
            {
                text.text = aux.ToString();
            }
        }
    }
}
