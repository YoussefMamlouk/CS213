using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btn : AgentBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void FixedUpdate(){
        this.transform.position = player.transform.position ;
    }
}
