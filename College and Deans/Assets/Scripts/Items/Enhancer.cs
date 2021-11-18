using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enhancer", menuName = "Items/Enhancer")]
public class Enhancer : ScriptableObject
{
    public Sprite sprite;
    public float amount;
    public AffectedStat affected;

    public enum AffectedStat
    {
        Speed, Damage, TimeScale, Berserk
    }
}
