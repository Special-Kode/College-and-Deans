using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Collisions : MonoBehaviour
{

    public bool collideHole = false;


   void OnCollisionStay2D(Collision2D other)
    {
        if (this.tag == "Player" )
        {

            if(other.gameObject.tag == "Enemy" || other.gameObject.tag=="Boss")
                this.GetComponent<ExternMechanicsPlayer>().damage = true;
            this.GetComponent<Animator>().SetBool("Dash", false);
            this.GetComponent<Movement>().agent.enabled=true;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {

        if (this.tag == "Bullet")
        {
            if (other.gameObject.tag == "Enemy")
            {
                Destroy(this.gameObject);
                //Falta añadir el daño del jugador
                other.gameObject.GetComponent<Enemy>().GetHit(3);
            }
           else if(other.gameObject.tag == "Boss")
            {
                Destroy(other.gameObject);
                if (FindObjectOfType<LevelLoader>() != null)
                    FindObjectOfType<LevelLoader>().LoadNextLevel();
                else
                    SceneManager.LoadScene("MainMenu");
            }
            else if(other.gameObject.tag == "Wall")
            {
                Destroy(this.gameObject);
            }
        }

        if(this.tag == "EnemyBullet")
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<ExternMechanicsPlayer>().damage = true;
                Destroy(this.gameObject);
            }else if(other.gameObject.tag == "Wall")
            {
                Destroy(this.gameObject);
            }
            
        }



       

    }
}
