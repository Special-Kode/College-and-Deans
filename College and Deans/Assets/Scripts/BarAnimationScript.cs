using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarAnimationScript : MonoBehaviour
{
    //private GameObject HealthBar;
    public bool damage;
    public int TimeDamage;
    public float sizeBar;
    private string text;
    // Start is called before the first frame update
    void Start()
    {
        TimeDamage = 20;
       // HealthBar = this.gameObject;
        //sizeBar = HealthBar.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {

        /*
        damage = GameObject.FindGameObjectWithTag("Player").GetComponent<ExternMechanicsPlayer>().damage;
        if (damage == true)
        {
            if (TimeDamage == 0)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<ExternMechanicsPlayer>().damage = false;
                TimeDamage = 20;
            }
           

            TimeDamage -= 1;
            if (HealthBar.transform.localScale.x > 0)
                HealthBar.transform.localScale = new Vector3(HealthBar.transform.localScale.x - sizeBar/100,
                HealthBar.transform.localScale.y, 0);


           
        }*/
        this.GetComponentInChildren<UnityEngine.UI.Text>().text= GameObject.FindGameObjectWithTag("Player").GetComponent<ExternMechanicsPlayer>().vida.ToString();
    }
}
