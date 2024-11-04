using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{

    [Header("Spawn Limit")]
    public float minumumY = -4.3f;
    public float maximumY = 4.3f;


    [Header("Enemy Prefabs")]
    public GameObject[] asteroidPrefabs;
    public GameObject bossPrefab;
    public GameObject enemyPrefab;


    [Header("Timer")]
    public float invokingTimer = 2f;



    void Update()
    {

    }

    void Start()
    {

        Invoke("SpawnEnemies", invokingTimer);


    }

   
    void SpawnBoss()
    {

        float positionY = 0;
        Vector3 temp = transform.position;
        temp.y = positionY;
        Instantiate(bossPrefab, temp, Quaternion.Euler(0f, 0f, 90f));

    }



    void SpawnEnemies()
    {
        float positionY = Random.Range(minumumY, maximumY);
        Vector3 temp = transform.position;
        temp.y = positionY;

       

        if(Random.Range(0,2) > 0)
        {

            Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)], temp, Quaternion.identity);

        } else {

            Instantiate(enemyPrefab, temp, Quaternion.Euler(0f, 0f, 0f));

        }

        Invoke("SpawnEnemies", invokingTimer);


        if (ScoreScript.scoreValue > 50)
        {

            CancelInvoke("SpawnEnemies");
            SpawnBoss();

        }

      
    }




} //Class


















