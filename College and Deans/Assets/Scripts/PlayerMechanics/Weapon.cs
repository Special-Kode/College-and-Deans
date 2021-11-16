using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon 
{
    private string name;
    private int damage;
    private int type;
    public Weapon(string name,int damage,int type){
        this.name = name;
        this.damage = damage;
        this.type = type;
    }
    public string getName()
    {
        return this.name;
    }
    public int getDamage()
    {
        return this.damage;
    }
    public int getType()
    {
        return this.type;
    }
    
}
