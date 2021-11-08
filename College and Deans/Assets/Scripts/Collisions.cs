using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Collisions : MonoBehaviour
{

    public bool collide = false;


   void OnCollisionStay2D(Collision2D other)
    {
        collide = true;
        if (this.tag == "Player" )
        {
            this.GetComponent<AnimatorPlayerScript>().isDashed = false;

            if(other.gameObject.tag == "Enemy" || other.gameObject.tag=="Boss")
                this.GetComponent<ExternMechanicsPlayer>().damage = true;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {        
        if (this.tag == "Bullet")
        {
            

            if (other.gameObject.tag == "Enemy")
            {
                Destroy(this.gameObject);
                //hacer daño al enemigo
                Destroy(other.gameObject);
            }
            if(other.gameObject.tag == "Boss")
            {
                Destroy(other.gameObject);
                SceneManager.LoadScene("MainMenu");
            }
        }

        if(this.tag == "EnemyBullet")
        {
            
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<ExternMechanicsPlayer>().damage = true;
                Destroy(this.gameObject);
            }
            
        }
    }
}
