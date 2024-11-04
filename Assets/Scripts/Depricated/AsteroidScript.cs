using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{

    public float speed = 130f;
    public float rotateSpeed = 50f;

    public Collider2D enemyCollider;
    public bool canRotate;

    private bool canMove = true;
    public float boundX = -11f;

    private Animator anim;
    private AudioSource explosionSound;

    public GameObject shieldPickupPrefab;



    void Awake()
    {
        
        anim = GetComponent<Animator>();
        explosionSound = GetComponent<AudioSource>();

    }


    void Start()
    {

        //When the game starts it checks if the asteroid can rotate.
        if (canRotate)
        {

            //Here is the code handling the rotation of the asteroid. It is all generated randomly. 
            if (Random.Range(0, 2) > 0)
            {
                rotateSpeed = Random.Range(rotateSpeed, rotateSpeed + 20f);
                rotateSpeed *= -1f;
            }

        }

    }


    void Update()
    {
        Move();
        RotateEnemy();

    }


    void Move()
    {
        //Checks if the asteroid is able to move. This is set to true in the private bool.
        if (canMove)
        {
            //The way the asteroid moves code. 
            Vector3 temp = transform.position;
            temp.x -= speed * Time.deltaTime;
            transform.position = temp;


            //Here if the asteroid passes a certain point it will deactivate the game object.
            if (temp.x < boundX)
                gameObject.SetActive(false);

        }

    }


    void RotateEnemy()
    {
        //Checks if the asteroid is able to rotate.
        if (canRotate)
        {
            //The rotation code. It includes the vector with the rotateSpeed, using delta time. 
            transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime), Space.World);
        }

    }


    void TurnOffGameObject()
    {
        //The game object is turned off and the active setting is changed to false. This gets used with the collider function.
        gameObject.SetActive(false);

    }


    private void OnTriggerEnter2D(Collider2D target)
    {


        if (target.tag == "PlayerBullet")
        {

            //When the asteroid is shot, it will add 10 points to the score value.
            ScoreScript.scoreValue += 10;

            //When the asteroid is shot, it is no longer able to move.
            canMove = false;
          
            //When asteroid is shot, there is a 3 second delay before the game object is disabled.
            Invoke("TurnOffGameObject", 3f);

            //When the asteroid is shot, the collider on the asteroid is turned off.
            enemyCollider.enabled = false;

            //When the asteroid is shot, the audio plays an explosion sound.
            explosionSound.Play();

            //DEBUG NOTES
            //Debug.Log("explosionsound");

            //When the asteroid is shot, a destroy animation will play.
            anim.Play("Destroy");

            Instantiate(shieldPickupPrefab, transform.position, Quaternion.identity);

        }

    }

}
