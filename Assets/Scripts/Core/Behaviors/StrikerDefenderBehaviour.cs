using System.Linq;
using UnityEngine;

public class StrikerDefenderBehaviour : AgentBehaviour
{
    private float minDistance = 8.0f;
    
    private float timer;
    // -1 if dog 1 defender, 1 if striker
    private float state;
    private float currentTime;
    public AudioClip whistle;
    public bool musicPlaying;
    public Timer overallTimer;
    public float publicTimer;
    public AudioClip losePoint;
    private AudioSource src;
    private AudioSource src2;
    private GameObject p1;
    private GameObject p2;
    public ParticleSystem magic_ring;

    private float prevScore1;
    private float prevScore2;

    private bool onPause;
     public void pauseUnpause(){
        onPause = !onPause;
    }
    public void Start()
    {
        
        p1 = GameObject.FindGameObjectsWithTag("Player1")[0];
        p2 = GameObject.FindGameObjectsWithTag("Player2")[0];
        onPause = false;
        src = GetComponent<AudioSource>();
        musicPlaying = true;
        state = 1.0f;
        //changeState();
        currentTime = 0.0f;
        timer = 30.0f;

        Debug.Log(p1.GetComponent<CelluloAgentRigidBody>().initialColor);
        
        

    }
    public void changeState()
    {

        currentTime = 0.0f;
        timer = 30.0f;
        state = -state;
        

        if (!tmr.isGameOverOrNot() && !onPause)
        {
            src.clip = whistle;
            if (musicPlaying)
                src.Play();
            if (state == 1.0f)
            {
                magic_ring.GetComponent<ChangeColor>().UpdateColor(p1.GetComponent<CelluloAgentRigidBody>().initialColor);

                prevScore1 = p1.GetComponent<ChangeScore>().getScore();
                if (prevScore2 == p2.GetComponent<ChangeScore>().getScore()){
                    p2.GetComponent<ChangeScore>().decrementScore();
                    src.clip = losePoint;
                    if (musicPlaying)
                        src.Play();
                }
                transform.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.red, 0);
                p1.GetComponent<CelluloAgentRigidBody>().ClearHapticFeedback();
                p2.GetComponent<CelluloAgentRigidBody>().ClearHapticFeedback();
                p1.GetComponent<CelluloAgentRigidBody>().MoveOnIce();
                //p1.GetComponent<CelluloAgentRigidBody>().SetCasualBackdriveAssistEnabled(true);
                p2.GetComponent<CelluloAgentRigidBody>().MoveOnMud();
            }
            else if (state == -1)
            {
                magic_ring.GetComponent<ChangeColor>().UpdateColor(p2.GetComponent<CelluloAgentRigidBody>().initialColor);

                prevScore2 = p2.GetComponent<ChangeScore>().getScore();
                if (prevScore1 == p1.GetComponent<ChangeScore>().getScore()){
                    p1.GetComponent<ChangeScore>().decrementScore();
                    src.clip = losePoint;
                    if (musicPlaying)
                        src.Play();
                }

                
            
                transform.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.green, 0);
                                
                p2.GetComponent<CelluloAgentRigidBody>().ClearHapticFeedback();
                p1.GetComponent<CelluloAgentRigidBody>().ClearHapticFeedback();


                //p2.GetComponent<CelluloAgentRigidBody>().SetCasualBackdriveAssistEnabled(true);
                p2.GetComponent<CelluloAgentRigidBody>().MoveOnIce();
                p1.GetComponent<CelluloAgentRigidBody>().MoveOnMud();
            }
            if (musicPlaying) { src.Play();
            }
            else
            {
                src.Stop();
            }
        
           
        }

    }
    
    public void muteUnmute()
    {
        musicPlaying = !musicPlaying;
    }

    public override void FixedUpdate()
    {
        publicTimer = overallTimer.getTime(); 
        Debug.Log(publicTimer);
        canMove = !tmr.isGameOverOrNot();
        if (canMove && !tmr.isGameOverOrNot() && !onPause)
        {
            currentTime += Time.deltaTime;
            

            if (currentTime >= timer)
            {
                changeState();
            }

            if (agent.blendWeight)
                agent.SetSteering(GetSteering(), weight);
            else
                agent.SetSteering(GetSteering());
        }

        if (publicTimer > 0 && publicTimer < 0.06){
            src.clip = whistle;
            if (musicPlaying)
                src.Play();
            magic_ring.GetComponent<ChangeColor>().UpdateColor(p1.GetComponent<CelluloAgentRigidBody>().initialColor);
            transform.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.red, 0);
            p1.GetComponent<CelluloAgentRigidBody>().ClearHapticFeedback();
            
            p1.GetComponent<CelluloAgentRigidBody>().MoveOnIce();
            p2.GetComponent<CelluloAgentRigidBody>().MoveOnSandpaper();
        }

    }
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        steering.linear = new Vector3(0, 0, 0);
        if (canMove && !onPause){
            
            GameObject celluloDog;
            float distance = Mathf.Infinity;
            (celluloDog, distance) = FindClosestEnemy(distance);
            if (distance != Mathf.Infinity)
            {
                agent.SetMaxSpeed(3.0f);
                Vector3 direction = -(celluloDog.transform.position - transform.position);
                steering.linear = direction * agent.maxAccel;
                
                steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.
                                    linear, agent.maxAccel));
            }
        }    
            
        return steering;
        
    }    

    public (GameObject, float) FindClosestEnemy(float distance)
    {
        
        GameObject closest = null;

        Vector3 position = gameObject.transform.position;
        GameObject[] dogs = {p1, p2}; 
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
     
    public float getstate() {
        return state;
    }

    public (float, float) getScores(){
        return (prevScore1, prevScore2);
    }


}
