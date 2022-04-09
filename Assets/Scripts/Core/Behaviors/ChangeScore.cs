using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeScore : MonoBehaviour
{


    private int score;
    public TextMeshProUGUI txt;

    public void Start()
    {
        score = 0;
        txt.text = "00";
    }

    public void Update()
    {
        txt.text = string.Format("{0:00}", score);
    }

    public void incrementScore()
    {
        score++;
    }

    public void decrementScore()
    {
        score--;
    }

    public int getScore()
    {
        return score;
    }
}