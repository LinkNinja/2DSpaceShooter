using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{


    // Background scrolling speed variable.
    public float scrollSpeed = 0.1f;


    //Variable to hold the mesh renderer
    private MeshRenderer meshRenderer;


    // Variable to hold the float for what axis we are scrolling. In this case the X
    private float xScroll;




    // Start is called before the first frame update
    void Awake()
    {
        //As soon as the object wakes up. We use the meshRenderer variable to get the objects component <MeshRenderer>
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        //Every frame call the scroll Function.
        Scroll();


    }

    
    //Scroll Function.
    //This function handles the scrolling of the background. Using all the variables above.
    void Scroll()
    {
        //We assign the xScroll a new value which is Time.time and multiply it by the variable ScrollSpeed.
        xScroll = Time.time * scrollSpeed;

        //We create a new vector 2 variable OFFSET and assign it a new value.
        //The new value is also a vector 2 with the variable XSCROLL inserted in its x spot and 0 in the y spot.
        Vector2 offset = new Vector2(xScroll, 0f);

        //Grabe the shared Material and set its texture.
        meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);

    }





}
