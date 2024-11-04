using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float speed = 5f;
    public float enemySpeed = 5f;
    public float deactivateTimer = 3f;
    public bool isEnemyBullet = false;
    private GameManager gameManager;


    void Start()
    {
        //Enemt Bullet Speed
        if (isEnemyBullet)
            speed *= -2f;
        //Players bullet speed
        if(!isEnemyBullet)
            speed *= 3f;

        Invoke("DeactivateGameObject", deactivateTimer);

        
    }


    void Update()
    {
        Move();
    }

    private void Move()
    {

        Vector3 temp = transform.position;
        //Smoothing the bullet and moving it
        temp.x += speed * Time.deltaTime;
        transform.position = temp;

    }

   

    void DeactivateGameObject()
    {
        //Deactivate the object
        gameObject.SetActive(false);

    }


    
    private void OnTriggerEnter2D(Collider2D target)
    {
  
        if(target.tag == "Bullet" || target.tag == "Enemy")
        {

            gameObject.SetActive(false);
            //testing whats getting hit
            Debug.Log(target.tag);
            Debug.Log("Hitting");
            
        }

  
    }

}//class







