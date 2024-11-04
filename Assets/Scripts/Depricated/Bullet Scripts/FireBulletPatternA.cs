using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBulletPatternA : MonoBehaviour
{
    [SerializeField]
    private int bulletsAmount = 10;
    [SerializeField]
    private float startAngle = 90f;
    [SerializeField]
    private float endAngle = 270f;

    private Vector3 bulletMoveDirection;
      

    // Start is called before the first frame update
    void Start()
    {
        
        InvokeRepeating("FireBullets", 0f, 2f);
        
    }


    private void FireBullets()
    {
   
            float angleStep = (endAngle - startAngle) / bulletsAmount;
            float angle = startAngle;

            for (int i = 0; i < bulletsAmount + 1; i++)
            {
                float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
                float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

                Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
                Vector3 bulDir = (bulMoveVector - transform.position).normalized;

                GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
                bul.transform.position = transform.position;
                bul.transform.rotation = transform.rotation;
                bul.SetActive(true);
                bul.GetComponent<BulletPatternA>().SetMoveDirection(bulDir);

                angle += angleStep;



            }
        
    
    }

   
}
