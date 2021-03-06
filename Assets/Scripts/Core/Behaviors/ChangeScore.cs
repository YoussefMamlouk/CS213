using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeScore : MonoBehaviour
{


    private int score;
    public TextMeshProUGUI txt;
    public Timer timer;


    public void Start()
    {
        score = 0;
        txt.text = "00";
       
    }
    public void changeColor(Color color)
    {
        txt.color = color;
    }

    public void Update()
    {
        if(!this.GetComponent<MoveWithKeyboardBehavior>().getOnPause())
        txt.text = string.Format("{0:00}", score);
    }

    public void incrementScore()
    {
        if(!this.GetComponent<MoveWithKeyboardBehavior>().getOnPause())
            if (timer.isTimeout())
                score+=2;
            else
                score++;
    }

    public void decrementScore()
    {
        if(!this.GetComponent<MoveWithKeyboardBehavior>().getOnPause())

           if (timer.isTimeout())
                score-=2;
            else
                score--;
    }

    public int getScore()
    {
        return score;
    }

    
}