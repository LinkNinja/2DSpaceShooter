using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{


    //speed of the scroll
    public float scrollSpeed = 0.1f;


    //Variable to hold the mesh renderer
    private MeshRenderer meshRenderer;


    private float xScroll;




    // Start is called before the first frame update
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
    }

    void Scroll()
    {
        xScroll = Time.time * scrollSpeed;

        Vector2 offset = new Vector2(xScroll, 0f);
        meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);

    }





}
