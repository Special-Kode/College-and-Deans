using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon 
{
    private string name;
    private int damage;
    private int type;

    private int damageMultiplier;
    public Weapon(string name,int damage,int type){
        this.name = name;
        this.damage = damage;
        this.type = type;

        this.damageMultiplier = 1;
    }
    public string getName()
    {
        return this.name;
    }
    public int getDamage()
    {
        return this.damage * damageMultiplier;
    }
    public int getType()
    {
        return this.type;
    }
    
    public void SetDamageMultiplier(int _damageMultiplier)
    {
        damageMultiplier = _damageMultiplier;
    }
}
