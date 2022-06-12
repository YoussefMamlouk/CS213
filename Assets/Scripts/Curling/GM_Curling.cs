using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GM_Curling : AgentBehaviour
{
    public CurlingPlayer player1FirstCellulo;
    public CurlingPlayer player2FirstCellulo;
    public CurlingPlayer player1SecondCellulo;
    public CurlingPlayer player2SecondCellulo;
    private int score1;
    private int score2;
    private bool gameIsOver;
    private bool player1Turn;
    private bool player2Turn;
    private int round;
    private int playerWinning;
    private float distanceOfWinner;

    private bool startOfGame;
    private Vector3 initialPosition;

    public TextMeshProUGUI WinnerTextPurple;
    
    public TextMeshProUGUI WinnerTextBlue;

    public TextMeshProUGUI Felicitations;
     public GameObject baks;

    public GameObject img;



    // Start is called before the first frame update
    void Start(){
        distanceOfWinner = 0;
        initialPosition = player1FirstCellulo.gameObject.transform.position;
        startOfGame = true;
        round = 0;
        player1FirstCellulo.startTurn();
        gameIsOver = false;
        player2Turn = false;
        player1Turn = true;

    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
       
        if(!player1FirstCellulo.getIsThrowing() && player1Turn && !gameIsOver && round == 0 && !startOfGame){
            
            player1FirstCellulo.finishTurn();
            player2FirstCellulo.gameObject.transform.position = initialPosition;
            player2FirstCellulo.startTurn();
            player1Turn = false;
            player2Turn = true;
        }
         if(player1FirstCellulo.getIsThrowing() && startOfGame){
            startOfGame = false;
        }
        if(!player2FirstCellulo.getIsThrowing() && player2Turn && !gameIsOver && round == 0){
            round++;
            player2FirstCellulo.finishTurn();
            player1SecondCellulo.gameObject.transform.position = initialPosition;
            player1SecondCellulo.startTurn();
            player1Turn = true;
            player2Turn = false;
        }
        if(!player1SecondCellulo.getIsThrowing() && player1Turn && !gameIsOver && round == 1){

            player1SecondCellulo.finishTurn();

            player2SecondCellulo.gameObject.transform.position = initialPosition;
            player2SecondCellulo.startTurn();
            player1Turn = false;
            player2Turn = true;
        }

        if(!player2SecondCellulo.getIsThrowing() && player2Turn && !gameIsOver && round == 1){
            round++;
            player2SecondCellulo.finishTurn();
            player2Turn = false;
            player1Turn = false;
        }
        if(round == 2){
            gameIsOver = true;
        }
        if(gameIsOver){
            playerWinning = 1;
            distanceOfWinner = computeScore(player1FirstCellulo , 1);
            computeScore(player1SecondCellulo , 1);
            computeScore(player2FirstCellulo , 2);
            computeScore(player2SecondCellulo , 2);
            gameIsOver = false;
            round++;
            img.SetActive(true);
            baks.SetActive(true);
            Felicitations.text  = "IS THE WINNER !";
       
        if(playerWinning == 1 )
          
        { 
                WinnerTextBlue.text  = "TEAM BLUE";
             
                
                
                
            }else
            {
                 WinnerTextPurple.text = "TEAM PURPLE";
                 
                 
                
                               
            }
        }
    }

    public float computeScore(CurlingPlayer score, int player){
        float x,y  = 0;
        x = (score.gameObject.transform.position.x - 22.2f);
        y = (score.gameObject.transform.position.z + 10.1f);
        float eq = Mathf.Sqrt(x*x +y*y);
        if(distanceOfWinner != 0 && eq < distanceOfWinner){
            distanceOfWinner = eq;
            playerWinning = player;
        }
        return eq;
        
        
    }
}
