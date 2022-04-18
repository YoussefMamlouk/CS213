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
        float horizontal = 0f;
        float vertical = 0f;
        Steering steering = new Steering();

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
        return steering;
    }

}
