using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefab;
        public int enemyCount;
        public float spawnRate;
        public Transform spawnPoint;
    }

    public Wave[] waves;
    public Transform bossSpawnPoint;
    public GameObject bossPrefab;
    private int currentWaveIndex = 0;
    public float timeBetweenWaves = 5f;
    private bool spawning = false;
    private List<GameObject> activeEnemies = new List<GameObject>();
    public NewDialogueSystem dialogueSystem;
    public LevelChanger nextLevel;
    public ObjectScaler objectScaler;

    void Start()
    {
        StartCoroutine(StartDialogue());
    }

    void Update()
    {
        if (!spawning)
        {
            if (currentWaveIndex < waves.Length)
            {
                StartCoroutine(SpawnWave(waves[currentWaveIndex]));
                spawning = true;
            }
            else if (currentWaveIndex == waves.Length)
            {
                // Uncomment to automatically spawn the boss after the last wave
                // StartCoroutine(SpawnBoss());
                // spawning = true;
            }
        }
    }

    IEnumerator SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.enemyCount; i++)
        {
            GameObject enemy = Instantiate(wave.enemyPrefab, wave.spawnPoint.position, wave.spawnPoint.rotation);
            objectScaler.AdjustObjectPosition(enemy.transform);
            objectScaler.AdjustObjectSize(enemy);
            activeEnemies.Add(enemy);
            yield return new WaitForSeconds(1f / wave.spawnRate);
        }
        yield return new WaitForSeconds(timeBetweenWaves);
        if (currentWaveIndex < waves.Length - 1)
        {
            spawning = false;
            currentWaveIndex++;
        }
        else
        {
            // Debug.Log("All waves completed!");
            StartCoroutine(SpawnBoss());
        }
    }

    IEnumerator SpawnBoss()
    {
        dialogueSystem.TriggerDialogueAtBoss();
        GameObject boss = Instantiate(bossPrefab, bossSpawnPoint.position, bossSpawnPoint.rotation);
        objectScaler.AdjustObjectPosition(boss.transform); // Adjust boss position
        objectScaler.AdjustObjectSize(boss); // Adjust boss size
        activeEnemies.Add(boss);
        yield return new WaitUntil(() => activeEnemies.Count == 0);
        Debug.Log("Boss defeated!");
        nextLevel.FadeToNextLevel();
    }

    public void NotifyEnemyDestroyed(GameObject enemy)
    {
        activeEnemies.Remove(enemy);
        Debug.Log("Enemy destroyed. Remaining enemies: " + activeEnemies.Count);
    }

    IEnumerator StartDialogue()
    {
        yield return new WaitForSeconds(1f);
        dialogueSystem.TriggerDialogueAtStart();
    }
}
