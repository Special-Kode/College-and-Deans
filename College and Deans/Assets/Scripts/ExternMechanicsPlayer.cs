using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExternMechanicsPlayer : MonoBehaviour
{
    public bool damage;
    public bool death;
    public int vida;
    public bool canMove;
    private float NoDamageTimer;
    private int TimeLife= 100;
    private bool canBeDamage;
    [SerializeField] private int DamageAmount = 3;
    [SerializeField] private float Invulnerability = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        damage = false;
        vida = 100;
        NoDamageTimer = 0;
        canBeDamage = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (damage)
        {

            animationDamage();
            canBeDamage = false;
            AddNoDamageTime();
        }

        vida=TimeLife-(int)Time.timeSinceLevelLoad;
        calculateHealth();

    }
    //se asigna la vida según la escala.x de la barra de vida,
    //con esto,si se hacen cambios de cuánto baja la barra de vida por cada golpe, se actualizará solo
    void calculateHealth()
    {
        float temp1, temp2, temp3;
        if (vida <= 0)
        {
            death = true;
            SceneManager.LoadScene("MainMenu");
        }

    }
    void animationDamage()
    {
        if(canBeDamage)
            TimeLife -= DamageAmount;
        /*
        if (GameObject.FindGameObjectWithTag("HealthBarTag").GetComponentInChildren<BarAnimationScript>().TimeDamage == 20)
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        else
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, (1f / GameObject.FindGameObjectWithTag("HealthBarTag").GetComponentInChildren<BarAnimationScript>().TimeDamage),
                (1f / GameObject.FindGameObjectWithTag("HealthBarTag").GetComponentInChildren<BarAnimationScript>().TimeDamage));
    }*/

        /* public bool MoveOrNot(Vector3 position)
         {

             foreach (Collider collider in colliders){
                 if (Physics.OverlapBox(collider.transform.position,collider.transform.localScale).Length!=0)
                    return false;
             }
             return true;
         }
        */
    }
    void AddNoDamageTime()
    {
        NoDamageTimer += Time.deltaTime;
        if (NoDamageTimer >= Invulnerability)
        {
            damage = false;
            canBeDamage = true;
            NoDamageTimer = 0;
        }
           
    }
}
