using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [Header("HealthStats")]
    public float enemyHealth = 5;

    [Header("Movement")]
    [SerializeField]
    private bool canMove = true;
    public float speed = 5f;
    public float boundX = -11f;

    [Header("Shooting")]
    public bool canShoot;
    public bool canRotate;
    
    
    public Collider2D enemyCollider;
    public Transform attackPoint;
    public GameObject bulletPrefab;

    private AudioSource laserAudio;
    private Animator anim;
    private AudioSource explosionSound;


    public AudioSource takeDamageSFX;

    
    void Awake()
    {
        laserAudio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        explosionSound = GetComponent<AudioSource>();

    }


    void Start()
    {
       

        if (canShoot)
            Invoke("StartShooting", Random.Range(1f, 3f));

    }



    void Update()
    {
        Move();
    }


    //Slow them Down shoot more and more health. 


    //Movement Function. 
    /// <summary>
    // All the movement for the droid is done here.
    /// </summary>
    void Move()
    {

        //Check if the Droid fighter can move.
        if (canMove)
        {
            
            Vector3 temp = transform.position;
            temp.x -= speed * Time.deltaTime;
            transform.position = temp;

            //If the Droid fighter goes farther then the set X bound then it will deactivate.
            if (temp.x < boundX)
                gameObject.SetActive(false);

        }

    }

    //Shooting Function.
    /// <summary>
    // All the Shooting for the droid is done here.
    /// </summary>
    void StartShooting()
    {

        //Instantiate/Spawn an enemy bullet from the fire point
        Instantiate(bulletPrefab, attackPoint.position, Quaternion.identity);

        //When the bullet is spawned. It changed the bullet prefab to Enemey bullet true.
        bulletPrefab.GetComponent<EnemyBullet>().isEnemyBullet = true;

        //Check if the Droid fighter can shoot.
        if (canShoot)

            //Invoke the function inside of itself to shoot over and over between a random float range of 1-3.
            Invoke("StartShooting", Random.Range(0f, 1.5f));

        
    }


    // Function to Turn off the Game Object
    /// <summary>
    /// Turn off the game object.
    /// </summary>
    void TurnOffGameObject()
    {
        //Game object is turned off. NOT DESTROYED.
        gameObject.SetActive(false);

    }

    //Colliding Function.
    /// <summary>
    /// This is the function that handles if all the collisiong regarding what is hitting the Droid fighter.
    /// </summary>
    /// <param name="target"></param>
    private void OnTriggerEnter2D(Collider2D target)
    {
     
        //Check if Droid is colliding with a target tagged "Bullet".
        if(target.tag == "PlayerBullet")
        {
            //Reduce Enemy Health by 1.
            enemyHealth -= 1;
            takeDamageSFX.Play();
            //Check if Droid fighter Health is 0
            if (enemyHealth == 0)
            {
                //Add 10 to the UI score.
                ScoreScript.scoreValue += 10;

                //Droid can not move anymore.
                canMove = false;

                //Check if the Droid is able to shoot. If it can. turn it off.
                if (canShoot)
                {
                    //Turn off the Droids ability to shoot.
                    canShoot = false;

                    //Cancel Invoke.
                    CancelInvoke("StartShooting");

                }

                //Invoke the Turn Off Game Object function after a certain amount of time.
                Invoke("TurnOffGameObject", 3f);

                //Turn off the Droid Collider.
                enemyCollider.enabled = false;

                //Explosion SFX
                explosionSound.Play();

                //Explosion Animation
                anim.Play("Destroy");
            }

        }

    }








}

