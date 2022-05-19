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

    public CelluloAgentRigidBody player1rigid;
    public CelluloAgentRigidBody player2rigid;

    public GameObject pauseButton;
    public TextMeshProUGUI timerWhenResume;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI WinnerTextBlue;
    public TextMeshProUGUI WinnerTextPurple;
    public TextMeshProUGUI WinnerTextDraw;
    public GameObject img;
    private bool isGameOver = true; 
    public GameObject ButtonPlayAgain;
    public GameObject BackButton;
    public GameObject StartButton;
    public ChangeScore player1;
    public ChangeScore player2;
    public TextMeshProUGUI winnerText;
    public timeDisplayer timeDisplayer;
    private float maxTime;
    public Slider sld;

    public Scrollbar slider;
    public GameManager gameManager;
    public bool timerStart;

    public GameObject gem;
    public GameObject player1GameObject;
    public GameObject player2GameObject;
    public GameObject ghostSheep;
    private bool GemSpawned;
    public AudioSource gemSoundSource;
    public AudioClip gemSound;

    public AudioClip IJA;

    public bool musicPlaying;
    private bool onPause;
    public GameObject blockObject;
    private bool blockAppeared;

    public void Awake() {
        initTimerValue = Time.time;
        blockAppeared = false;
        maxTime = 30;
        timerStart = false;
        isGameOver = true;
    }
    public void pauseUnpause(){
        onPause = !onPause;
    }

    // Start is called before the first frame update
    public void Start() {

        resetTimer();
        timerStart = false;
        isGameOver = true;
        onPause = false;
        gemSoundSource = this.GetComponent<AudioSource>();
        GemSpawned = false;
        gem.SetActive(false);
        timerText.text = string.Format("{0:00}:{1:00}", 0, 0);
        maxTime = timeDisplayer.getRealTime();
        musicPlaying = true ; 
    }

    // Update is called once per frame
    public void Update() {
        player1.changeColor(player1rigid.initialColor);
        player2.changeColor(player2rigid.initialColor);

        if (player1GameObject.GetComponent<MoveWithKeyboardBehavior>().getReady() 
                && player2GameObject.GetComponent<MoveWithKeyboardBehavior>().getReady() && !onPause){
                player1GameObject.GetComponent<AgentBehaviour>().setIsMoving();
                player2GameObject.GetComponent<AgentBehaviour>().setIsMoving();
                ghostSheep.GetComponent<AgentBehaviour>().setIsMoving();
                StartButton.SetActive(false);
                startTimer();
        }

        timerWhenResume.text = timerText.text; 
        if (timerStart && !isGameOver && !onPause)
        {
            if (initTimerValue <= maxTime)
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
        if (initTimerValue >= maxTime && !onPause)
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
                WinnerTextBlue.text  = "TEAM LEFT";
                WinnerTextBlue.color = player1rigid.initialColor;
               
            }
            else if (player1.getScore() < player2.getScore())
            {
                
                WinnerTextPurple.text = "TEAM RIGHT";
                WinnerTextPurple.color = player2rigid.initialColor;
                
            } else
            {
                WinnerTextDraw.text = "DRAW";
            }
          
         
        }
        float f = UnityEngine.Random.Range(-7500f, 7500f);
       
        if ( f > 7497f && !GemSpawned && initTimerValue <= maxTime && !onPause && timerStart && !isGameOver)

        {
            appearGem();
        }
        if(maxTime - initTimerValue < 30.0f && !blockAppeared){
            appearBlockObject();
        }
    }
    public void appearBlockObject(){
        Vector3 blockPosition = new Vector3(UnityEngine.Random.Range(3f, 28f), 0f, UnityEngine.Random.Range(-16f, -2f));
        while ((blockPosition - player1GameObject.transform.position).magnitude < 2 || (blockPosition - player2GameObject.transform.position).magnitude < 2)
        {
            blockPosition = new Vector3(UnityEngine.Random.Range(3, 28f), 0f, UnityEngine.Random.Range(-16f, -2f));
        }
        blockObject.SetActive(true);
        blockObject.transform.position = blockPosition;
        blockAppeared = true;
    
    }

    private void appearGem()
    {
        Vector3 gemPosition = new Vector3(UnityEngine.Random.Range(3f, 28f), 0f, UnityEngine.Random.Range(-16f, -2f));

        while ((gemPosition - player1GameObject.transform.position).magnitude < 2 || (gemPosition - player2GameObject.transform.position).magnitude < 2)
        {
            gemPosition = new Vector3(UnityEngine.Random.Range(3, 28f), 0f, UnityEngine.Random.Range(-16f, -2f));
        }
        gem.SetActive(true);
        gem.transform.position = gemPosition;
        GemSpawned = true;
        if( musicPlaying && !onPause){
       gemSoundSource.clip = gemSound;

        gemSoundSource.Play();}
    }
    public void stopMusic(){
        gemSoundSource.Stop();
    }
    public void destroyBlock(){
        blockObject.SetActive(false);
    }

    public void destroyGem()
    {
        gem.SetActive(false);
         if( musicPlaying && !onPause){
              gemSoundSource.clip = gemSound;
        gemSoundSource.Play();}
    }
    public void muteUnmute(){
        musicPlaying = !musicPlaying;
    }
    public void bonusApplied() {
        GemSpawned = false;
     if( musicPlaying && !onPause){
        gemSoundSource.clip =IJA ;
        gemSoundSource.Play();}
    }


    public void resetTimer()
    {
        initTimerValue = 0;
    }

    public void startTimer()
    {
        timerStart = true;
        isGameOver = false;
    }
    
    public bool isGameOverOrNot()
    {
        return isGameOver;
    }
    public float getTime(){return  initTimerValue;}
   
}
