using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class timeDisplayer : MonoBehaviour
{
    public TextMeshProUGUI timeChosen;
    public Slider sld;
    private float realTime;
   

    public void displayTime()
    {
        realTime = sld.value * 120f;
        timeChosen.text = realTime.ToString() + " seconds.";
    }
    public void erraseTime()
    {
        timeChosen.text = "";
    }
    public float getRealTime()
    {
        return realTime;
    }
}
