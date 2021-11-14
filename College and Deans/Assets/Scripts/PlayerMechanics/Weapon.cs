using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon 
{
    private string name;
    private int damage;
    private string type;
    public Weapon(string name,int damage,string type){
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
    public string getType()
    {
        return this.type;
    }
    
}
