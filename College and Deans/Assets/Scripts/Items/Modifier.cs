using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Modifier", menuName = "Items/Modifier")]
public class Modifier : ScriptableObject
{
    public Sprite sprite;
    public int modifierId;
}
