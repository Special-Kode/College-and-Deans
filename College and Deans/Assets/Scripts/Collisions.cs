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
                //cuando se añadan más colisiones revisar esta línea
                this.GetComponent<AnimatorPlayerScript>().isMoved = false;
                this.GetComponent<AnimatorPlayerScript>().isDashed = false;
            }
        }
        if (this.tag == "Bullet" )
        {
            Destroy(this.gameObject);
        }




    }


}
