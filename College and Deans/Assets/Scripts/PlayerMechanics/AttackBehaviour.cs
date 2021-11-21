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
                temp = Shoot(position, MousePos,temp,speedBullet,90);
                FindObjectOfType<SFXManager>().shotSFX();
                temp.GetComponent<Collisions>().damage = weapon.getDamage();
                break;
            case 1:
                bulletShooted = true;
                //animatorPlayer.WhereToLook(Input.mousePosition);
                temp = Instantiate(bullet, this.transform.position, Quaternion.identity);
                temp = Shoot(position, MousePos,temp,speedBullet,90);
                FindObjectOfType<SFXManager>().shotSFX();
                StartCoroutine(ExecuteAfterTime(0.2f, position, MousePos,speedBullet,90));
                temp.GetComponent<Collisions>().damage = weapon.getDamage();
                break;
            case 2:
                bulletShooted = true;
                temp = Instantiate(Bomb, this.transform.position, Quaternion.identity);
                Shoot(position, MousePos, temp,speedBullet-20f,0);
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
  

    GameObject Shoot(Vector3 playerPos,Vector3 mousePos,GameObject TypeOfShoot,float speed,float rotation)
    {
        if (TypeOfShoot != null)
        {
            Vector2 dir = new Vector2(mousePos.x - playerPos.x, mousePos.y - playerPos.y).normalized;
            TypeOfShoot.GetComponent<Rigidbody2D>().velocity = dir * speed;
            dir = (mousePos - playerPos).normalized;
            TypeOfShoot.transform.position += new Vector3(dir.x,dir.y,0);
            float rot_z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            TypeOfShoot.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + rotation);
        }  
            
        return TypeOfShoot;
    }
    IEnumerator ExecuteAfterTime(float time,Vector3 position, Vector3 MousePos,float speed, float rotation)
    {

         GameObject temp_b = Instantiate(bullet, this.transform.position, Quaternion.identity);
         yield return new WaitForSeconds(time);
         Shoot(position, MousePos, temp_b, speed, rotation);
         FindObjectOfType<SFXManager>().shotSFX();
         temp_b.GetComponent<Collisions>().damage = weapon.getDamage();


    }
    IEnumerator WaveCollider(float time,GameObject temp,int count)
    {
        if (count < 5)
        {
            if (temp!=null)
            {
                if (temp.name == "Wave360(Clone)")
                {
                    temp.transform.localScale += new Vector3(.2f, .2f, 0);
                    temp.GetComponent<CircleCollider2D>().radius += .2f;
                }
                else
                {
                    temp.transform.localScale += new Vector3(.3f, 0, 0);
                    temp.GetComponent<BoxCollider2D>().size += new Vector2(.3f, .3f);
                }

                yield return new WaitForSeconds(time);
                StartCoroutine(WaveCollider(time, temp, count + 1));
            }
        }
        else
            Destroy(temp.gameObject);

    }
    public void Explode(GameObject bomb)
    {
        bomb.transform.localScale = new Vector3(2f, 2f, 0);
        bomb.GetComponent<CircleCollider2D>().radius = 2f;
        bomb.GetComponent<SpriteRenderer>().sprite = Explosion;
        bomb.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        FindObjectOfType<SFXManager>().explosionSFX();
        StartCoroutine(DestroyAfterTime(0.4f,bomb));
    }
    void CreateWave(Vector3 posToShoot,Vector3 position,GameObject temp)
    {
        bulletShooted = true;
        temp = Shoot(position, posToShoot, temp, speedBullet - 25f, 90);
        StartCoroutine(WaveCollider(0.1f, temp, 0));
    }
    IEnumerator DestroyAfterTime(float time,GameObject temp)
    {
        yield return new WaitForSeconds(time);
        if (temp != null)
           Destroy(temp.gameObject);
    }

}


