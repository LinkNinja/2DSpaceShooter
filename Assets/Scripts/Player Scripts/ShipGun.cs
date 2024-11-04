using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipGun : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject chargedProjectile;
    [SerializeField] private Transform firePoint;
    public ChargeBar chargeBar;
    public float chargeTime = 1f;
    public float chargeCounter = 0f;
    private bool isCharging;
    private GameManager gameManager;


    private void Start()
    {
        // Get reference to the GameManager
        gameManager = FindObjectOfType<GameManager>();
    }
    void Update()
    {
        Timer();
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (gameManager.currentState == GameManager.GameState.Paused)
        {
            return;
        }
        if (gameManager.currentState == GameManager.GameState.GameOver)
        {
            return;
        }

        if (context.started)
        {
            isCharging = true;
        }
        if (context.canceled)
        {
            if (chargeCounter >= chargeTime)
            {
                FireChargedShot();
            }
            else
            {
                FireSmallProjectile();
            }
            isCharging = false;
        }
    }

    void Timer()
    {
        if (isCharging)
        {
            chargeCounter += Time.deltaTime;
        }
        else
        {
            chargeCounter = 0;
        }
    }

    void FireSmallProjectile()
    {
        Instantiate(projectile, firePoint.position, firePoint.rotation);
        FindObjectOfType<AudioManager>().Play("ShootSmallProjectile");
    }

    void FireChargedShot()
    {
        Instantiate(chargedProjectile, firePoint.position, firePoint.rotation);
        FindObjectOfType<AudioManager>().Play("ShootLargeProjectile");
    }

    public void ResetCharging()
    {
        isCharging = false;
        chargeCounter = 0;
    }
}
