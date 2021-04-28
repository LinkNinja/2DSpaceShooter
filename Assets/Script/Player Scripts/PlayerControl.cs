using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float speed = 5f;
    public float minY, maxY;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }


    void MovePlayer()
    {

        if (Input.GetAxisRaw("Vertical") > 0f)
        {

            Vector3 temp = transform.position;
            temp.y += speed * Time.deltaTime;


            if (temp.y > maxY)
                temp.y = maxY;

            transform.position = temp;

        } else if (Input.GetAxisRaw("Vertical") <0f)
        {

            Vector3 temp = transform.position;
            temp.y -= speed * Time.deltaTime;

            if (temp.y < minY)
                temp.y = minY;

            transform.position = temp;

        }

    }

} //class















