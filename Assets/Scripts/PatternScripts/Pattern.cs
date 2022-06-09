using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class Pattern : AgentBehaviour {

private static List<Directions> pattern;
private readonly float step = 2.89f;
private int maxIndex;
private bool Showing;
private Vector3 initialPosition;
private Steering currentSteering;

public void Start(){
    initialPosition  = this.transform.position;
    maxIndex = -1;
    Showing = false;
    currentSteering = new Steering();
    pattern = new List<Directions>();
}


public override Steering GetSteering()
{
if(Showing){
Vector3 vec = new Vector3(0,0,0);
switch(pattern[maxIndex]){
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
currentSteering.linear = vec * this.agent.maxAccel ;
}
return currentSteering;

}

public void learNewPattern(){
this.transform.position = initialPosition;
maxIndex = -1;
generateNewDirection();
}

public override void FixedUpdate(){

if(Showing){
Vector3 vec = initialPosition;

for(int i = 0; i < maxIndex + 1; i++){
switch(pattern[i]){
case Directions.RIGHT :
 vec = new Vector3(vec.x + step, vec.y, vec.z); break;
case Directions.LEFT :
 vec = new Vector3(vec.x - step , vec.y, vec.z); break;
case Directions.UP :  
 vec = new Vector3(vec.x  , vec.y , vec.z + step); break;
case Directions.DOWN :
  vec = new Vector3(vec.x , vec.y , vec.z- step); break; 
}
}

Vector3 vex = new Vector3(transform.position.x , 0 , transform.position.z);

if(compareTwoVectors(vex, vec)){
    goToNextMove();
}
if (agent.blendWeight){
            agent.SetSteering(GetSteering(), weight);}
        else{
            agent.SetSteering(GetSteering());}

}

}

public void goToNextMove(){
    if(maxIndex < pattern.Count - 1){
        maxIndex++;
    }
    else{
        maxIndex = 0;
        stopMoving();

    }
}

public void generateNewDirection()
{
while(true){
int index = Random.Range(0,4);
switch(index) {
case 0 : pattern.Add(Directions.RIGHT); break;
case 1 : pattern.Add(Directions.UP); break;
case 2 : pattern.Add(Directions.LEFT); break;
case 3 : pattern.Add(Directions.DOWN); break;
}
int upCounter = 0 ;
int downCounter = 0; 
int leftCounter = 0;
int rightCounter = 0;

for(int i = 0; i < pattern.Count; i++){
    switch(pattern[i]){
        case Directions.RIGHT : ++rightCounter; break;
        case Directions.UP :  ++upCounter; break;
        case Directions.LEFT:  ++leftCounter; break;
        case Directions.DOWN : ++downCounter; break;
    }
}
if(Mathf.Abs(leftCounter - rightCounter) < 4 && Mathf.Abs(downCounter - upCounter) < 4){
break;
}
else{
        pattern.RemoveAt(pattern.Count - 1);
}
}

++maxIndex;
setIsShowing(true);


}

public bool isShowing(){
    return Showing;
}

public void stopMoving(){
    maxIndex = 0;
    currentSteering.linear = new Vector3(0,0,0);
    setIsShowing(false);

    if (agent.blendWeight)
            agent.SetSteering(currentSteering, weight);
        else
            agent.SetSteering(currentSteering);
    this.transform.position = initialPosition;

}

public void setIsShowing(bool arg){
    Showing = arg;
}
public bool compareTwoVectors(Vector3 v1, Vector3 v2){
    bool xBool,yBool = false;

        xBool = Mathf.Abs(v1.x - v2.x) <= 0.1f;
        yBool = Mathf.Abs(v1.z - v2.z) <= 0.1f;
    

    return xBool && yBool;

}

public int sizeOfPattern(){
    return pattern.Count;
}

public Directions getPattern(int i ){
    return pattern[i];
}

}