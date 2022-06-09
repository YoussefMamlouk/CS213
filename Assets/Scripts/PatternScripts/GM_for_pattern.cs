using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class GM_for_pattern : AgentBehaviour
{
    public Pattern dog;
    public PatternPlayer player1;
    public PatternPlayer player2;
    public TextMeshProUGUI WinnerTextBlue;
    public TextMeshProUGUI WinnerTextPurple;
    public TextMeshProUGUI WinnerTextDraw;
    public TextMeshProUGUI Felicitations;
    public GameObject img;

    private Vector3 initialPosition1;
    private Vector3 initialPosition2;
    public CelluloAgentRigidBody player1rigid;
    public CelluloAgentRigidBody player2rigid;
    public GameObject baks;



    public bool playersPlaying;
    private Vector3 initialDogPosition;
    public bool player1Turn;
    public bool player2Turn;
    public Button btn;
    private bool startOfGame;
    private bool gameIsOn;
    public bool arg;
    private int counter;

    public void Start(){
        initialPosition1 = player1.gameObject.transform.position;
       initialPosition2 = player2.gameObject.transform.position;
       counter = 0;
       initialDogPosition = dog.gameObject.transform.position;

        dog.Start();
        gameIsOn = true;
        arg = false;
        player1Turn = false;
        player2Turn = false;

        startOfGame = true;
        playersPlaying = false;
        btn.onClick.AddListener(() => {
            playersPlaying = false;
            dog.learNewPattern();
            player1.disableButtons();
            player2.disableButtons();
            startOfGame = false;
            btn.gameObject.SetActive(false);
        });
    }
    public override void FixedUpdate(){
        gameIsOn = player1.getIsWinning() && player2.getIsWinning();
        if(gameIsOn){
        playersPlaying = player1Turn || player2Turn;
        if(!playersPlaying && !startOfGame && arg){
            dog.learNewPattern();
            playersPlaying = false;
            player1Turn = false;
            player2Turn = false;
            arg = false;
        }
       
        if(!playersPlaying && !dog.isShowing() && !startOfGame){
           dog.gameObject.transform.position = new Vector3(initialPosition2.x, initialPosition2.y, -1.16f);
           player1.setAttendedPosition(initialDogPosition);
           player1.gameObject.transform.position = initialDogPosition;
            player1.resetPlayerPattern();
            player1Turn = true; 
            player1.enableButtons(); 
            player1.setIsPlaying(true);
            player2.setIsPlaying(false);

        }
        
        if(player1Turn && !player1.getIsPlaying()){
    
            player1Turn = false;
            player2.resetPlayerPattern();
            player2Turn = true;
            player1.setIsPlaying(false);
            player2.setIsPlaying(true);

            player2.setAttendedPosition(initialDogPosition);
            player2.gameObject.transform.position = initialDogPosition;

           
            player2.enableButtons();
        }
        if(player2Turn && !player2.getIsPlaying()){
           
            playersPlaying = false;
            player1Turn = false;
            
            dog.gameObject.transform.position = initialDogPosition;
            player2.setIsPlaying(false);
            player1.setIsPlaying(false);
            player2Turn = false;
            arg = true;

            for(int i  = 0; i<= counter; i++){
            if((player1.getPattern(i) != dog.getPattern(i))){
                player1.setIsWinning(false);
                gameIsOn = false;
               
            }
            if((player2.getPattern(i) != dog.getPattern(i))){
                player2.setIsWinning(false);
                gameIsOn = false;
            }
            }
            
            counter++;

        }
        } else {
            img.SetActive(true);
            baks.SetActive(true);
        if(!player1.getIsWinning() && !player2.getIsWinning() )
            { 
                WinnerTextDraw.text = "DRAW";
                Felicitations.text  = "NEXT TIME !";
 
            }
            else if (player1.getIsWinning() && !player2.getIsWinning())
            {
              
                WinnerTextBlue.text  = "TEAM BLUE";
                WinnerTextBlue.color = player1rigid.initialColor ;
                Felicitations.text  = "IS THE WINNER !";
                Felicitations.color = player1rigid.initialColor ;
                
               
                
            }else
            {
                 WinnerTextPurple.text = "TEAM PURPLE";
                 WinnerTextPurple.color = player2rigid.initialColor ; 
                 Felicitations.text  = "IS THE WINNER !";
                Felicitations.color = player2rigid.initialColor ;
                               
            }
    }
        
    }
}

