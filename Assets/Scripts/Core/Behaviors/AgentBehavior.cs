using UnityEngine;
using System.Collections;
public class AgentBehaviour : MonoBehaviour
{
    public float weight = 1.0f;
    protected CelluloAgent agent;
    public bool canMove = false;

    public virtual void Awake()
    {
        agent = gameObject.GetComponent<CelluloAgent>();
    }
    public virtual void FixedUpdate()
    {
        if(canMove){
            if (agent.blendWeight)
                agent.SetSteering(GetSteering(), weight);
            else
                agent.SetSteering(GetSteering());
        }
    }
    public virtual Steering GetSteering()
    {
        return new Steering();
    }

    public void setIsMoving()
    {
        canMove = true;
    }
}
