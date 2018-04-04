using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wanderBehaviour : MonoBehaviour {

    // Use this for initialization
    public GameObject target;
    Vector3 v = new Vector3(0,0,0);
    int neighborCount = 0;
    int max_velocity = 25;
    float slowingRadius = 2.0f;
    GameObject[] agents;
    public bool Reached = false;





    void Start () {
      agents = GameManager.instance.GetGos();
    }

    


    Vector3 Arrive()
    {
        // Calculate the desired velocity
        GameObject agent = this.gameObject;
        v = agent.GetComponent<Rigidbody>().velocity;
        Vector3 desired_velocity = target.transform.position - agent.transform.position;
        desired_velocity.y = 0;
        float distance = desired_velocity.magnitude;

        // Check the distance to detect whether the character
        // is inside the slowing area
        if (distance < slowingRadius)
        {
            // Inside the slowing area
            desired_velocity.Normalize();
            desired_velocity *= (max_velocity * (distance / slowingRadius));
        }
        else
        {
            // Outside the slowing area.
            desired_velocity.Normalize();
            desired_velocity *= max_velocity;
        }

        // Set the steering based on this
        Vector3 steering = desired_velocity - v;
        return steering;
    }


    Vector3 computeSteering(GameObject agent)
    {
        Vector3 desired_velocity = (target.transform.position - agent.transform.position)*max_velocity;
        desired_velocity.Normalize();
        Vector3 steering = desired_velocity - v;
        return steering;
        
       
    }

    Vector3 Seek(Vector3 target)
    {
        //Reynolds steering behaviour = desired - velocity
        //Should be global variables
        float maxSpeed = 20f;    //fastest possible speed
        float maxForce = 5f;  //turning speed of the object

        //the direction that you need to go to reach the target
        GameObject agent = this.gameObject;
        Vector3 currentVelocity = agent.GetComponent<Rigidbody>().velocity;
        Vector3 desired = target - agent.transform.position;
        float distance = desired.magnitude;
        if (distance < slowingRadius)
        {
            // Inside the slowing area
            //desired_velocity.Normalize();
            //Debug.Log("I am here!" + distance.ToString());
            Reached = true;
            //desired_velocity *= (max_velocity * (distance / slowingRadius));
            //desired_velocity = new Vector3
        }
        else
        {
            desired.Normalize();
            desired *= maxSpeed;
        }
        
        //steering (reynolds steering)
        Vector3 steer = desired - currentVelocity;
        steer.y = 0f; //assuming you want 2D motion on the XZ plane

        //limit the steering
        if (steer.sqrMagnitude > maxForce * maxForce)
        {
            steer.Normalize();
            steer *= maxForce;
        }
       
        return steer;
    }

    void FixedUpdate () {
        GameObject agent = this.gameObject;
        Vector3 force = new Vector3(0, 0, 0);
        

        if (Reached == false)
        {
            if (agent.name == "bruteLeader")
            {
                force = Seek(target.transform.position);
            }
            force.Normalize();
            agent.GetComponent<Rigidbody>().velocity += force * Time.fixedDeltaTime;
            transform.rotation = Quaternion.LookRotation(agent.GetComponent<Rigidbody>().velocity);//change the rotation
        }
        else {
            agent.GetComponent<Rigidbody>().velocity = Vector3.zero;
            agent.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        
        
        Debug.Log(agent.transform.position);
        Debug.Log(target.transform.position);
        Debug.Log("steer" + force);
    }
}
