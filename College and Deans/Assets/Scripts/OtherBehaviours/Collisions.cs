using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Collisions : MonoBehaviour
{

    public bool collideHole = false;
    public float damage { get; set; }

    void OnCollisionStay2D(Collision2D other)
    {
        if (this.tag == "Player" )
        {
            if(other.gameObject.tag == "Enemy" || other.gameObject.tag=="Boss")
            {
                this.GetComponent<ExternMechanicsPlayer>().damage = true;
            }
        }

       
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().GetHit((int)damage);
        }
        else if (other.gameObject.tag == "Boss")
        {
            other.gameObject.GetComponent<Enemy>().GetHit((int)damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().GetHit((int)damage);
        }
        else if(other.gameObject.tag == "Boss")
        {
            other.gameObject.GetComponent<Enemy>().GetHit((int)damage);
        }

        if (this.tag == "Bullet")
            if(this.gameObject.layer!=13)
                Destroy(this.gameObject);
        if (this.tag == "Bomb")
        {
            Debug.Log(other.gameObject.tag);
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
