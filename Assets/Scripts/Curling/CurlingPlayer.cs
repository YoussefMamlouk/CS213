using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;


public class CurlingPlayer : AgentBehaviour
{
    private bool isThrowing;
    private Steering currentSteering;
    private float currentTime;
    private float initialVelocityX;
    private float initialVelocityZ;
    private bool isChoosingForceAndDirection;
    private bool arg;
    private float scale ;
    private float scaleX;
    private Vector3 currentForce;
    void Start()
    {
        scaleX = 1;
        currentForce = new Vector3(0,0,0);
        arg = false;
        scale = -1;
        initialVelocityX = 0;
        initialVelocityZ = 0;
        
        isChoosingForceAndDirection = false;
        isThrowing = false;
    }


    public override void FixedUpdate(){
        if(Input.GetMouseButtonDown(0) && isThrowing && arg){
            if(isChoosingForceAndDirection){
            calculateT_Max();
            }
        }
        if(isThrowing && !isChoosingForceAndDirection){
        currentTime += Time.deltaTime;
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;

        this.GetComponent<Rigidbody>().AddForce(scaleX * (4f - (currentTime*currentTime/(initialVelocityX))),0, initialVelocityZ,ForceMode.VelocityChange);
        if(Mathf.Abs(4f - (currentTime*currentTime/(initialVelocityX))) < 0.25f && isThrowing){
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        isThrowing = false;
        scale = -1f;
        currentForce = new Vector3(0,0,0);
        }
     }
     else{
           this.GetComponent<Rigidbody>().velocity = Vector3.zero;
     }
    }

    public void calculateT_Max(){

        Vector3 v = Input.mousePosition;
        initialVelocityX = Mathf.Abs((582f - v.x)/7f) - 7f;
        initialVelocityZ = (Input.mousePosition.y - this.transform.position.z)/((Input.mousePosition.x - this.transform.position.x)) - 0.2f ;
        if(538f - Input.mousePosition.y > 0){
            scale = 1;
        }

        if(538f - Input.mousePosition.y > -20f && 538f - Input.mousePosition.y < 20){
            initialVelocityZ = 0;
        }

        initialVelocityZ *= scale * Mathf.Abs((538f - Input.mousePosition.y)/80);
        if(Input.mousePosition.x  < 479f){
        isChoosingForceAndDirection = false;
        arg = false;
        }

    }


    public void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag.Contains("player")){
              collision.gameObject.GetComponent<Rigidbody>().AddForce(currentForce);
        }
        if(collision.gameObject.tag.Contains("Side Border")){
            initialVelocityZ = -(initialVelocityZ/1.5f);
        }
        if(collision.gameObject.tag.Contains("Front Border")){
            scaleX = -1;
        }
    }

    public void startTurn(){
        isThrowing = true;
        isChoosingForceAndDirection = true;
        arg = true;
    }
    public void finishTurn(){
        isChoosingForceAndDirection = false;
        isThrowing = false;
        arg = false;
    }
    
    public bool getIsThrowing(){
        return isThrowing;
    }

}
