using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    [SerializeField] private int weaponDamage;
    private bool isMobile;
    void Start()
    {
        Clicks = 0;
        ClickDelay = 0.3f;
        animator = GetComponent<Animator>();
        movement = this.GetComponent<Movement>();
        /**
        HowToAttack = this.GetComponent<AttackBehaviour>();
        Weapons = GetComponent<Modifiers>();
        Weapons.Init();
        HowToAttack.SetWeapon(Weapons.modifiers[0]);
        //*/
        SecondsToAttack = 0;
        canDash = true;
        isMobile = Application.isMobilePlatform;
    }

    // Aquí se cambian las variables de estado dependeiendo de el estado al cual se quiere llegar partiendo de un estado específico. También se dispone a ejecutar 
    //las distintas animaciones
    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            //Control movil
            if (isMobile)
            {

                ClickDown();
                //if you stop pressing left click, it is saved the position of the mouse, and check if distance of init dash and end dash is higher than 2
                ClickUp();
                //if the user press left click, there might be the possibility to the user press second click, if this not happen, the player attack if the user input click an enemy.
                CheckIfStartToMove();
                

                //  Debug.Log(this.GetComponent<Rigidbody2D>().velocity);
                if ((Vector2.Distance(posFinaldash, transform.position) < 0.1f || (Time.time - DashTimer) > 0.5f) && animator.GetBool("Dash")) //&& isDashed==true                                                                        )
                    EndDash();

                if (canDash == false)
                    checkIfcanDash();

                if (this.gameObject.GetComponentInChildren<ExternMechanicsPlayer>().death == true)
                {
                    animator.SetBool("Death", true);
                }
            }
            //ControlOrdenador
            else
            {
                MovementWASD();
                ClickDown();
                //if you stop pressing left click, it is saved the position of the mouse, and check if distance of init dash and end dash is higher than 2
                ClickUp();
                //if the user press left click, there might be the possibility to the user press second click, if this not happen, the player attack if the user input click an enemy.
                resetClicks();

                if ((Vector2.Distance(posFinaldash, transform.position) < 0.1f || (Time.time - DashTimer) > 0.5f) && animator.GetBool("Dash")) //&& isDashed==true                                                                        )
                    EndDash();

                if (canDash == false)
                    checkIfcanDash();

                if (this.gameObject.GetComponentInChildren<ExternMechanicsPlayer>().death == true)
                {
                    animator.SetBool("Death", true);
                }


            }
            //if you press left click, clicks is added 1 and it is saved the time
            

        } else
        {
            canDash = false;
            Clicks = 0;
        }

    }
    private void LateUpdate()
    {

        if (!PauseMenu.GameIsPaused)
        {
            if (animator.GetBool("Walking"))
                setAnimationDashOrWalk("BlendWalking");
            else if (animator.GetBool("Dash"))
                setAnimationDashOrWalk("BlendDash");
            if (animator.GetBool("Attacking"))
                WhereToLook(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            /*  else
                  setAnimationIdle();
            */
        }
    }


    public void Attack()
    {
        animator.SetBool("Attacking", true);
        SecondsToAttack = Time.time;
        HowToAttack.SetWeapon(Weapons.modifiers[NumModifier]);
        HowToAttack.attack(SecondsToAttack, transform.position, PosInitMouse);
    }

    public void InitDash()
    {
        direction = (posFinalMouse - PosInitMouse);
        posFinaldash = transform.position + direction.normalized * 3f;
        PosInitDash = transform.position;
        if (canPass())
        {
            movement.agent.enabled = false;
            movement.PlayerDashed(direction);
            animator.SetBool("Dash", true);
            animator.SetBool("Walking", false);
            MouseClickedTime = 0;
            Clicks = 0;
            WhenDashStatusStarted = Time.time;
            DashTimer = Time.time;
            canDash = false;
        }
        else
        {
            Clicks = 0;
        }
           
        
    }

    public void InitMove()
    {
        movement.agent.enabled = true;
        animator.SetBool("Walking", true);
        animator.SetBool("Dash", false);
        movement.screenPos = posToMove;
        movement.PlayerMoved(true);
        enter = false;
        MouseClickedTime = Time.time;
        Clicks = 0;
    }

    public void EndDash()
    {
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        animator.SetBool("Dash", false);
        this.gameObject.layer = LayerMask.NameToLayer("Player");
        movement.dashing = false;
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
        animator.SetFloat("BlendIdle", animator.GetFloat(TypeMov));
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
   
    public bool canPass()
    {
    Collider2D collider;
        int i = 0;
        bool check = false;
        Vector2[] positionsToCheck = new Vector2[4];
        while (i < 3)
        {
            positionsToCheck[i] = transform.position + direction.normalized * (i+1);
            collider = Physics2D.OverlapBox(positionsToCheck[i], transform.GetComponent<BoxCollider2D>().size / 2, 0, LayerMask.GetMask("Holes", "Enemy","Walls"));

            if (collider == null)
            {
                posFinaldash = positionsToCheck[i];
                //  this.GetComponent<BoxCollider2D>().enabled = false;
                this.gameObject.layer = LayerMask.NameToLayer("PassHoles");
                check = true;
            }
            else
            {
                if (collider.gameObject.layer == 9)
                    i = 3;
            }
            i++;
        }
        if(check==true)
            return true;
        else
            return false;






    }
    public void checkIfcanDash()
    {
        if (Time.time - WhenDashStatusStarted > 0.5f)
            canDash = true;
        
    }
    public void WhereToLook(Vector3 screenPos)
    {


        float angle = Mathf.Atan2((screenPos.y - transform.position.y), (screenPos.x - transform.position.x)) * Mathf.Rad2Deg;
        if (screenPos.y <= transform.position.y)
        {
            angle += 360;
        }

        float baseValue = 45f;
        float multiplier = 90f;

        //derecha
        if (angle <= baseValue || angle > baseValue + multiplier * 3)
        {
            animator.SetFloat("BlendAttacking", 0.75f);
        }

 

        //arriba
        else if (angle <= baseValue + multiplier && angle > baseValue)
        {
            animator.SetFloat("BlendAttacking", 0.5f);
        }




        //izquierda
        else if (angle <= baseValue + multiplier * 2 && angle >= baseValue + multiplier)
        {
          animator.SetFloat("BlendAttacking", 0.25f);
        }



        //abajo
        else if (angle <= baseValue + multiplier * 3 && angle > baseValue + multiplier * 2)
        {
           animator.SetFloat("BlendAttacking", 0);
        }
        animator.SetFloat("BlendIdle", animator.GetFloat("BlendAttacking"));

      
    }
    public void InitWeapon(int weaponId)
    {
        HowToAttack = this.GetComponent<AttackBehaviour>();
        Weapons = GetComponent<Modifiers>();
        Weapons.Init();
        HowToAttack.SetWeapon(Weapons.modifiers[weaponId]);
        NumModifier = weaponId;
        weaponDamage = HowToAttack.getWeapon().GetDamageMultiplier();
        Weapons.modifiers[weaponId].SetDamageMultiplier(weaponDamage);
    }

    public void UpdateWeapon(int weaponId)
    {
        HowToAttack = this.GetComponent<AttackBehaviour>();
        Weapons = GetComponent<Modifiers>();
        weaponDamage = HowToAttack.getWeapon().GetDamageMultiplier();
        HowToAttack.SetWeapon(Weapons.modifiers[weaponId]);
        NumModifier = weaponId;
        Weapons.modifiers[weaponId].SetDamageMultiplier(weaponDamage);
    }

    public void ClickDown()
    {
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
    }

    public void ClickUp()
    {
        if (Input.GetMouseButtonUp(0))
        {

            posFinalMouse = Input.mousePosition;
            posFinalMouse = Camera.main.ScreenToWorldPoint(posFinalMouse);


            if (Vector2.Distance(posFinalMouse, PosInitMouse) > 1.5f && canDash == true
                 && !animator.GetBool("Dash"))
            {
                InitDash();
            }

            // if is not dashing,it means that might player can move
            else if (Clicks == 2 && !animator.GetBool("Dash") && GameObject.FindGameObjectWithTag("Bullet") == null && GameObject.FindGameObjectWithTag("Bomb") == null)
            {
                Attack();
                Clicks = 0;
                MouseClickedTime = 0;
            }



        }
    }
    public void CheckIfStartToMove()
    {
        if ((Time.time - MouseClickedTime) > ClickDelay && !animator.GetBool("Dash") && Vector3.Distance(posFinalMouse, PosInitMouse) < 1.5)
        {


            if (Clicks == 1)
                InitMove();


            Clicks = 0;
            MouseClickedTime = 0;
        }
    }
    void resetClicks()
    {
        if ((Time.time - MouseClickedTime) > ClickDelay)
        {
            Clicks = 0;
            MouseClickedTime = 0;
        }
    }
    void MovementWASD()
    {
        animator.SetBool("Walking", true);
        movement.PlayerMoved(false);
    }
}
