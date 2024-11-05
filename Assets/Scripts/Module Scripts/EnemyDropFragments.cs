using UnityEngine;

public class EnemyDropFragments : MonoBehaviour
{
    public enum FragmentType { Speed, Shield, Fire, Missile, Laser, Health }
    public FragmentType fragmentType;
    public GameObject fragmentPrefab; // Prefab of the fragment to drop

    //private bool killedByPlayer = false;

    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            killedByPlayer = true; // BUG**** When the enemy move off screen and is killed it still drops the fragment if the player hits them just once.
            Destroy(other.gameObject); // Destroy the player's bullet
            HandleDestruction();
        }
    }


    void HandleDestruction()
    {

        if(killedByPlayer)
        {
            DropFragment();
        }
        Destroy(gameObject);
    }
    void OnDestroy()
    {
        Debug.Log("Killed by Player");
    }
    */

    public void DropFragment()
    {
        Instantiate(fragmentPrefab, transform.position, Quaternion.identity);
    }
}
