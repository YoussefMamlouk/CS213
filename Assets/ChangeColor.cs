using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public ParticleSystem ring; 
    public ParticleSystem effect; 
    // Start is called before the first frame update
    public void UpdateColor(Color color){
        var color_ring = ring.colorOverLifetime;
        color_ring.enabled = true;
        color_ring.color = color;
        
        var color_effect = effect.colorOverLifetime;
        color_effect.enabled = true;
        color_effect.color = color;
    }
}
