using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class AttackBehaviour :MonoBehaviour
{
    //Aquí están los distintos comportamientos de ataque dependiendo de si el arma es una arma blanca o una arma de fuego.
    private Weapon weapon;
    private AnimatorPlayerScript animatorPlayer;
   [SerializeField] private GameObject bullet;
    public bool bulletShooted;
    public bool ClickedEnemy;
    float speedBullet;
    Vector3 EnemyPos;
    public void Start()
    {
        animatorPlayer = this.GetComponent<AnimatorPlayerScript>();
        bulletShooted = false;
        speedBullet = 200000f;
        ClickedEnemy = false;
       
    }
    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }
    public Weapon getWeapon()
    {
        return weapon;
    }

    public void attack(float seconds,Vector3 position,Vector3 MousePos,int device)
    {
        this.EnemyPos = EnemyPos;
        if (getWeapon().getType() == "Sword")
        {

            if (Vector3.Distance(position, GameObject.FindGameObjectWithTag("Enemy").transform.position) < 1f)
            {
                if (Time.time - seconds > 2f || seconds == 0)
                {
                    animatorPlayer.SetAttack();
                }

            }
        }
        else if (getWeapon().getType() == "Gun")
        {

           bulletShooted = true;
           //animatorPlayer.WhereToLook(Input.mousePosition);
           animatorPlayer.SetAttack();
           shootBullet(position,MousePos);
                    
           
        }
            
        


    }
  

    public void shootBullet(Vector3 playerPos,Vector3 mousePos)
    {
        GameObject tempBullet = Instantiate(bullet, this.transform.position, Quaternion.identity);
        tempBullet.transform.position = playerPos;
        Vector2 dir = new Vector2(mousePos.x - playerPos.x, mousePos.y - playerPos.y).normalized;
        tempBullet.GetComponent<Rigidbody2D>().AddForce(dir * speedBullet * Time.deltaTime);
        dir = (Camera.main.ScreenToWorldPoint(mousePos) - playerPos).normalized;
        float rot_z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        tempBullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }
}


