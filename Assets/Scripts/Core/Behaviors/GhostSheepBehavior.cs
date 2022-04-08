﻿using System.Linq;
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
    private AudioSource src;
    

    public void Start()
    {
        src = GetComponent<AudioSource>();
        state = -1.0f;
        currentTime = 0.0f;
        timer = 15.0f;

    }
    public void changeState()
    {
        currentTime = 0.0f;
        timer = Random.Range(10.0f, 20.0f);
        state = -state;
        
        if( state == 1.0f)
        {
            src.clip = audioWolf;
            transform.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.red, 0);
        }
        else
        {
            src.clip = audioSheep;
            transform.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.green, 0);
            minDistance = 20.0f;
        }
        src.Play();
       
    
    }
    public override void FixedUpdate()
    {

        if (canMove)
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

    }
    public override Steering GetSteering()
    {

        Steering steering = new Steering();

        GameObject celluloDog;
        float distance = Mathf.Infinity;
        (celluloDog, distance) = FindClosestEnemy(distance);
        steering.linear = new Vector3(0, 0, 0);
        if (distance != Mathf.Infinity)
        {

            Vector3 direction = (celluloDog.transform.position - transform.position) * state;
            direction.Normalize();
            steering.linear = direction * agent.maxAccel;
            steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.
                                linear, agent.maxAccel));
        }
        return steering;
    }

    (GameObject, float) FindClosestEnemy(float distance)
    {
        GameObject[] dogs;
        if(state == 1.0f)
        {
            minDistance = Mathf.Infinity;
        }
        dogs = GameObject.FindGameObjectsWithTag("CelluloDog");
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



}
