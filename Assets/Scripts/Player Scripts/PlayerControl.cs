using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    

    public ShieldBar shieldBar;
    [Header("HealthStats")]
    public float maxShield = 100f;
    public float currentShield;
    public float damage;
    public int playerLives = 2;

    [Header("MovementStats")]
    public float speed = 5f;
    public float minY, maxY;
    private float vertical;
    public Rigidbody2D rb;



    [Header("AttackStats")]
    public float attackTimer;
    [SerializeField]
    private float currentAttackTimer;
    [SerializeField]
    private bool canAttack;
    [SerializeField]
    private bool laserPickup;
    [SerializeField]
    private bool shotIsLeft;

    [Header("Audio")]
    [SerializeField]
    private AudioSource laserAudio;
    [SerializeField]
    private AudioSource explosionSound;
    [SerializeField]
    private AudioSource shieldPickup;
    [SerializeField]
    private AudioSource dialogue;
    [SerializeField]
    private AudioSource takeDamage;

    [Header("PlayerObject")]
    [SerializeField]
    private GameObject playerBullet;
    [SerializeField]
    private Transform attackPointL;
    [SerializeField]
    private Transform attackPointR;
    public Collider2D playerCollider;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameManager gameManager;

    //private bool gamePaused;
    void Awake()
    {
        //Get Animator component when the player wakes up.
        anim = GetComponent<Animator>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //At the start of the scene. Players Current shield is equal to the maxShield value.
        currentShield = maxShield;
        //The Shieldbar UI compononet assignes the value of the maxshield to itself. 
        shieldBar.SetMaxShields(maxShield);
        //At the start of the scene. The players current attack timer is equal to the attack timer value.
        currentAttackTimer = attackTimer;
        //The player object will look for the gameobject in the scene called "GameManager", it then gets the components from that object.
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //Used for Dual barrel alternating shooting. The left barrel shot is set to true.
        shotIsLeft = true;

    }

    // Update is called once per frame
    void Update()
    {
        //rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        //rb.transform.position = new Vector2(vertical * speed, rb.transform.position.y);


        
            AttackMode2();
            //Run the MovePlayer function every frame.
          
              MovePlayer();
            



        
    }


    
    void MovePlayer()
    {

        //Check the keyboard input
        if (Input.GetAxisRaw("Vertical") > 0f)
        {

            Vector3 temp = transform.position;
            temp.y += speed * Time.deltaTime;

            if (temp.y > maxY)
                temp.y = maxY;

            transform.position = temp;

        }

        //Check Keyboard input
        else if (Input.GetAxisRaw("Vertical") <0f)
        {
            Vector3 temp = transform.position;
            temp.y -= speed * Time.deltaTime;
            if (temp.y < minY)
                temp.y = minY;
            transform.position = temp;
        }

    }


    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rb.velocity = new Vector2(rb.velocity.y,vertical);
        }


    }

    void AttackMode2()
    {

        //Check if laser has been picked up.
        if (laserPickup == false)
        {
            //Attack timer is incremented by Time.Deltatime.
            attackTimer += Time.deltaTime;

            //Check if the attacktimer is greater than the Curren attack timer.
            if (attackTimer > currentAttackTimer)
            {
                
                //Check if player collider is enabled.
                if (playerCollider.enabled == true)
                {
                    //Enable canAttack
                    canAttack = true;
                }

            }
            //Check Keyboard input.
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightControl))
            {
                Debug.Log("Shooting Single");
                //Check if can attack is enabled.
                if (canAttack && shotIsLeft == true)
                {
                    //Disable can attack
                    canAttack = false;

                    //Set the attack timer
                    //When a shot is fired the attackTimer is reset to 1f.
                    attackTimer = .15f;

                    //Create player bullet.
                    Instantiate(playerBullet, attackPointL.position, Quaternion.identity);
                    shotIsLeft = false;

                    // Play laser Sound Effect
                    laserAudio.Play();

                }
                if(canAttack && shotIsLeft == false)
                {
                    //Set the attack timer
                    //When a shot is fired the attackTimer is reset to 1f.
                    attackTimer = .15f;
                    //Fire right barrell
                    Instantiate(playerBullet, attackPointR.position, Quaternion.identity);
                    shotIsLeft = true;
                    laserAudio.Play();
                    canAttack = false;
                }
            }
        }

        else
        {
            attackTimer += Time.deltaTime;
            if (attackTimer > currentAttackTimer)
            {
                if (playerCollider.enabled == true)
                {
                    canAttack = true;
                }
            }
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.RightControl))
            {
                if (canAttack)
                {

                    canAttack = false;
                    Debug.Log("Shooting multiple");

                    //Set the attack timer
                    attackTimer = .15f;

                    //Spawn player bullet from Left turret
                    Instantiate(playerBullet, attackPointL.position, Quaternion.identity);


                    //Spawn player bullet from Right turret
                    Instantiate(playerBullet, attackPointR.position, Quaternion.identity);

                    //Laser SFX
                    laserAudio.Play();

                }
            }
        }
    }











    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D target)
    {
        //Take damage from a collision
        damage = Random.Range(10, 20);

        //Check the colliding objects tags
        if (target.tag == "Bullet" || target.tag == "Enemy" || target.tag == "BulletPattern")
        {
            
            //Current shield is reduced from damage.
            currentShield -= damage;

            //Damage SFX
            takeDamage.Play();

            //anim.Play("FlyAway");
            //UI Shieldbar set to current shield bar amount on Player
            shieldBar.SetShield(currentShield);


            anim.Play("TakeDamage");

            //Check if players shield is below 0
            if (currentShield <= 0)
            {
                //Turn off can attack
                canAttack = false;
                
                //Turn off game object after 3 seconds.
                Invoke("TurnOffGameObject", 3f);

                //Turn off players collider.
                playerCollider.enabled = false;
             
                //Explosion SFX
                explosionSound.Play();

                //Explosion Animation
                anim.Play("Destroy");

                //Game over in game manager.
                //gameManager.GameOver();
                
                
            }
            
        }

        //Check if the colliding object is tagged SHIELDS
        if(target.tag == "ShieldRecharge")
        {
            //Add to shield health
            currentShield += 10;

            //UI Shield bar is increased
            shieldBar.SetShield(currentShield);


            //Debug.Log("POWER UP SHIELDS!");
        }

        //Check if the colliding object is tagged LASER
        if (target.tag == "LaserPowerUp")
        {
            //Turn on laser Pick up
            laserPickup = true;

            //Debug.Log(laserPickup);
        }

    }

} //class















