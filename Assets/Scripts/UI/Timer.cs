using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/**
	This class is the implementation of the timer used in the game and how it is handled in it
*/
public class Timer : MonoBehaviour
{
    private float initTimerValue;
 
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI WinnerTextBlue;
    public TextMeshProUGUI WinnerTextPurple;
    public TextMeshProUGUI WinnerTextDraw;
    public GameObject img;
    private bool isGameOver = false; 
    public GameObject ButtonPlayAgain;
    public ChangeScore player1;
    public ChangeScore player2;
    public TextMeshProUGUI winnerText;

    public Slider slider;
    public GameManager gameManager;
    public bool timerStart = false;

    public void Awake() {
        initTimerValue = Time.time; 
    }

    // Start is called before the first frame update
    public void Start() {
        resetTimer();
        timerText.text = string.Format("{0:00}:{1:00}", 0, 0);
        
    }

    // Update is called once per frame
    public void Update() {
        if (timerStart && !isGameOver)
        {
            if (initTimerValue <= 120.0f)
            {
                initTimerValue += Time.deltaTime;
                float minutes = Mathf.FloorToInt(initTimerValue / 60);
                float seconds = Mathf.FloorToInt(initTimerValue % 60);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                timerStart = false;
            }
            
        }
        slider.value = initTimerValue;
        if( initTimerValue >= 120.0f)
        {
            isGameOver = true;
            ButtonPlayAgain.SetActive(true);
            img.SetActive(true);
            GameOverText.text = "Game Over ! \n\n" +
                "THE WINNER IS :";
            if(player1.getScore() > player2.getScore())
            { 
                WinnerTextBlue.text  = "TEAM BLUE";
               
            }
            else if (player1.getScore() > player2.getScore())
            {
                
                WinnerTextPurple.text = "TEAM PURPLE";
                
            } else
            {
                WinnerTextDraw.text = "DRAW";
            }
          
         
        }
    }

    public void resetTimer()
    {
        initTimerValue = 0;
    }

    public void startTimer()
    {
        timerStart = true;
    }
    
    public bool isGameOverOrNot()
    {
        return isGameOver;
    }
}
