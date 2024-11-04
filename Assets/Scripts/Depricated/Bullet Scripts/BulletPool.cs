using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    //We get a static variable of the BulletPool script.
    public static BulletPool bulletPoolInstance;

    // Variable that holds the Enement Bullet for the bullet pool.
    [SerializeField]
    private GameObject pooledBullet;

    // Variable to check if there is enought bullets in the pool.
    private bool notEnoughBulletsInPool = true;


    // a List variable is created. Filled with Game objects. THose objects are the Bullets from the pooledBullet.
    private List<GameObject> bullets;

    private void Awake()
    {
        //On awake we assign the Bullet bool script variable a value.
        //The value is ITSELF.
        bulletPoolInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //The bullets variable will be assigned a new List Value of game objects.
        bullets = new List<GameObject>();
    }


    //Function that is being used to get the bullet.
    public GameObject GetBullet()
    {

        //Check if the Bullets list count is greater than 0. 
        if(bullets.Count > 0)
        {
            //For Loop.
            //Check if I is less than the Bullets count list.
            //If it is add 1 to I.
            for (int i = 0; i < bullets.Count; i++)
            {
                //If the bullet[i] is not active in the hieararchy AKA scene. Then continue.
                if (!bullets[i].activeInHierarchy)
                {
                    //Return the bullets to the bullet pool.
                    return bullets[i];
                }
            }
        }


        //Check if there is not enought bullets in the bullet pool.
        if (notEnoughBulletsInPool)
        {
            //New game object variable is created and assigned a new value.
            //The value is an Instantiated pooled Bullet.
            GameObject bul = Instantiate(pooledBullet);

            //
            bul.SetActive(false);
            bullets.Add(bul);
            return bul;
        }

        return null;
    }

}
