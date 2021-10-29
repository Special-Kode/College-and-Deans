using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    AttackBehaviour attack;
    Movement movement;
    public bool collide = false;


   void OnCollisionStay2D(Collision2D other)
    {
        collide = true;
        if (this.tag == "Player" )
        {
            if(other.gameObject.tag == "Enemy")
                this.GetComponent<ExternMechanicsPlayer>().damage = true;
            else
            {
                //cuando se a�adan m�s colisiones revisar esta l�nea
                this.GetComponent<AnimatorPlayerScript>().isMoved = false;
                this.GetComponent<AnimatorPlayerScript>().isDashed = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {        
        if (this.tag == "Bullet" )
        {   
            if(other.gameObject.tag == "Enemy")
            {
                //hacer daño al enemigo
                Destroy(this.gameObject);
            }
        }

        if(this.tag == "EnemyBullet")
        {
            if(other.gameObject.tag == "Player")
            {
                this.GetComponent<ExternMechanicsPlayer>().damage = true;
                Destroy(this.gameObject);
            }
        }
    }
}
