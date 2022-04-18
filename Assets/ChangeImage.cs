using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ChangeImage : MonoBehaviour
{
    public enum Sound
    {
        on = 0,
        off = 1 
    }
    public Sprite mutedImage;
    public Sprite unmutedImage;
    public Button changeMuteButton;
    private Sound sound;


    void Start()
    {
        sound = Sound.on;
    }

    public void changeImage()
    {
        if (sound == Sound.on) {
            changeMuteButton.image.sprite = mutedImage;
            sound = Sound.off;
        }
        else if(sound == Sound.off)
        {
            changeMuteButton.image.sprite = unmutedImage;
            sound = Sound.on;
        }
    }
}
