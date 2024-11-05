using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerControls_V2 : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private float vertical;
    private float horizontal;
    public float speed = 5f;
    public float minY, maxY;
    public float minX, maxX;
    public float originalSpeed;
    public int maxShield = 100;
    public int currentShield;
    public Slider shieldbar;
    public int originalDamage = 10;
    public GameObject explosionPrefab; 
    private ModuleManager moduleManager;
    private CombinedModules combinedModules;
    private GameManager gameManager; 
    public ObjectScaler objectScaler;
    public GameObject player;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        currentShield = maxShield;
        shieldbar.value = currentShield;
        originalSpeed = speed;

        // Get references to the ModuleManager and CombinedModules scripts
        moduleManager = GetComponent<ModuleManager>();
        combinedModules = GetComponent<CombinedModules>();

        // Get reference to the GameManager
        gameManager = FindObjectOfType<GameManager>();
        objectScaler.AdjustObjectPosition(transform);
        objectScaler.AdjustObjectSize(player);

    }

    private void FixedUpdate()
    {
        shieldbar.value = currentShield;
        _rigidbody2D.velocity = new Vector2(horizontal, vertical);
        if (vertical > 0f)
        {
            Vector3 temp = transform.position;
            temp.y += speed * Time.deltaTime;
            if (temp.y > maxY)
                temp.y = maxY;
            transform.position = temp;
        }
        else if (vertical < 0f)
        {
            Vector3 temp = transform.position;
            temp.y -= speed * Time.deltaTime;
            if (temp.y < minY)
                temp.y = minY;
            transform.position = temp;
        }

        if (horizontal > 0f)
        {
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;
            if (temp.x > maxX)
                temp.x = maxX;
            transform.position = temp;
        }
        else if (horizontal < 0f)
        {
            Vector3 temp = transform.position;
            temp.x -= speed * Time.deltaTime;
            if (temp.x < minX)
                temp.x = minX;
            transform.position = temp;
        }



    }

    void Update()
    {
        // Update code as needed
    }

    // Unity Event that's called on the new input system
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                TakeDamage(enemy.collisionDamage); // Apply damage to player
                if (enemy.canBeDestroyedOnCollision)
                {

                    //Need to notify the wave system that the enemy has been destroyed from crashing into the player.
                    Destroy(other.gameObject); // Destroy the enemy
                    // Notify wave manager that the enemy has been destroyed
                    WaveSystem waveSystem = FindObjectOfType<WaveSystem>();
                    if (waveSystem != null)
                    {
                        waveSystem.NotifyEnemyDestroyed(other.gameObject);
                    }
                    FindObjectOfType<AudioManager>().Play("PlayerHit");
                }
            }
        }
    }

    void TakeDamage(int damage)
    {
        currentShield -= damage;
        shieldbar.value = currentShield;
        FindObjectOfType<AudioManager>().Play("PlayerHit");

        if (currentShield <= 0)
        {
            Instantiate(explosionPrefab, transform.position, transform.rotation); // Create the explosion
            LoseLife();
        }
    }

    void LoseLife()
    {
        gameManager.LoseLife(); // Call GameManager's LoseLife method

        if (gameManager.playerLives > 0)
        {
            
            currentShield = maxShield;
            shieldbar.value = currentShield;
        }
        else
        {
            // Handle Game over (e.g., restart level)
        }
    }
    public void OnSpeed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TryActivatePowerUp(ModuleManager.ModuleType.Speed);
        }
    }

    public void OnShield(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TryActivatePowerUp(ModuleManager.ModuleType.Shield);
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TryActivatePowerUp(ModuleManager.ModuleType.Fire);
        }
    }

    public void OnMissile(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TryActivatePowerUp(ModuleManager.ModuleType.Missile);
        }
    }

    public void OnLaser(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TryActivatePowerUp(ModuleManager.ModuleType.Laser);
        }
    }

    public void OnHealth(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            TryActivatePowerUp(ModuleManager.ModuleType.Health);
        }
    }

    private void TryActivatePowerUp(ModuleManager.ModuleType moduleType)
    {
        moduleManager.UseModule(moduleType);
    }
}
