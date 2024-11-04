using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HighScore : MonoBehaviour
{

    [SerializeField]
    private TMP_Text score;

    // Start is called before the first frame update
    void Start()
    {
        score.text = "Score: " + ScoreScript.scoreValue;
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + ScoreScript.scoreValue;
    }
}
