using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingTrigger : MonoBehaviour
{
    private int playerOneScore;
    private int playerTwoScore;

    // Start is called before the first frame update
    void Start()
    {
        playerOneScore = 0; playerTwoScore = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnTriggerEnter(Collider other){
        Debug.Log(other.transform.parent.gameObject.name + " triggers.");

        if (this.CompareTag("ghostSheep")){
            float diffWithPlayerOne = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);
            float diffWithPlayerTwo = Vector3.Distance(GameObject.FindGameObjectWithTag("Player2").transform.position, transform.position);


            if (diffWithPlayerOne > diffWithPlayerTwo)
            {
                ++playerTwoScore;
            }
            else
            {
                ++playerOneScore;

            }
        }
        
    }
}
