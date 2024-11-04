using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUpScript : MonoBehaviour
{

    public float speed = 5f;
    public float deactivateTimer = 3f;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("DeactivateGameObject", deactivateTimer);
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }


    private void Move()
    {

        Vector3 temp = transform.position;
        //Smoothing the bullet and moving it
        temp.x -= speed * Time.deltaTime;
        transform.position = temp;

    }



    void DeactivateGameObject()
    {
        //Deactivate the object
        gameObject.SetActive(false);

    }


    private void OnTriggerEnter2D(Collider2D target)
    {

        if (target.tag == "Player")
        {


            gameObject.SetActive(false);

            //testing whats getting hit
            Debug.Log(target.tag);
            Debug.Log("Touching Player with Shield Power Up");

        }


    }

}
