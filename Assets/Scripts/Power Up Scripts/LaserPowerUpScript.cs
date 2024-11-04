using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPowerUpScript : MonoBehaviour
{


    //Power Up speed.
    public float speed = 5f;
    //Amount of time to deactivate.
    public float deactivateTimer = 7f;


    // Start is called before the first frame update
    void Start()
    {

        //Deactivate the object by invoking a timer As soon as its created.
        Invoke("DeactivateGameObject", deactivateTimer);


    }

    // Update is called once per frame
    void Update()
    {
        //Call the move function every update.
        Move();

    }


    //Move the Laser Power up function. 
    //This function handles the movement of the laser power up.
    private void Move()
    {
        //Get a temporary vector 3 position. Assign it a temp name.
        Vector3 temp = transform.position;

        //Smoothing the bullet and moving it
        //Take the temp "X" transform postion and move it minus the speed and multiply by Time.DeltaTime.
        temp.x -= speed * Time.deltaTime;

        //Transform the laser power up objects position by assigning it the temp position.
        transform.position = temp;

    }


    //Deactivate Game Object function.
    void DeactivateGameObject()
    {
        //Deactivate the object
        gameObject.SetActive(false);

    }


    //Collider trigger function.
    //This function handles what occures when the laser power up object collides with another object.
    //The other object is tagged and the laser power up will turn off if it collides with the Player tag.
    private void OnTriggerEnter2D(Collider2D target)
    {

        //If target Collider2D touches object Tagged player.
        if (target.tag == "Player")
        {
            //Turn off the LaserPower Up
            gameObject.SetActive(false);  

        }


    }

}
