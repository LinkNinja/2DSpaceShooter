using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScaler : MonoBehaviour
{
    private Vector2 referenceResolution = new Vector2(1920, 1080); // Reference resolution

    public void AdjustObjectSize(GameObject obj)
    {
        Vector2 screenResolution = new Vector2(Screen.width, Screen.height);
        float scaleX = screenResolution.x / referenceResolution.x;
        float scaleY = screenResolution.y / referenceResolution.y;

        obj.transform.localScale = new Vector3(obj.transform.localScale.x * scaleX, obj.transform.localScale.y * scaleY, obj.transform.localScale.z);
    }

    public void AdjustObjectPosition(Transform objTransform)
    {
        Vector2 screenResolution = new Vector2(Screen.width, Screen.height);
        float scaleX = screenResolution.x / referenceResolution.x;
        float scaleY = screenResolution.y / referenceResolution.y;

        Vector3 adjustedPosition = new Vector3(objTransform.position.x * scaleX, objTransform.position.y * scaleY, objTransform.position.z);
        objTransform.position = adjustedPosition;
    }
}