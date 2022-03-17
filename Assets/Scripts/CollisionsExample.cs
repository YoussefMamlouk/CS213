using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionsExample : MonoBehaviour
{
    void OnCollisionEnter(Collision collisioninfo)
    {
        print("Detected collision between " + gameObject.name + " and " + collisioninfo.collider.name);
        print("There are " + collisioninfo.contacts.Length + " point(s) of contacts");
        print("Their relative velocity is " + collisioninfo.relativeVelocity);
    }
    void OnCollisionStay(Collision collisionInfo)
    {
        print(gameObject.name + " and " + collisionInfo.collider.name + " are still colliding");
    }
    void OnCollisionExit(Collision collisionInfo)
    {
        print(gameObject.name + " and " + collisionInfo.collider.name + " are no longer colliding");
    }
}
