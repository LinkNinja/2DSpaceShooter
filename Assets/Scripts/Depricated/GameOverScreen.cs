using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{

    public TextMeshProUGUI pointstext;
    
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointstext.text = score.ToString() + " Points";


    }


}
