using UnityEngine;
using System.Collections;
public class AgentBehaviour : MonoBehaviour
{
    public float weight = 1.0f;
    protected CelluloAgent agent;
    public bool canMove = false;
    public Timer tmr;

    public virtual void Awake()
    {
        agent = gameObject.GetComponent<CelluloAgent>();
        if (agent == null)
        {
            Debug.LogWarning("An active CelluloAgent should be attached to the same gameobject.");
        }
    }
    public virtual void FixedUpdate()
    {
        
        canMove = !tmr.isGameOverOrNot();
        if (tmr != null){
            if (canMove)
            {
                if (agent.blendWeight)
                    agent.SetSteering(GetSteering(), weight);
                else
                    agent.SetSteering(GetSteering());
            }
        
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

    /// <summary>
    /// Virtual function, to be implemented if OnTouchBegan from Real robot is needed
    /// </summary>
    /// <param name="key">
    /// the number of the key touch sensor 
    /// </param>
    public virtual void OnCelluloTouchBegan(int key) { }
    /// <summary>
    /// Virtual function, to be implemented if OnTouchReleased from Real robot is needed
    /// </summary>
    /// <param name="key">
    /// the number of the key touch sensor 
    /// </param>
    public virtual void OnCelluloTouchReleased(int key) { }
    /// <summary>
    /// Virtual function, to be implemented if OnLongTouch from Real robot is needed
    /// </summary>
    /// <param name="key">
    /// the number of the key touch sensor 
    /// </param>
    public virtual void OnCelluloLongTouch(int key) { }

    /// <summary>
    /// Event signaled when robot kidnapping ends 
    /// </summary>
    public virtual void OnCelluloUnKidnapped() { }
    /// <summary>
    /// Event signaled when robot is kidnapped
    /// </summary>
    public virtual void OnCelluloKidnapped() { }

}