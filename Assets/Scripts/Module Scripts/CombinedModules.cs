using System.Collections;
using UnityEngine;

public class CombinedModules : MonoBehaviour
{



    public float speedDuration = 10f;
    public float shieldDuration = 1f;
    public float fireDuration = 10f;
    public float missileDuration = 10f;
    public float laserDuration = 10f;
    public float healthDuration = 0f; 
    public GameObject shieldEffectPrefab; 
    public GameObject speedTrailEffectPrefab;



    private PlayerControls_V2 player;

    void Start()
    {
        player = GetComponent<PlayerControls_V2>();
    }

    public IEnumerator ActivateSpeed()
    {
        if (player == null) yield break;

        // Adjust the offset as needed
        Vector3 offset = new Vector3(-1f, -0.1f, 0);
        var speedTrailEffect = Instantiate(speedTrailEffectPrefab, player.transform.position + offset, Quaternion.identity, player.transform);
        speedTrailEffect.transform.parent = player.transform;
        AudioManager.Instance.Play("SpeedModule");
        player.speed *= 1.5f;
        yield return new WaitForSeconds(speedDuration);
        player.speed = player.originalSpeed;
        Destroy(speedTrailEffect);
    }

    public IEnumerator ActivateShield()
    {
        if (player == null) yield break;

        //Off set the Shield Animation Prefab, adjust as needed.
        Vector3 offset = new Vector3(.25f, 0, 0);

        //Assign the Prefab to a local shield variable to destroy after shields are recharged.
        var shieldEffect = Instantiate(shieldEffectPrefab, player.transform.position + offset, Quaternion.identity, player.transform);

        //Play the Shield recharge sound from AudioManager
        AudioManager.Instance.Play("ShieldRecharge");

        // Restore Player Shields
        player.currentShield += 25; 

        //Wait for the shield power up to finish.
        yield return new WaitForSeconds(shieldDuration);

        //Destroy the shield animation.
        Destroy(shieldEffect);
        
    }

    public IEnumerator ActivateFire()
    {
        if (player == null) yield break;

        player.originalDamage *= 2;
        yield return new WaitForSeconds(fireDuration);
        player.originalDamage = 10;
    }

    public IEnumerator ActivateMissile()
    {
        if (player == null) yield break;

        // Implement missile attack logic
        yield return new WaitForSeconds(missileDuration);
    }

    public IEnumerator ActivateLaser()
    {
        if (player == null) yield break;

        // Implement laser weapon logic
        yield return new WaitForSeconds(laserDuration);
    }

    public IEnumerator ActivateHealth()
    {
        if (player == null) yield break;

        player.currentShield = player.maxShield;
        yield return null; 
    }
}
