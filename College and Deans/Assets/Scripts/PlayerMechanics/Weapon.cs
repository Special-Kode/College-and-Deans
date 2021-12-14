using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon 
{
    [SerializeField] private string name;
    [SerializeField] private int damage;
    [SerializeField] private int type;
    
    [SerializeField] private float damageMultiplier;
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
    public float getDamage()
    {
        return this.damage * damageMultiplier;
    }
    public int getType()
    {
        return this.type;
    }
    
    public float GetDamageMultiplier()
    {
        return damageMultiplier;
    }

    public void MultiplyDamage(float _multiplyDamage)
    {
        damageMultiplier *= _multiplyDamage;
    }

    public void SetDamageMultiplier(float _damageMultiplier)
    {
        int amount = (int)_damageMultiplier;

        damageMultiplier = amount;
    }
}
