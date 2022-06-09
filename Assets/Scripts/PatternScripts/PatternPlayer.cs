using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PatternPlayer : AgentBehaviour
{
    public Pattern dog;
    public Button UP;
    public Button DOWN;
    public Button RIGHT;
    public Button LEFT;

  
    public readonly float step = 2.89f;
    private int nbOfMoves;
    private int counter;

    private Steering currentSteering ;
    private Directions currentDirection;
    private Vector3 initialPosition;
    private Vector3 attendedPosition;
    private bool isPlaying;
    private bool winning;
    private List<Directions> playerPattern;



    
    // Start is called before the first frame update
    public void Start()
    {
        playerPattern = new List<Directions>();
        counter = 0;
        nbOfMoves = 0;
        currentDirection = Directions.NOWHERE;
        initialPosition = this.transform.position;
        attendedPosition = initialPosition;
        currentSteering =  new Steering();
        currentSteering.linear = new Vector3(0,0,0);
        winning = true;
      

        
        UP.onClick.AddListener(() => {
            isPlaying = true;
            disableButtons();
            currentDirection = Directions.UP;
            counter++;
            playerPattern.Add(currentDirection);
        });

         DOWN.onClick.AddListener(() => {
            isPlaying = true;
            disableButtons();
            currentDirection = Directions.DOWN;
            counter++;
                        playerPattern.Add(currentDirection);


        });
         LEFT.onClick.AddListener(() => {
            isPlaying = true;
            disableButtons();
            currentDirection = Directions.LEFT;
            counter++;
                        playerPattern.Add(currentDirection);


        });
         RIGHT.onClick.AddListener(() => {
            isPlaying = true;
            disableButtons();
            currentDirection = Directions.RIGHT;
            counter++;
            playerPattern.Add(currentDirection);


        });
          disableButtons();
       

    }
    public void disableButtons(){
        UP.interactable = false;
        DOWN.interactable = false;
        LEFT.interactable = false;
        RIGHT.interactable = false;
    }

   public override void FixedUpdate(){
       if(isPlaying){
            Vector3 vex = attendedPosition;
            switch(currentDirection){
            case Directions.RIGHT: 
            vex = new Vector3(vex.x + step, vex.y , vex.z);
            break;
            case Directions.LEFT : 
            vex = new Vector3(vex.x -step, vex.y , vex.z);
            break;
            case Directions.UP: 
            vex = new Vector3(vex.x , vex.y , vex.z + step);
            break;
            case Directions.DOWN: 
            vex = new Vector3(vex.x , vex.y ,vex.z -step);
            break;
            }
            Vector3 v = new Vector3(this.transform.position.x, 0, this.transform.position.z);

            if(currentDirection != Directions.NOWHERE){
                 if(compareTwoVectors(vex, v)){
                     goToNextMove();
                    
                 }
            }
        if (agent.blendWeight){
            agent.SetSteering(GetSteering(), weight);}
        else{
            agent.SetSteering(GetSteering());}
       }
   }

   public void goToNextMove(){
       if(dog.sizeOfPattern() == nbOfMoves + 1){
        
           attendedPosition = initialPosition;
           currentSteering.linear = new Vector3(0,0,0);
           currentDirection = Directions.NOWHERE;
           disableButtons();
           nbOfMoves = 0;
           isPlaying = false;
           
           if (agent.blendWeight)
            agent.SetSteering(currentSteering, weight);
        else
            agent.SetSteering(currentSteering);
            transform.position = initialPosition;
           
       }else{
           ++nbOfMoves;
           currentDirection = Directions.NOWHERE;
           enableButtons();
           attendedPosition = new Vector3(this.transform.position.x, 0, this.transform.position.z);
       }
   }
   public void resetPlayerPattern(){
       playerPattern = new List<Directions>();
   }

   public void enableButtons(){
       UP.gameObject.SetActive(true);
       DOWN.gameObject.SetActive(true);
       RIGHT.gameObject.SetActive(true);
       LEFT.gameObject.SetActive(true);

       UP.interactable = true;
       DOWN.interactable = true;
       LEFT.interactable = true;
       RIGHT.interactable = true;
        
   }

   public override Steering GetSteering(){
    if(isPlaying){
    Vector3 vec = new Vector3(0,0,0);
    switch(currentDirection){
    case Directions.RIGHT: 
    vec = new Vector3(step,0,0);
    break;
    case Directions.LEFT : 
    vec = new Vector3(-step,0,0);
    break;
    case Directions.UP: 
    vec = new Vector3(0,0,step);
    break;
    case Directions.DOWN: 
    vec = new Vector3(0,0,-step);
    break;
    case Directions.NOWHERE : 
    vec = new Vector3(0,0,0);
    break;
    }
    currentSteering.linear = vec * agent.maxAccel;
    }
    else{
        currentSteering.linear = new Vector3(0,0,0);
    }

    return currentSteering;

   }

   public void setIsPlaying(bool a){
       isPlaying = a;
   }
   public bool getIsPlaying(){
       return isPlaying;
   }
   public bool getIsWinning(){
       return winning;
   }

   public void setAttendedPosition( Vector3 v){
       attendedPosition = v;
   }

   public bool compareTwoVectors(Vector3 v1, Vector3 v2){
    bool xBool,yBool = false;

        xBool = Mathf.Abs(v1.x - v2.x) <= 0.1f;
        yBool = Mathf.Abs(v1.z - v2.z) <= 0.1f;
    

    return xBool && yBool;

}
public Directions getPattern(int i ){
    return playerPattern[i];
}

public void setIsWinning( bool a){
    winning = a;
}

public override void OnCelluloLongTouch(int key){
    isPlaying = true;
    disableButtons();
    switch(key){
        case 0: currentDirection = Directions.UP; break;
        case 1: currentDirection = Directions.UP; break;
        case 2: currentDirection = Directions.RIGHT; break;
        case 3: currentDirection = Directions.DOWN; break;
        case 4: currentDirection = Directions.DOWN; break;
        case 5: currentDirection = Directions.LEFT; break;
    }
    counter++;
    playerPattern.Add(currentDirection);
}

}
