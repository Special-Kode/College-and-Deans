using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    Movement movement;
    GameObject bullet;
    public Collisions collision;
    private float MouseClickedTime, touchTime, ClickDelay,SecondsToAttack, WhenDashStatusStarted,DashTimer;
    private int Clicks;
    public AttackBehaviour HowToAttack;
    Vector3 posToMove;
    public bool enter,canDash;
    public Vector3 PosInitMouse, posFinalMouse, PosInitDash, posFinaldash;
    Vector3 direction;

    void Start()
    {
        Clicks = 0;
        ClickDelay = 0.25f;
        animator = GetComponent<Animator>();
        movement = this.GetComponent<Movement>();
        bullet = GameObject.FindGameObjectWithTag("Bullet");
        HowToAttack = this.GetComponent<AttackBehaviour>();
        Weapon gun = new Weapon("Lapiz", 20, "Gun");
        HowToAttack.SetWeapon(gun);
        SecondsToAttack = 0;
        canDash = true;
    }

    // Aquí se cambian las variables de estado dependeiendo de el estado al cual se quiere llegar partiendo de un estado específico. También se dispone a ejecutar 
    //las distintas animaciones
    void Update()
    {


        if (SystemInfo.deviceType == DeviceType.Desktop)
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
            if (Input.GetMouseButtonUp(0))
            {

                posFinalMouse = Input.mousePosition;
                posFinalMouse = Camera.main.ScreenToWorldPoint(posFinalMouse);



                if (Vector3.Distance(posFinalMouse, PosInitMouse) > 2 && canDash==true
                     && !animator.GetBool("Dash"))
                {
                    movement.agent.enabled = false;
                    direction = (posFinalMouse - PosInitMouse);
                    posFinaldash = transform.position + direction.normalized * 3f;
                    PosInitDash = transform.position;
                    canPass();
                    movement.PlayerDashed(direction);
                    animator.SetBool("Dash", true);
                    animator.SetBool("Walking", false);
                    movement.InitialPos = transform.position;
                    MouseClickedTime = 0;
                    Clicks = 0;
                    WhenDashStatusStarted = Time.time;
                    DashTimer = Time.time;
                    canDash = false;

                }



                // if is not dashing,it means that might player can move
                else if (Clicks % 2 == 0 && Clicks != 0)
                {
                    //  isMoved = true;
                    movement.agent.enabled = true;
                    animator.SetBool("Walking", true);
                    animator.SetBool("Dash", false);
                    setMovement();
                    movement.PlayerMoved();
                    enter = false;
                    MouseClickedTime = Time.time;
                    Clicks = 0;

                }



            }
            //if the user press left click, there might be the possibility to the user press second click, if this not happen, the player attack if the user input click an enemy.

            if ((Time.time - MouseClickedTime) > ClickDelay && Clicks == 1)
            {
                isEnemyClicked(posToMove);

                if (HowToAttack.ClickedEnemy)
                {
                    HowToAttack.attack(SecondsToAttack, transform.position, Camera.main.ScreenToWorldPoint(posToMove), 0);

                }
                Clicks = 0;
                MouseClickedTime = 0;
            }


            
              //  Debug.Log(this.GetComponent<Rigidbody2D>().velocity);
            if (Vector2.Distance(posFinaldash,transform.position)<0.1f && animator.GetBool("Dash")) //&& isDashed==true                                                                        )
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                movement.distanceDashed = 0;
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                animator.SetBool("Dash", false);
                this.gameObject.layer = LayerMask.NameToLayer("Default");
                // isDashed = false;
            }



        }

        if (canDash == false)
            checkIfcanDash();




        if (this.gameObject.GetComponentInChildren<ExternMechanicsPlayer>().death == true)
        {
            animator.SetBool("Death", true);
            /* if (animator.GetCurrentAnimatorStateInfo(0).IsTag("DeathTag"))
                 UnityEditor.EditorApplication.isPlaying = false;*/

        }




    }




    void setMovement()
    {
        movement.screenPos = posToMove;
    }


    public void SetAttack()
    {
        animator.SetBool("Attacking", true);
        SecondsToAttack = Time.time;
    }

    public void isEnemyClicked(Vector3 pos)
    {
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
    public void WhereToLook(Vector3 screenPos)
    {


        float angle = Mathf.Atan2((screenPos.y - transform.position.y), (screenPos.x - transform.position.x)) * Mathf.Rad2Deg;
        if (screenPos.y <= transform.position.y)
        {
            angle += 360;
        }
        //derecha
        if (angle <= 22.5f)
        {
            if (animator.GetBool("Walking"))
                animator.SetFloat("BlendWalking", 0.25f);
            else if (!animator.GetBool("Walking") && !animator.GetBool("Attacking"))
                animator.SetFloat("BlendIdle", 0.25f);
            else
                animator.SetFloat("BlendAttacking", 0.25f);
        }

        // else if(angle <= 3/8*Mathf.PI && angle >= -Mathf.PI / 8)


        //arriba
        else if (angle <= 112.5f && angle > 67.5f)
        {
            if (animator.GetBool("Walking"))
                animator.SetFloat("BlendWalking", 0f);
            else if (!animator.GetBool("Walking") && !animator.GetBool("Attacking"))
                animator.SetFloat("BlendIdle", 0f);
            else
                animator.SetFloat("BlendAttacking", 0);

        }

        //else if (angle <= 7 / 8 * Mathf.PI && angle > 5 / 8 * Mathf.PI)



        //izquierda
        else if (angle <= 202.5f && angle >= 157.5f)
        {
            if (animator.GetBool("Walking"))
                animator.SetFloat("BlendWalking", 0.75f);
            else if (!animator.GetBool("Walking") && !animator.GetBool("Attacking"))
                animator.SetFloat("BlendIdle", 0.75f);
            else
                animator.SetFloat("BlendAttacking", 0.75f);
        }

        //else if (angle <= 1 + (3 / 8) * Mathf.PI && angle > 1 + (1 / 8) * Mathf.PI)


        //abajo
        else if (angle <= 292.5f && angle > 247.5f)
        {
            if (animator.GetBool("Walking"))
                animator.SetFloat("BlendWalking", 0.5f);
            else if (!animator.GetBool("Walking") && !animator.GetBool("Attacking"))
                animator.SetFloat("BlendIdle", 0.5f);
            else
                animator.SetFloat("BlendAttacking", 0.5f);
        }

        //else if (angle <= 1 + (7 / 8) * Mathf.PI && angle > 1 + (5 / 8) * Mathf.PI)

        //Sea d el segmento ab

        //  if (screenPos.x>0&&screenPos.y>0)
        /*
            this.gameObject.transform.rotation=Quaternion.Euler(0,0,Mathf.Atan2((screenPos.y - transform.position.y), (screenPos.x - transform.position.x)) * Mathf.Rad2Deg);
        */

    }
    public void canPass()
    {
        int i = 3;
        bool check = false;
        Vector2[] positionsToCheck = new Vector2[4];
        while (i > 0 && check == false)
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
        if (Time.time - WhenDashStatusStarted > 2f)
            canDash = true;
        
    }
   

}
