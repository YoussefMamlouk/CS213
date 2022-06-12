
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Input Keys
public enum InputKeyboard
{
    arrows = 0,
    wasd = 1
}
public class MoveWithKeyboardBehavior : AgentBehaviour
{
    private InputKeyboard inputKeyboard;
    //public MoveWithKeyboardBehavior otherPlayer;
    private bool ready = false;
    private bool onPause;
    public Timer timer;
    private bool gotBlockedByObject;
    private float timerOfBlock;
    public GameObject ghostSheep;
    public void pauseUnpause()
    {
        onPause = !onPause;
    }
    public void Start()
    {
        onPause = false;
        gotBlockedByObject = false;
        timerOfBlock = 0.0f;
    }


    public void changeStatusToWasd()
    {
        Debug.Log("wasd");
        inputKeyboard = InputKeyboard.wasd;
    }
    public void changeStatusToArrows()
    {
        Debug.Log("arrows");
        inputKeyboard = InputKeyboard.arrows;

    }
    public bool getOnPause()
    {
        return onPause;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Contains("blockObject"))
        {
            //otherPlayer.playerBlocked();
            //timer.destroyBlock();
        }
    }
    public void playerBlocked()
    {
        gotBlockedByObject = true;
        timerOfBlock = 10.0f;
    }
    public override void FixedUpdate()
    {
        if (gotBlockedByObject)
        {
            timerOfBlock -= Time.deltaTime;
            if (timerOfBlock <= 0.0f)
            {
                resetPlayerMovement();
            }
        }

    }
    public void resetPlayerMovement()
    {
        gotBlockedByObject = false;
    }


    public override Steering GetSteering()
    {
        canMove = !tmr.isGameOverOrNot();
        Steering steering = new Steering();
        if (canMove && !onPause && !gotBlockedByObject)
        {
            float horizontal = 0f;
            float vertical = 0f;


            //implement your code here
            if (inputKeyboard == InputKeyboard.arrows)
            {
                horizontal = Input.GetAxis("Horizontal");
                vertical = Input.GetAxis("Vertical");
            }
            else if (inputKeyboard == InputKeyboard.wasd)
            {
                horizontal = Input.GetAxis("Horizontal WASD");
                vertical = Input.GetAxis("Vertical WASD");
            }

            if (this.CompareTag("Player1"))
            {
                if (ghostSheep.GetComponent<StrikerDefenderBehaviour>().getstate() == 1.0f)
                {
                    agent.SetMaxSpeed(4.0f);
                }
                else
                    agent.SetMaxSpeed(1.5f);
            }
            if (this.CompareTag("Player2"))
            {
                if (ghostSheep.GetComponent<StrikerDefenderBehaviour>().getstate() == -1.0f)
                {
                    agent.SetMaxSpeed(4.0f);
                }
                else
                    agent.SetMaxSpeed(1.5f);
            }

            steering.linear = new Vector3(horizontal, 0, vertical) * agent.maxAccel;
            steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear, agent.maxAccel));

        }
        return steering;

    }

    public override void OnCelluloLongTouch(int key)
    {
        ready = true;
    }

    public bool getReady()
    {
        return ready;
    }

}