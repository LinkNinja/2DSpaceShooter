using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public float speed = 5f;
    public float deactivateTimer = 3f;


    [HideInInspector]
    public bool isEnemyBullet = false;

    
    void Start()
    {
        //test if enemy bullet
        if (isEnemyBullet)
            speed *= -1f;



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

        //Destroy the object. Find out if you should use both, or if one is better than the other. why not just use destroy all the time?. the bullets not coming back right?
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        
        if(target.tag == "Bullet" || target.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }

        



    }

}//class







