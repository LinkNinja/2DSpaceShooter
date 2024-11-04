using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldManager : MonoBehaviour
{
    public ShieldBar shieldBar;
    [Header("HealthStats")]
    public float maxShield = 100f;
    public float currentShield;
    public float damage;
    public int playerLives = 2;
    float _shields;

    [Header("Audio")]
    [SerializeField]
    private AudioSource laserAudio;
    [SerializeField]
    private AudioSource explosionSound;
    [SerializeField]
    private AudioSource shieldPickup;
    [SerializeField]
    private AudioSource dialogue;
    [SerializeField]
    private AudioSource takeDamage;

    [SerializeField]
    private Animator anim;

    // The health property
    public float Shields
    {
        set
        {
            _shields = value;

        }
        get
        {
            return _shields;
        }
    }

    void Awake()
    {
        //Get Animator component when the player wakes up.
        anim = GetComponent<Animator>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //At the start of the scene. Players Current shield is equal to the maxShield value.
        currentShield = maxShield;
        shieldBar.SetMaxShields(maxShield);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }


}
