using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGem : MonoBehaviour
{
    public GameObject gem;
    public GameObject player1;
    public GameObject player2;
    private bool spawned;
    public Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        spawned = false;
        gem.SetActive(false);
    }

    // Update is called once per frame
    public void Update()
    {
        float rng = Random.Range(-5f, 5f);

        Debug.Log("yes");

        if((rng > 4.5f && !spawned ) || (Mathf.FloorToInt(timer.initTimerValue) == 10))
        {
            appearGem();
        }
    }
    private void appearGem()
    {
        Vector3 gemPosition = new Vector3(Random.Range(0f, 16f), 0f, Random.Range(-8f, 0f));

        while ((gemPosition - player1.transform.position).magnitude < 1 || (gemPosition - player2.transform.position).magnitude < 1)
        {
            gemPosition = new Vector3(Random.Range(0f, 16f), 0f, Random.Range(-8f, 0f));
        }
        Instantiate(gem, gemPosition, Quaternion.identity);
        spawned = true;
    }

    public void gemDisappeared()
    {
        spawned = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gem); 
        gemDisappeared();
    }
}
