using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonBehavior : MonoBehaviour
{
    public GameObject btnPlayer1WASD;
    public GameObject btnPlayer1Arrow;
    public GameObject btnPlayer2WASD;
    public GameObject btnPlayer2Arrows;
    public GameObject PlayButton;


    // Update is called once per frame
    void Update()
    {
        if((btnPlayer1WASD.activeInHierarchy && btnPlayer2Arrows.activeInHierarchy)
            || (btnPlayer2WASD.activeInHierarchy && btnPlayer1Arrow.activeInHierarchy))
        {
            PlayButton.SetActive(true);
        }
        else
        {
            PlayButton.SetActive(false);
        }
    }
}
