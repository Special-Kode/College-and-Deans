using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    Movement movement;
    private float MouseClickedTime, ClickDelay,SecondsToAttack, WhenDashStatusStarted,DashTimer;
    private int Clicks;
    public AttackBehaviour HowToAttack;
    Vector3 posToMove;
    public bool enter,canDash;
    public Vector3 PosInitMouse, posFinalMouse, PosInitDash, posFinaldash;
    Vector3 direction;
    [SerializeField] public int Weapon;
    public Modifiers Weapons;
    [SerializeField] public int NumModifier;
    void Start()
    {
        Clicks = 0;
        ClickDelay = 0.4f;
        animator = GetComponent<Animator>();
        movement = this.GetComponent<Movement>();
        HowToAttack = this.GetComponent<AttackBehaviour>();
        Weapons = GetComponent<Modifiers>();
        Weapons.Init();
        SecondsToAttack = 0;
        canDash = true;
       
    }

    // Aquí se cambian las variables de estado dependeiendo de el estado al cual se quiere llegar partiendo de un estado específico. También se dispone a ejecutar 
    //las distintas animaciones
    void Update()
    {

        //if you press left click, clicks is added 1 and it is saved the time
        if (Input.GetMouseButtonDown(0))
            {

                PosInitMouse = Input.mousePosition;
                PosInitMouse = Camera.main.ScreenToWorldPoint(PosInitMouse);
                PosInitMouse.z = 0;


                posToMove = PosInitMouse;
                posToMove.z = 0;

                Clicks++;
                if (Clicks % 2 != 0)
                {

                    MouseClickedTime = Time.time;


                }


            }



            //if you stop pressing left click, it is saved the position of the mouse, and check if distance of init dash and end dash is higher than 2
            else if (Input.GetMouseButtonUp(0))
            {

                posFinalMouse = Input.mousePosition;
                posFinalMouse = Camera.main.ScreenToWorldPoint(posFinalMouse);


                if (Vector3.Distance(posFinalMouse, PosInitMouse) > 1.5 && canDash==true
                     && !animator.GetBool("Dash"))
                {
                    InitDash();
                }

                // if is not dashing,it means that might player can move
                else if (Clicks % 2 == 0 && Clicks != 0)
                {
                    InitMove(); 
                }



            }
            //if the user press left click, there might be the possibility to the user press second click, if this not happen, the player attack if the user input click an enemy.

            if ((Time.time - MouseClickedTime) > ClickDelay && !animator.GetBool("Dash") && Vector3.Distance(posFinalMouse, PosInitMouse) < 1.5)
            {
                isEnemyClicked(posToMove);
            /*
                if (!HowToAttack.ClickedEnemy)
                {
                   

                }*/
                if (Clicks == 1 && !animator.GetBool("Dash") && GameObject.FindGameObjectWithTag("Bullet") == null)
                    Attack();
                Clicks = 0;
                MouseClickedTime = 0;
            }
   
              //  Debug.Log(this.GetComponent<Rigidbody2D>().velocity);
            if ((Vector2.Distance(posFinaldash,transform.position)<0.1f || (Time.time-DashTimer)>0.3f) && animator.GetBool("Dash")) //&& isDashed==true                                                                        )
                EndDash();
            


            if (canDash == false)
                checkIfcanDash();




            if (this.gameObject.GetComponentInChildren<ExternMechanicsPlayer>().death == true)
            {
                animator.SetBool("Death", true);
             }




    }
    private void LateUpdate()
    {
        if (animator.GetBool("Walking"))
            setAnimationDashOrWalk("BlendWalking");
        else if(animator.GetBool("Dash"))
            setAnimationDashOrWalk("BlendDash");
        else if (animator.GetBool("Attacking"))
            setAnimationAttacking();
      /*  else
            setAnimationIdle();
      */


    }


    public void Attack()
    {
        animator.SetBool("Attacking", true);
        SecondsToAttack = Time.time;
        HowToAttack.SetWeapon(Weapons.modifiers[NumModifier]);
        HowToAttack.attack(SecondsToAttack, transform.position, PosInitMouse);
        animator.SetBool("Attacking", false);
    }

    public void InitDash()
    {
        movement.agent.enabled = false;
        direction = (posFinalMouse - PosInitMouse);
        posFinaldash = transform.position + direction.normalized * 3f;
        PosInitDash = transform.position;
        canPass();
        movement.PlayerDashed(direction);
        animator.SetBool("Dash", true);
        animator.SetBool("Walking", false);
        MouseClickedTime = 0;
        Clicks = 0;
        WhenDashStatusStarted = Time.time;
        DashTimer = Time.time;
        canDash = false;
    }

    public void InitMove()
    {
        movement.agent.enabled = true;
        animator.SetBool("Walking", true);
        animator.SetBool("Dash", false);
        movement.screenPos = posToMove;
        movement.PlayerMoved();
        enter = false;
        MouseClickedTime = Time.time;
        Clicks = 0;
    }

    public void EndDash()
    {
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        animator.SetBool("Dash", false);
        this.gameObject.layer = LayerMask.NameToLayer("Default");
        // isDashed = false;
    }
    public void setAnimationDashOrWalk(string TypeMov)
    {
        if (movement.velocity.x > 0)
        {
            if (Mathf.Abs(movement.velocity.x) > Mathf.Abs(movement.velocity.y))
                animator.SetFloat(TypeMov, 0.75f);

            else
            {
                if (movement.velocity.y > 0)
                    animator.SetFloat(TypeMov, 0.5f );
                else
                    animator.SetFloat(TypeMov, 0f );

            }
        }
        else
        {
            if (Mathf.Abs(movement.velocity.x) > Mathf.Abs(movement.velocity.y))
                animator.SetFloat(TypeMov, 0.25f );

            else
            {
                if (movement.velocity.y > 0)
                    animator.SetFloat(TypeMov, 0.5f );
                else
                    animator.SetFloat(TypeMov, 0f );

            }
        }
        if (TypeMov.Equals("BlendDash"))
            Weapon = 1;
    }
    public void setAnimationAttacking()
    {

    }
    public void isEnemyClicked(Vector3 pos)
    {
        pos = Camera.main.WorldToScreenPoint(pos);
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Enemy" || hit.collider.tag == "Boss")
                HowToAttack.ClickedEnemy = true;
            else
                HowToAttack.ClickedEnemy = false;
        }
        else
            HowToAttack.ClickedEnemy = false;

    }
   
    public void canPass()
    {
        int i = 3;
        bool check = false;
        Vector2[] positionsToCheck = new Vector2[4];
        while (i >= 0 && check == false)
        {
            positionsToCheck[i] = transform.position + direction.normalized * i;
            if (Physics2D.OverlapBox(positionsToCheck[i], transform.GetComponent<BoxCollider2D>().size / 2, 0, LayerMask.GetMask("Holes")) == null)
            {
                posFinaldash = positionsToCheck[i];
                check = true;
                //  this.GetComponent<BoxCollider2D>().enabled = false;
                this.gameObject.layer = LayerMask.NameToLayer("PassHoles");
            }
            i--;

        }
        if (check == false)
            posFinaldash = transform.position;






    }
    public void checkIfcanDash()
    {
        if (Time.time - WhenDashStatusStarted > 1f)
            canDash = true;
        
    }
   

}
