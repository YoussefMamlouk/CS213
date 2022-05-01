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
        realTime = Mathf.FloorToInt(sld.value * 270f);
        float f = realTime + 30;
        timeChosen.text = f.ToString() + " seconds.";
    }
    public void erraseTime()
    {
        timeChosen.text = "";
    }
    public float getRealTime()
    {
        return realTime + 30;
    }
}
