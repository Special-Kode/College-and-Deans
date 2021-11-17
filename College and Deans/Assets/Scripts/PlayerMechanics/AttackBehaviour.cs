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
    [SerializeField] private GameObject Wave360;
    [SerializeField] private Sprite Explosion;
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
            if (Vector2.Distance(GameObject.FindGameObjectWithTag("Bomb").transform.position,posBomb) < 0.1f)
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
                temp = Instantiate(bullet, this.transform.position, Quaternion.identity);
                temp = Shoot(position, MousePos,temp,0,speedBullet,90);
                temp.GetComponent<Collisions>().damage = weapon.getDamage();
                break;
            case 1:
                bulletShooted = true;
                //animatorPlayer.WhereToLook(Input.mousePosition);
                temp = Instantiate(bullet, this.transform.position, Quaternion.identity);
                temp = Shoot(position, MousePos,temp,1,speedBullet,90);
                StartCoroutine(ExecuteAfterTime(0.2f, position, MousePos,2,bullet, speedBullet,0));
                temp.GetComponent<Collisions>().damage = weapon.getDamage();
                break;
            case 2:
                bulletShooted = true;
                temp = Instantiate(Bomb, this.transform.position, Quaternion.identity);
                Shoot(position, MousePos, temp, 2,speedBullet-20f,0);
                posBomb = MousePos;
                temp.GetComponent<Collisions>().damage = weapon.getDamage();
                break;
            case 3:
                temp = Instantiate(SimpleWave, this.transform.position, Quaternion.identity);
                CreateWave(MousePos, position, temp);
                temp.GetComponent<Collisions>().damage = weapon.getDamage();
                break;
            case 4:
                bulletShooted = true;
                temp = Instantiate(Wave360, this.transform);
                StartCoroutine(WaveCollider(0.1f,temp,0));
                temp.GetComponent<Collisions>().damage = weapon.getDamage();
                break;
        }

    }
  

     GameObject Shoot(Vector3 playerPos,Vector3 mousePos,GameObject TypeOfShoot,int type,float speed,float rotation)
    {
        if (TypeOfShoot != null)
        {
            Vector2 dir = new Vector2(mousePos.x - playerPos.x, mousePos.y - playerPos.y).normalized;
            TypeOfShoot.GetComponent<Rigidbody2D>().velocity = dir * speed;
            dir = (mousePos - playerPos).normalized;
            float rot_z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            TypeOfShoot.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + rotation);
        }  
            
        return TypeOfShoot;
    }
    IEnumerator ExecuteAfterTime(float time,Vector3 position, Vector3 MousePos,int Type,GameObject temp,float speed, float rotation)
    {
        if(GameObject.FindGameObjectWithTag(temp.tag) != null)
        {
            yield return new WaitForSeconds(time);
                Shoot(position, MousePos, temp, Type, speed, rotation);

        }

    }
    IEnumerator WaveCollider(float time,GameObject temp,int count)
    {

            if (count < 5)
            {
                if (temp!=null)
                {
                    temp.transform.localScale += new Vector3(1f, 0, 0);
                    yield return new WaitForSeconds(time);
                    StartCoroutine(WaveCollider(time, temp, count + 1));
                }

            }
            else
                Destroy(temp.gameObject);

    }
   public void Explode(GameObject bomb)
    {
        bomb.transform.localScale += new Vector3(2f, 2f, 0);
        bomb.GetComponent<SpriteRenderer>().sprite = Explosion;
        bomb.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        StartCoroutine(DestroyAfterTime(0.2f,bomb));
    }
    void CreateWave(Vector3 posToShoot,Vector3 position,GameObject temp)
    {
        bulletShooted = true;
        temp = Shoot(position, posToShoot, temp, 3, speedBullet - 25f, 90);
        StartCoroutine(WaveCollider(0.1f, temp, 0));
    }
    IEnumerator DestroyAfterTime(float time,GameObject temp)
    {
        yield return new WaitForSeconds(time);
        if (temp != null)
           Destroy(temp.gameObject);
    }

}


