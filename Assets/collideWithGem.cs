using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collideWithGem : MonoBehaviour
{
    public Timer timer;
    private bool gainTwoPoints;
    

    public void Start()
    {
        gainTwoPoints = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Contains("Gem"))
        {
            timer.destroyGem();
            gainTwoPoints = true;
        }
        if (collision.gameObject.tag.Contains("CelluloDog") && gainTwoPoints)
        {
            collision.gameObject.GetComponent<ChangeScore>().decrementScore();
            collision.gameObject.GetComponent<ChangeScore>().decrementScore();
            this.GetComponent<ChangeScore>().incrementScore();
            this.GetComponent<ChangeScore>().incrementScore();
            timer.bonusApplied();

            gainTwoPoints = false;
        }
    }

}
