using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{



    public float speed = 5f;
    public float rotateSpeed = 50f;

    public bool canShoot;
    public bool canRotate;
    private bool canMove = true;

    public float boundX = -11f;

    //
    public Transform attackPoint;

    public GameObject bulletPrefab;

    private Animator anim;

    private AudioSource explosionSound;


    
    void Awake()
    {
        anim = GetComponent<Animator>();
        explosionSound = GetComponent<AudioSource>();




    }


    void Start()
    {
        if (canRotate)
        {
            if(Random.Range(0,2) > 0)
            {
                rotateSpeed = Random.Range(rotateSpeed, rotateSpeed + 20f);
                rotateSpeed *= -1f;
            }



        }

        if (canShoot)
            Invoke("StartShooting", Random.Range(1f, 3f));



    }



    void Update()
    {
        Move();

        RotateEnemy();


    }

    void Move()
    {

        if (canMove)
        {
            Vector3 temp = transform.position;
            temp.x -= speed * Time.deltaTime;
            transform.position = temp;

            if (temp.x < boundX)
                gameObject.SetActive(false);

        }

    }


    void RotateEnemy()
    {
        if (canRotate)
        {

            transform.Rotate(new Vector3 (0, 0, rotateSpeed * Time.deltaTime), Space.World);



        }

    }

    //Quaternion. identity to spawn at 0000

    void StartShooting()
    {
        GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, Quaternion.identity);

        bulletPrefab.GetComponent<BulletScript>().isEnemyBullet = true;

        if (canShoot)
            Invoke("StartShooting", Random.Range(1f, 3f));

        
    }


    void TurnOffGameObject()
    {

        gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D target)
    {
     
        
        if(target.tag == "Bullet")
        {
            canMove = false;
            if (canShoot)
            {
                canShoot = false;
                //Stop the enemy from shooting with the start shoot function
                CancelInvoke("StartShooting");

            }

            Invoke("TurnOffGameObject", 3f);


            //play explosion sound
            explosionSound.Play();

            //Play Explosion animation

            anim.Play("Destroy");

        }

    }








}

