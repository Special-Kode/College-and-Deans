using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorPlayerScript: MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    Movement movement;
    GameObject bullet;
    public bool isMoved;
    public bool isDashed;
    private float MouseClickedTime;
    private float touchTime;
    private float ClickDelay;
    private int Clicks;
    private float SecondsToAttack;
    public AttackBehaviour HowToAttack;
    Vector3 posToMove;
    public bool enter;
   public  Vector3 PosInitDash;
    public Vector3  posFinalDash;
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
        isMoved = false;
        SecondsToAttack = 0;
        
    }

    // Aquí se cambian las variables de estado dependeiendo de el estado al cual se quiere llegar partiendo de un estado específico. También se dispone a ejecutar 
    //las distintas animaciones
    void Update()
    {
        if (isDashed == false)
            animator.SetBool("Dash", false);
        else
            animator.SetBool("Dash", true);
        if (isMoved == false)
            animator.SetBool("Walking", false);
        else
            animator.SetBool("Walking", true);
        if (SystemInfo.deviceType == DeviceType.Desktop)
        {
           
            if (Input.GetMouseButtonDown(0))
            {

                    PosInitDash = Input.mousePosition;
                    PosInitDash = Camera.main.ScreenToWorldPoint(PosInitDash);
                    PosInitDash.z = 0;
                
                    
                posToMove = Input.mousePosition;
                posToMove.z = 0;

                Clicks++;
                if (Clicks % 2 != 0)
                {

                    MouseClickedTime = Time.time;


                }
                isEnemyClicked(posToMove);
                if (HowToAttack.ClickedEnemy == true)
                {
                    isMoved = false;

                }

            }
           







            
            if (Input.GetMouseButtonUp(0))
            {
                enter = true;
                posFinalDash = Input.mousePosition;
                    posFinalDash = Camera.main.ScreenToWorldPoint(posFinalDash);


                if (Vector3.Distance(posFinalDash, PosInitDash) > 2 && isDashed == false)
                {
                    movement.InitialPos = transform.position;
                    isDashed = true;
                    isMoved = false;
                    MouseClickedTime = 0;
                    Clicks = 0;
                    
                }


               

            }

            if ((Time.time - MouseClickedTime) <= ClickDelay && Clicks % 2 == 0 && Clicks!=0 && enter==true)
            {
                enter = false;

                if (HowToAttack.ClickedEnemy)
                {
                    HowToAttack.attack(SecondsToAttack, transform.position,Camera.main.ScreenToWorldPoint(posToMove), 0);

                }
                else
                {
                    isMoved = false;
                }

                MouseClickedTime =Time.time;
                Clicks = 0;

            }

            if (Clicks % 2 != 0)
            {
                if ((Time.time - MouseClickedTime) > ClickDelay && !HowToAttack.ClickedEnemy && isDashed == false && enter==true)
                {

                    isMoved = true;
                    setMovement(posToMove);
                    enter = false;
                }

            }

            if((Time.time - MouseClickedTime) > ClickDelay)
            {
                MouseClickedTime = Time.time;
                Clicks = 0;
            }





            if (Vector3.Distance(movement.screenPos, transform.position) <= 0.1f && this.GetComponent<Rigidbody2D>().velocity != Vector2.zero && isMoved ==true)
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                isMoved = false;
            }

            if (Vector3.Distance(transform.position, movement.InitialPos) > 2f && this.GetComponent<Rigidbody2D>().velocity!=Vector2.zero && isDashed==true)
            {
                this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                movement.distanceDashed = 0;
                isDashed = false;
            }





        }
        else if (SystemInfo.deviceType == DeviceType.Handheld)
        {

            posToMove = Input.GetTouch(0).position;
            posToMove.z = 0;

            if (Input.touchCount % 2 != 0)
            {

                touchTime = Time.time;


            }
            isEnemyClicked(posToMove);
            if (HowToAttack.ClickedEnemy == true)
            {
                isMoved = false;

            }


            if ((Time.time - touchTime) < ClickDelay)
            {

                if (Input.touchCount % 2 == 0)
                {
                    if (HowToAttack.ClickedEnemy)
                    {
                        HowToAttack.attack(SecondsToAttack, transform.position, Camera.main.ScreenToWorldPoint(posToMove), 0);
                    }
                    else
                    {
                        isMoved = false;
                    }


                }

            }

















            if (this.gameObject.GetComponentInChildren<ExternMechanicsPlayer>().death == true)
            {
                animator.SetBool("Death", true);
                /* if (animator.GetCurrentAnimatorStateInfo(0).IsTag("DeathTag"))
                     UnityEditor.EditorApplication.isPlaying = false;*/

            }






        }
    }



    void setMovement(Vector3 positionStart)
    {
        movement.positionToMove = positionStart;
        movement.screenPos = Camera.main.ScreenToWorldPoint(new Vector3(movement.positionToMove.x, movement.positionToMove.y, 0));
        movement.direction = movement.screenPos - transform.position;
    }


    public void SetAttack()
    {
        animator.SetBool("Attacking", true);
        SecondsToAttack = Time.time;
        isMoved = false;
        isDashed = false;
    }

    public void isEnemyClicked(Vector3 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Enemy")
                HowToAttack.ClickedEnemy = true;
            else
                HowToAttack.ClickedEnemy = false;
        }
        else
            HowToAttack.ClickedEnemy = false;

    }
    public void WhereToLook(Vector3 screenPos)
    {

        
        float angle = Mathf.Atan2((screenPos.y - transform.position.y), (screenPos.x - transform.position.x))*Mathf.Rad2Deg;
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
        else if(angle<= 112.5f && angle > 67.5f)
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
    
}
