using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPatternA : MonoBehaviour
{

    //Vector 3 Variable for the direction of bullets
    private Vector3 moveDirection;


    // Float variable for bullet movement speed.
    private float moveSpeed;


    //When the object is enabled. We will call a function.
    private void OnEnable()
    {
        //Call the invoke function with the values inserted. The values are the OnDestroy Function and the amount of time to call that function.
        Invoke("OnDestroy", 3f);
    }


    // Start is called before the first frame update
    void Start()
    {
        //At the start set its movement speed to 5f.
        moveSpeed = 5f;
        
    }

    // Update is called once per frame
    void Update()
    {

        //Every frame move the object using the transform.
        //The values inserted for the translate (direction * speed * Time.deltatime)
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }


    // Function to set the bullets Move Direction.
    public void SetMoveDirection(Vector3 dir)
    {
        //We take the previously made moveDirection variable and assign it the functions Dir variable.
        moveDirection = dir;
    }


    // Function to deactivate the game object.
    private void OnDestroy()
    {
        gameObject.SetActive(false);
    }


    // Function to cancel the invoke method.
    private void OnDisable()
    {
        CancelInvoke();
    }

}
