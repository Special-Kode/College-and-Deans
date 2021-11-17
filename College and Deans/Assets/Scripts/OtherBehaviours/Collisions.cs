using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Collisions : MonoBehaviour
{

    public bool collideHole = false;
    public int damage { get; set; }

   void OnCollisionStay2D(Collision2D other)
    {
        if (this.tag == "Player" )
        {

            if(other.gameObject.tag == "Enemy" || other.gameObject.tag=="Boss")
                this.GetComponent<ExternMechanicsPlayer>().damage = true;
            this.GetComponent<Movement>().agent.enabled=true;
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {

        if (this.tag == "Bullet" || this.tag=="Wave" || this.tag == "Bomb")
        {
            if (other.gameObject.tag == "Enemy")
            {
                //Falta añadir el daño del jugador
                other.gameObject.GetComponent<Enemy>().GetHit(damage);
            }
           else if(other.gameObject.tag == "Boss")
            {
                Destroy(other.gameObject);
                if (FindObjectOfType<LevelLoader>() != null)
                    FindObjectOfType<LevelLoader>().LoadNextStage();
                else
                    SceneManager.LoadScene("MainMenu");
            }

            if (this.tag == "Bullet")
                Destroy(this.gameObject);
            if (this.tag == "Bomb")
                GameObject.FindGameObjectWithTag("Player").GetComponent<AttackBehaviour>().Explode(this.gameObject);
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
