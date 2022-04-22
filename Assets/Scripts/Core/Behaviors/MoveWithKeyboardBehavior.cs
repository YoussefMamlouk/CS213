using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Input Keys
public enum InputKeyboard{
    arrows = 0, 
    wasd = 1
}
public class MoveWithKeyboardBehavior : AgentBehaviour
{
    private InputKeyboard inputKeyboard;
    private bool ready = false;

    public void changeStatusToWasd()
    {
         inputKeyboard = InputKeyboard.wasd;          
        
    }
    public void changeStatusToArrows()
    {
    inputKeyboard = InputKeyboard.arrows;
    }
    

    public override Steering GetSteering()
    {
        canMove = !tmr.isGameOverOrNot();
        Steering steering = new Steering();
        if (canMove){
            float horizontal = 0f;
            float vertical = 0f;
            

            //implement your code here
            if (inputKeyboard == InputKeyboard.arrows)
            {
                horizontal = Input.GetAxis("Horizontal");
                vertical = Input.GetAxis("Vertical");
            }
            else if(inputKeyboard == InputKeyboard.wasd)
            {
                horizontal = Input.GetAxis("Horizontal WASD");
                vertical = Input.GetAxis("Vertical WASD");
            }
            

        
            steering.linear = new Vector3(horizontal, 0, vertical) * agent.maxAccel;
            steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear, agent.maxAccel));
            
        }
        return steering;
        
    }

    public override void OnCelluloLongTouch(int key){
        ready = true;
    }

    public bool getReady(){
        return ready;
    }

}
