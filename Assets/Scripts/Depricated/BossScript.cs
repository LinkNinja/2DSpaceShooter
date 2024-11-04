using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{

    [SerializeField]
    private int bulletsAmount = 10;
    [SerializeField]
    private float startAngle = 90f;
    [SerializeField]
    private float endAngle = 270f;

    private Vector3 bulletMoveDirection;


    private GameManager gameManager;



    Animator playerAnimation;
    public GameObject playerObject;


    public bool isDead = false;


    public AudioSource takeDamageSFX;






    //-----------------------//
    public float bossHealth = 5;
    public float speed = 5f;

    public bool canShoot;
    private bool canMove = true;

    private float verticalSpeed = 2f;
    private bool moveVertical = false;

    public float boundX = -11f;
   

    public Collider2D enemyCollider;
    public Transform attackPoint;
    public GameObject bulletPrefab;
    private AudioSource laserAudio;
    private Animator anim;
    private AudioSource explosionSound;



    void Awake()
    {
        laserAudio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        explosionSound = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //playerAnimation = playerObject.GetComponent<Animator>();


    }


    void Start()
    {

        if (canShoot)
        {
            Invoke("StartShooting", Random.Range(1f, 3f));
            InvokeRepeating("FireBullets", 0f, 2f);
        }
            



    }



    void Update()
    {
        Move();
        if (canMove == false)
        {
            VerticalMovement();
        }
        
    }
    private void FireBullets()
    {

        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector3 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<BulletPatternA>().SetMoveDirection(bulDir);

            angle += angleStep;

        }

    }

    void Move()
    {
        
        if (canMove)
        {
            Vector3 temp = transform.position;
            temp.x -= speed * Time.deltaTime;
            transform.position = temp;

            if (temp.x < 4)
            {
                canMove = false;

           

            }


        }


    
    }

    void VerticalMovement()
    {

        if (transform.position.y > 4f)
        {
            moveVertical = false;

        }
        else if(transform.position.y < -4f)
        {

            moveVertical = true;

        }

        if(moveVertical)
        {
            Vector3 temp = transform.position;
            temp.y += verticalSpeed * Time.deltaTime;
            transform.position = temp;
        }
        else
        {
            Vector3 temp = transform.position;
            temp.y -= verticalSpeed * Time.deltaTime;
            transform.position = temp;
        }

    }



    //Quaternion. identity to spawn at 0000

    void StartShooting()
    {
        //GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, Quaternion.identity);

        bulletPrefab.GetComponent<EnemyBullet>().isEnemyBullet = true;

        if (canShoot)
            Invoke("StartShooting", Random.Range(1f, 3f));


    }


    /// <summary>
    /// created a whole function to turn off game o
    /// </summary>
    void TurnOffGameObject()
    {

        gameObject.SetActive(false);

    }




    private void OnTriggerEnter2D(Collider2D target)
    {

        // Check if enemy collides with Tag "Bullet"
        if (target.tag == "PlayerBullet")
        {
            
            

            //Subtract health
            bossHealth -= 1;

            takeDamageSFX.Play();

            if (bossHealth == 0)
            {
                // Add to score
                ScoreScript.scoreValue += 15;
                // Stop moving
                canMove = false;

                // Turn off enemy Shooting.
                if (canShoot)
                {
                    // Stop Shooting
                    canShoot = false;
                    CancelInvoke("StartShooting");
                    CancelInvoke("FireBullets");

                }

                // Turn off enemy
                Invoke("TurnOffGameObject", 3f);
                enemyCollider.enabled = false;

                // Explosion Sound Effect
                explosionSound.Play();

                // Explosion Animation
                anim.Play("Destroy");
                //playerAnimation.Play("FlyAway");

                isDead = true;

                //gameManager.DialogueText();

                //gameManager.PlayerLeaves();




            }

        }

    }
}
