using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {


        Player_Projectile bullet = collision.GetComponent<Player_Projectile>();

        if (bullet != null)
        {
            Destroy(gameObject);
            //Destroy(bullet.gameObject);
            FindObjectOfType<AudioManager>().Play("EnemyDead");
        }

    }

}
