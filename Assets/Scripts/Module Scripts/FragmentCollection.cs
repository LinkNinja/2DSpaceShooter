using UnityEngine;

public class FragmentCollection : MonoBehaviour
{
    public ModuleManager.ModuleType fragmentType;
    public float attractionRange = 5f; // Range at which fragments start moving towards the player
    public float attractionSpeed = 5f; // Speed at which the fragments move towards the player

    private Transform playerTransform;
    private bool attractToPlayer = false;
    private ModuleManager moduleManager;

    void Start()
    {
        moduleManager = FindObjectOfType<ModuleManager>();
    }

    void Update()
    {
        if (playerTransform != null)
        {
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            if (distance < attractionRange)
            {
                attractToPlayer = true;
            }

            if (attractToPlayer)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, attractionSpeed * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Vector3.Distance(transform.position, other.transform.position) < 0.5f)
        {
            if (moduleManager != null)
            {
                moduleManager.CollectFragment(fragmentType);
                Destroy(gameObject);
            }
        }
    }
}
