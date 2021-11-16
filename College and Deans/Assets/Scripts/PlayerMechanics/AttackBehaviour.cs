using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class AttackBehaviour :MonoBehaviour
{
    //Aqu� est�n los distintos comportamientos de ataque dependiendo de si el arma es una arma blanca o una arma de fuego.
    private Weapon weapon;
    private AnimatorPlayerScript animatorPlayer;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject SimpleWave;
    [SerializeField] private GameObject Bomb;
    public bool bulletShooted;
    public bool ClickedEnemy;
    float speedBullet;
    int typeMod;
    Vector3 posBomb;
    public void Start()
    {
        animatorPlayer = this.GetComponent<AnimatorPlayerScript>();
        bulletShooted = false;
        speedBullet = 30f;
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

    public void Update()
    {
        if (GameObject.FindGameObjectWithTag("Bomb") != null)
            if (Vector2.Distance(GameObject.FindGameObjectWithTag("Bomb").transform.position,posBomb) < 0.3f)
                Explode(GameObject.FindGameObjectWithTag("Bomb"));

    }
    public void attack(float seconds,Vector3 position,Vector3 MousePos)
    {
        typeMod = weapon.getType();

        GameObject temp;
        switch (getWeapon().getType())
        {
            
            case 0:
                bulletShooted = true;
                //animatorPlayer.WhereToLook(Input.mousePosition);
                temp = Shoot(position, MousePos,bullet,0,speedBullet,0);
                temp.GetComponent<Collisions>().damage = weapon.getDamage();
                break;
            case 1:
                bulletShooted = true;
                //animatorPlayer.WhereToLook(Input.mousePosition);
                temp = Shoot(position, MousePos,bullet,1,speedBullet,0);
                StartCoroutine(ExecuteAfterTime(0.2f, position, MousePos,2,bullet, speedBullet,0));
                break;
            case 2:
                bulletShooted = true;
                Shoot(position, MousePos, Bomb, 2,speedBullet-20f,0);
                posBomb = MousePos;
                break;
            case 3:
                CreateWave(MousePos, position);
                break;
            case 4:
                bulletShooted = true;
                CreateWave(position + Vector3.up,position);
                CreateWave(position + Vector3.right, position);
                CreateWave(position + Vector3.left, position);
                CreateWave(position + Vector3.down, position);

                break;
        }

    }
  

     GameObject Shoot(Vector3 playerPos,Vector3 mousePos,GameObject TypeOfShoot,int type,float speed,float rotation)
    {
        GameObject temp = TypeOfShoot;
        temp = Instantiate(TypeOfShoot, this.transform.position, Quaternion.identity);
        Vector2 dir = new Vector2(mousePos.x - playerPos.x, mousePos.y - playerPos.y).normalized;
        temp.GetComponent<Rigidbody2D>().velocity = dir * speed;
        dir = (mousePos - playerPos).normalized;
        float rot_z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        temp.transform.rotation = Quaternion.Euler(0f, 0f, rot_z+rotation);
        
            
        return temp;
    }
    IEnumerator ExecuteAfterTime(float time,Vector3 position, Vector3 MousePos,int Type,GameObject temp,float speed, float rotation)
    {
        yield return new WaitForSeconds(time);
        if (Type == 1)
            Shoot(position, MousePos, temp, Type,speed, rotation);
        else if (Type == 3 && Type == 4)
            Destroy(temp);
    }
    IEnumerator WaveCollider(float time,GameObject temp,int count)
    {
        if (count < 5)
        {
            temp.transform.localScale+=new Vector3(1f,0,0);
            yield return new WaitForSeconds(time);
            StartCoroutine(WaveCollider(time, temp,count+1));
        }
        else
            Destroy(temp);

    }
    void Explode(GameObject bomb)
    {
        bomb.transform.localScale += new Vector3(2f, 2f, 0);
    }
    void CreateWave(Vector3 posToShoot,Vector3 position)
    {
        GameObject temp;
        bulletShooted = true;
        temp = Shoot(position, posToShoot, SimpleWave, 3, speedBullet - 10f, 90);
        StartCoroutine(ExecuteAfterTime(0.1f, position, posToShoot, 3, temp, speedBullet - 10f, 90));
        StartCoroutine(WaveCollider(0.02f, temp, 0));
    }
}


