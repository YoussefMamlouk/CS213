using System.Linq;
using UnityEngine;

public class GhostSheepBehavior : AgentBehaviour
{
    private float minDistance = 20.0f;
    private float timer;
    // -1 if fuit, 1 if suit
    private float state;
    private float currentTime;
    public AudioClip audioSheep;
    public AudioClip audioWolf;
    public bool musicPlaying;
    private bool allyFind;
    private GameObject losingPlayer;
    private GameObject winningPlayer;
    public Timer overallTimer;
    public float publicTimer;




    public AudioClip losePoint;
    private AudioSource src;
    private GameObject[] dogs;

    private bool onPause;
     public void pauseUnpause(){
        onPause = !onPause;
    }
    public void Start()
    {

        onPause = false;
        allyFind = false;
        src = GetComponent<AudioSource>();
        musicPlaying = true;
        state = 1.0f;
        changeState();
        currentTime = 0.0f;
        timer = Random.Range(10.0f, 20.0f);
        dogs = GameObject.FindGameObjectsWithTag("CelluloDog");

    }
    public void changeState()
    {

        currentTime = 0.0f;
        timer = Random.Range(10.0f, 20.0f);
        state = -state;
        
        if (!tmr.isGameOverOrNot() && !onPause)
        {
            if (state == 1.0f)
            {
                src.clip = audioWolf;
                transform.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.red, 0);

                dogs[0].GetComponent<CelluloAgentRigidBody>().MoveOnStone();
                dogs[1].GetComponent<CelluloAgentRigidBody>().MoveOnStone();
            }
            else if (state == -1)
            {
                src.clip = audioSheep;
                transform.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.green, 0);
                minDistance = 20.0f;

                dogs[0].GetComponent<CelluloAgentRigidBody>().ClearHapticFeedback();
                dogs[1].GetComponent<CelluloAgentRigidBody>().ClearHapticFeedback();
                dogs[0].GetComponent<CelluloAgentRigidBody>().SetCasualBackdriveAssistEnabled(true);
                dogs[1].GetComponent<CelluloAgentRigidBody>().SetCasualBackdriveAssistEnabled(true);
            }
            if (musicPlaying) { src.Play();
            }
            else
            {
                src.Stop();
            }
        
           
        }

    }
    public void activateJoker(){
        //src.clip = " clip du jok ";
        state = 1.0f;
        Debug.Log(publicTimer%60.0f);
        if(publicTimer%60.0f >= 9.9f){
            allyFind = false;
            changeState();
        }
        else{
         transform.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.yellow, 0);

        GameObject cellulo1 = dogs[0];
         int score1  = cellulo1.GetComponent<ChangeScore>().getScore();
        GameObject cellulo2 = dogs[1];
         int score2 = cellulo2.GetComponent<ChangeScore>().getScore();
        allyFind = true;
        if(score1 > score2){
            losingPlayer = cellulo2;
            winningPlayer = cellulo1;
        }
        else if( score2 > score1){
            losingPlayer = cellulo1;
            winningPlayer = cellulo2;
        }
        winningPlayer.GetComponent<CelluloAgentRigidBody>().MoveOnStone();
        losingPlayer.GetComponent<CelluloAgentRigidBody>().ClearHapticFeedback();
        losingPlayer.GetComponent<CelluloAgentRigidBody>().SetCasualBackdriveAssistEnabled(true);
        }

    }
    public void muteUnmute()
    {
        musicPlaying = !musicPlaying;
    }

    public override void FixedUpdate()
    {
        //Debug.Log(allyFind);
        publicTimer = overallTimer.getTime(); 
        canMove = !tmr.isGameOverOrNot();
        if (canMove && !tmr.isGameOverOrNot() && !onPause)
        {
            currentTime += Time.deltaTime;
            if(publicTimer%60.0f < 10.0f  &&  publicTimer > 10.0f){
             activateJoker();
            }
            else{
            if (currentTime >= timer && !allyFind)
            {
                changeState();
            }
            }

            if (agent.blendWeight)
                agent.SetSteering(GetSteering(), weight);
            else
                agent.SetSteering(GetSteering());
        }

    }
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        steering.linear = new Vector3(0, 0, 0);
        if (canMove && !onPause){
            
            GameObject celluloDog;
            float distance = Mathf.Infinity;
            if(allyFind){
                celluloDog = winningPlayer;
                distance = 1.0f;

            }else{
            (celluloDog, distance) = FindClosestEnemy(distance);
            }
            if (distance != Mathf.Infinity)
            {

                Vector3 direction = (celluloDog.transform.position - transform.position) * state;
                direction.Normalize();
                if (state == 1.0f)
                {

                    steering.linear = direction * (agent.maxAccel - 2);
                }
                else
                {

                    steering.linear = direction * agent.maxAccel;
                }
                steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.
                                    linear, agent.maxAccel));
            }
            
        }
        return steering;
        
    }

    public (GameObject, float) FindClosestEnemy(float distance)
    {

        if (state == 1.0f)
        {
            minDistance = Mathf.Infinity;
        }
        
        GameObject closest = null;

        Vector3 position = gameObject.transform.position;
        foreach (GameObject dog in dogs)
        {
            Vector3 diff = dog.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && curDistance < minDistance)
            {
                closest = dog;
                distance = curDistance;
            }
        }
        return (closest, distance);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (state == 1.0f)
        {
            collision.gameObject.GetComponent<ChangeScore>().decrementScore();
            src.clip = losePoint;
            if (musicPlaying)
            {
                src.Play();
            }
            else
            {
                src.Stop();
            }

        }
    }
     
    public float getstate() {
        return state;
    }




}
