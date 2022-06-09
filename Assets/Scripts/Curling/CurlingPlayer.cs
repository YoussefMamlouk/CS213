using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class CurlingPlayer : AgentBehaviour
{
    public GameObject arrow;
    private bool isThrowing;
    private Steering currentSteering;
    public Button btn;
    private float currentTime;
    private float initialVelocity;
    private bool isChoosingForceAndDirection;
    private bool arg;
    // Start is called before the first frame update
    void Start()
    {
        arg = false;
        initialVelocity = 0;
        isChoosingForceAndDirection = false;
        isThrowing = false;


        btn.onClick.AddListener(() => {
            isThrowing = true;
            isChoosingForceAndDirection = true;
            arg = true;
            arrow.SetActive(true);
        });
    }

    public override void FixedUpdate(){
        if(arrow.activeInHierarchy){
            float f =  (415f - Input.mousePosition.x); 
            if(f >= 0f){
                Vector3 r = new Vector3(415f, 432f, 0);
                Vector3 i =  Input.mousePosition - r;

                Vector3 rota = new Vector3(0, 0,-(i.x + i.y)*0.05f);
             
             // rotate
                arrow.transform.Rotate(rota);
                arrow.transform.localScale = new Vector3(f/500f, 0.2361559f , 0.3828728f);
            }
            else{
            arrow.transform.localScale = new Vector3(0.01f,0.01f,0.01f);
            }
            //arrow.transform.position = new Vector3(4.24f + (1/2)* (f/4f - 5f), arrow.transform.position.y, arrow.transform.position.z);
        }
        if(Input.GetMouseButtonDown(0) && isThrowing && arg){
            if(isChoosingForceAndDirection){
            calculateT_Max();
            Debug.Log(initialVelocity);
            arg = false;
            }
        }
        if(currentTime <= initialVelocity){
        currentTime += Time.deltaTime;
        }

        if(isThrowing && !isChoosingForceAndDirection){
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponent<Rigidbody>().AddForce(3 - 3*(currentTime/initialVelocity),0,0,ForceMode.VelocityChange);
     }
     else{
           this.GetComponent<Rigidbody>().velocity = Vector3.zero;
     }
    }

    public void calculateT_Max(){
        Vector3 v = Input.mousePosition;
        initialVelocity = (300f - v.x)/8f - 5f;
        isThrowing = true;
        isChoosingForceAndDirection = false;
    }

}
