using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{

    public static int scoreValue = 0;

    [SerializeField]
    private TMP_Text score;


    // Start is called before the first frame update
    void Start()
    {
        //score.GetComponent<TMP_Text>();
        scoreValue = 0;
        score.text = "Score: " + scoreValue;
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + scoreValue;
    }
}
