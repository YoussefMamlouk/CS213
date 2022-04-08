using UnityEngine;
using UnityEngine.UI;
using TMPro;

/**
	This class is the implementation of the timer used in the game and how it is handled in it
*/
public class Timer : MonoBehaviour
{
    private float initTimerValue;
 
    public TextMeshProUGUI timerText;
    public Slider slider;
    public float maxMinutes = 5;
    public GameManager gameManager;
    public bool timerStart = false;

    public void Awake() {
        initTimerValue = Time.time; 
    }

    // Start is called before the first frame update
    public void Start() {
        resetTimer();
        //timerText = this.gameObject.transform.GetChild(0);
        timerText.text = string.Format("{0:00}:{1:00}", 0, 0);
    }

    // Update is called once per frame
    public void Update() {
        if (timerStart)
        {
            if (initTimerValue <= 120.0f)
            {
                initTimerValue += Time.deltaTime;
                float minutes = Mathf.FloorToInt(initTimerValue / 60);
                float seconds = Mathf.FloorToInt(initTimerValue % 60);
                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                timerStart = false;
            }
            
        }
        slider.value = initTimerValue;
    }

    public void resetTimer()
    {
        initTimerValue = 0;
    }

    public void startTimer()
    {
        timerStart = true;
    }
}
