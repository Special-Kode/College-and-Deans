using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enhancer", menuName = "Items/Enhancer")]
public class Enhancer : ScriptableObject
{
    public Sprite sprite;
    public AffectedStat affected;
    public float amount;

    public enum AffectedStat
    {
        Speed = 1, 
        Damage = 2, 
        Timescale = 4, 
        Resistance = 8,
        Berserk = 16,
        SpeedAndTimescale = Speed | Timescale,
        SpeedAndDamage = Speed | Damage,
        DamageAndResistance = Damage | Resistance,
        CookieJar = SpeedAndTimescale | Berserk, //For cookie jar
    }
}
