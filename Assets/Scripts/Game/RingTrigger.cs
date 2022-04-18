using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingTrigger : MonoBehaviour
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

        if (other.transform.parent.gameObject.CompareTag("ghostSheep"))
        {

            GameObject closest = FindClosestEnemy();
            closest.GetComponent<ChangeScore>().incrementScore();
            src.clip = winPoint;
            if (musicPlaying)
            {
                src.Play();
            }
        }

    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] dogs;
        float distance = Mathf.Infinity;
        dogs = GameObject.FindGameObjectsWithTag("CelluloDog");
        GameObject closest = null;

        Vector3 position = gameObject.transform.position;
        foreach (GameObject dog in dogs)
        {
            Vector3 diff = dog.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = dog;
                distance = curDistance;
            }
        }
        return closest;
    }
}
