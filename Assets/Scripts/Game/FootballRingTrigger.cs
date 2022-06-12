using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballRingTrigger : MonoBehaviour
{

    public AudioClip winPoint;
    private AudioSource src;
    private bool musicPlaying;



    // Start is called before the first frame update
    void Start()
    {
        src = GetComponent<AudioSource>();
        musicPlaying = true;

    }
    public void muteUnmute()
    {
        musicPlaying = !musicPlaying;
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.parent.gameObject.name + " triggers.");
        StrikerDefenderBehaviour ghostSheep = other.transform.parent.gameObject.GetComponent<StrikerDefenderBehaviour>();
        if (other.transform.parent.gameObject.CompareTag("ghostSheep"))
        {

            //change closest to attacker
            GameObject[] p1;
            GameObject[] p2;
            p1 = GameObject.FindGameObjectsWithTag("Player1");
            p2 = GameObject.FindGameObjectsWithTag("Player2");

            GameObject striker = ghostSheep.getstate()==1.0f ? p1[0] : p2[0];
            striker.GetComponent<ChangeScore>().incrementScore();
            
            src.clip = winPoint;
            if (musicPlaying)
            {
                src.Play();
            }
        }

    }


}
