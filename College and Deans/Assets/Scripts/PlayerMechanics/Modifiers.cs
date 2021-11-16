using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Modifiers :MonoBehaviour
{
    // Start is called before the first frame update
  public List< Weapon> modifiers;

    public void Init()
    {
        modifiers = new List<Weapon>(5);
        modifiers.Add(new Weapon("NormalWeapon", 1, 0));
        modifiers.Add(new Weapon("DoubleShoot", 1, 1));
        modifiers.Add(new Weapon("Bomb", 1, 2));
        modifiers.Add(new Weapon("SimpleWave", 2, 3));
        modifiers.Add(new Weapon("MultiWave", 2, 4));
    }
}
