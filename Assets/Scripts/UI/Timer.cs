using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

/**
	This class is the implementation of the timer used in the game and how it is handled in it
*/
public class Timer : MonoBehaviour
{
    public float initTimerValue;

    public GameObject pauseButton;
    public TextMeshProUGUI timerWhenResume;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI WinnerTextBlue;
    public TextMeshProUGUI WinnerTextPurple;
    public TextMeshProUGUI WinnerTextDraw;
    public GameObject img;
    private bool isGameOver = false; 
    public GameObject ButtonPlayAgain;
    public GameObject BackButton;
    public ChangeScore player1;
    public ChangeScore player2;
    public TextMeshProUGUI winnerText;
    public timeDisplayer timeDisplayer;
    private float maxTime;
    public Slider sld;

    public Scrollbar slider;
    public GameManager gameManager;
    public bool timerStart = false;

    public GameObject gem;
    public GameObject player1GameObject;
    public GameObject player2GameObject;
    private bool GemSpawned;
    private AudioSource gemSoundSource;
    public AudioClip gemSound;

    public void Awake() {
        initTimerValue = Time.time;
        maxTime = 0;
    }

    // Start is called before the first frame update
    public void Start() {
        resetTimer();
        gemSoundSource = gem.GetComponent<AudioSource>();
        gemSoundSource.clip = gemSound;
        GemSpawned = false;
        gem.SetActive(false);
        timerText.text = string.Format("{0:00}:{1:00}", 0, 0);
        maxTime = timeDisplayer.getRealTime();
    }

    // Update is called once per frame
    public void Update() {
        timerWhenResume.text = timerText.text; 
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
        slider.size = initTimerValue / maxTime;
        if ( initTimerValue >= maxTime)
        {
            isGameOver = true;
            pauseButton.SetActive(false);
            ButtonPlayAgain.SetActive(true);
            BackButton.SetActive(false);
            slider.gameObject.SetActive(false);
            img.SetActive(true);
            GameOverText.text = "Game Over ! \n\n" +
                "THE WINNER IS :";
            if(player1.getScore() > player2.getScore())
            { 
                WinnerTextBlue.text  = "TEAM BLUE";
               
            }
            else if (player1.getScore() < player2.getScore())
            {
                
                WinnerTextPurple.text = "TEAM PURPLE";
                
            } else
            {
                WinnerTextDraw.text = "DRAW";
            }
          
         
        }
        float f = UnityEngine.Random.Range(-10000f, 10000f);
        if (f > 9999f)
        {
            print(f.ToString());
        }
        if (f > 9999f && !GemSpawned)
        {
            appearGem();
        }
    }

    private void appearGem()
    {
        Vector3 gemPosition = new Vector3(UnityEngine.Random.Range(0f, 16f), 0f, UnityEngine.Random.Range(-8f, 0f));

        while ((gemPosition - player1GameObject.transform.position).magnitude < 1 || (gemPosition - player2GameObject.transform.position).magnitude < 1)
        {
            gemPosition = new Vector3(UnityEngine.Random.Range(0f, 16f), 0f, UnityEngine.Random.Range(-8f, 0f));
        }
        gem.SetActive(true);
        gem.transform.position = gemPosition;
        GemSpawned = true;
    }

    public void destroyGem()
    {
        gem.SetActive(false);
        gemSoundSource.Play();
    }

    public void bonusApplied() {
        GemSpawned = false;
        gemSoundSource.Play();
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
