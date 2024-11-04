using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        
        //Play Audio Clip
        FindObjectOfType<AudioManager>().Play("Explosion");
        

        StartCoroutine(DestroyAfterAnimation());
    }

    IEnumerator DestroyAfterAnimation()
    {
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
