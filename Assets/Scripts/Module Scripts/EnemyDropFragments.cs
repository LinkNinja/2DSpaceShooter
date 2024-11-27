using UnityEngine;

public class EnemyDropFragments : MonoBehaviour
{
    public enum FragmentType { Speed, Shield, Fire, Missile, Laser, Health }
    public FragmentType fragmentType;
    public GameObject fragmentPrefab; // Prefab of the fragment to drop

 
    

    public void DropFragment()
    {
        Instantiate(fragmentPrefab, transform.position, Quaternion.identity);
    }
}
