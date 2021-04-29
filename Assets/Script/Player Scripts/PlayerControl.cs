using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float speed = 5f;
    public float minY, maxY;

    public float attackTimer = 0.35f;

    private float currentAttackTimer;

    private bool canAttack;

    [SerializeField]
    private GameObject playerBullet;


    [SerializeField]
    private Transform attackPoint;

    private AudioSource laserAudio;


    void Awake()
    {

        laserAudio = GetComponent<AudioSource>();


    }

    // Start is called before the first frame update
    void Start()
    {
        currentAttackTimer = attackTimer;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        Attack();
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

    void Attack()
    {

        //here is where we attack and where we are using a timer to restrict the attack timer.
        //Could be upgraded to be used with power ups later on.
        attackTimer += Time.deltaTime;
        if(attackTimer > currentAttackTimer)
        {
            canAttack = true;
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            if(canAttack)
            {
                canAttack = false;
                attackTimer = 0f;

                Instantiate(playerBullet, attackPoint.position, Quaternion.identity);


                //play shooting sound fx

                laserAudio.Play();



            }
        }


    }




} //class















