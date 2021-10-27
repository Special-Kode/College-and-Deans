using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternMechanicsPlayer : MonoBehaviour
{
    public bool damage;
    public bool death;
    public int vida;
    public bool canMove;
    private Collider[] colliders;
    Vector3 direction;
    Vector3 temp;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        damage = false;
        vida = 100;
        colliders = new BoxCollider[GameObject.FindGameObjectWithTag("CollidersTag").GetComponentInChildren<Transform>().childCount];
        colliders= GameObject.FindGameObjectWithTag("CollidersTag").GetComponentsInChildren<Collider>();
        direction = Vector3.zero;
        temp = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {

        if (damage == true)
        {
            calculateHealth();
            animationDamage();
        }
        
        
    }
    //se asigna la vida según la escala.x de la barra de vida,
    //con esto,si se hacen cambios de cuánto baja la barra de vida por cada golpe, se actualizará solo
    void calculateHealth()
    {
        float temp1, temp2, temp3;
        temp1 = GameObject.FindGameObjectWithTag("HealthBarTag").transform.localScale.x;
        temp2 = GameObject.FindGameObjectWithTag("HealthBarTag").GetComponentInChildren<BarAnimationScript>().sizeBar;
        temp3 = 100;
        vida =  Mathf.RoundToInt(temp1*temp3/temp2);
        if (vida == 0)
        {
            death = true;
        }

    }
    void animationDamage()
    {
        if (damage == true)
        {
            if (GameObject.FindGameObjectWithTag("HealthBarTag").GetComponentInChildren<BarAnimationScript>().TimeDamage == 20)
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            else
                this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, (1f / GameObject.FindGameObjectWithTag("HealthBarTag").GetComponentInChildren<BarAnimationScript>().TimeDamage),
                    (1f / GameObject.FindGameObjectWithTag("HealthBarTag").GetComponentInChildren<BarAnimationScript>().TimeDamage));
        }
    }
   /* public bool MoveOrNot(Vector3 position)
    {

        foreach (Collider collider in colliders){
            if (Physics.OverlapBox(collider.transform.position,collider.transform.localScale).Length!=0)
               return false;
        }
        return true;
    }
   */
}
