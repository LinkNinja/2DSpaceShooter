using UnityEngine;

public class PositionManager : MonoBehaviour
{
    public Camera mainCamera; // Main Camera reference
    public Transform playerTransform; // Player Transform reference
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public Transform bossSpawnPoint;

    void Start()
    {
        SetInitialPositions();
    }

    void SetInitialPositions()
    {
        // Example: Setting the player's start position to the bottom-left corner with an offset
        Vector3 playerStartPosition = mainCamera.ViewportToWorldPoint(new Vector3(0.1f, 0.2f, mainCamera.nearClipPlane));
        playerTransform.position = playerStartPosition;

        // Similarly, position other objects relative to the screen size
        // Example: An object at the top-right corner with an offset
        Vector3 spawnPoint1Start = mainCamera.ViewportToWorldPoint(new Vector3(1.5f, 0.75f, mainCamera.nearClipPlane));
        spawnPoint1.position = spawnPoint1Start;
        // transform.position = mainCamera.ViewportToWorldPoint(new Vector3(0.9f, 0.9f, mainCamera.nearClipPlane));

        Vector3 spawnPoint2Start = mainCamera.ViewportToWorldPoint(new Vector3(1.5f, 0.50f, mainCamera.nearClipPlane));
        spawnPoint2.position = spawnPoint2Start;

        Vector3 spawnPoint3Start = mainCamera.ViewportToWorldPoint(new Vector3(1.5f, 0.25f, mainCamera.nearClipPlane));
        spawnPoint3.position = spawnPoint3Start;


        Vector3 BossSpwanPointStart = mainCamera.ViewportToWorldPoint(new Vector3(1.5f, 0.50f, mainCamera.nearClipPlane));
        bossSpawnPoint.position = BossSpwanPointStart;

    }
}
