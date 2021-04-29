using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    public float minumumY = -4.3f, maximumY = 4.3f;

    public GameObject[] asteroidPrefabs;

    public GameObject enemyPrefab;

    public float timer = 2f;


    

    void Start()
    {

        Invoke("SpawnEnemies", timer);

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
            Instantiate(enemyPrefab, temp, Quaternion.Euler(0f, 0f, 90f));

        }

        Invoke("SpawnEnemies", timer);

      
    }




} //Class


















