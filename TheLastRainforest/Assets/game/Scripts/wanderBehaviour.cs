using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wanderBehaviour : MonoBehaviour {

    // Use this for initialization
    public GameObject target;
    Vector3 v = new Vector3(0,0,0);
    int neighborCount = 0;
    int max_velocity = 25;
    float slowingRadius = 0.5f;
    GameObject[] agents;






    void Start () {
      agents = GameManager.instance.GetGos();
    }

    Vector3 computeAlignment(GameObject agent)
    {
        for (int i = 0; i < agents.Length; i++)
        {
            if (agent != agents[i])
            {
                if (Vector3.Distance(agent.transform.position,agents[i].transform.position) < 500)
                {
                    v.x += agents[i].GetComponent<Rigidbody>().velocity.x;
                    v.z += agents[i].GetComponent<Rigidbody>().velocity.z;

                    neighborCount++;
                }

            }

        }
        if (neighborCount == 0)
            return v;

        v.x /= neighborCount;
        v.z /= neighborCount;
        v.Normalize();
        return v;
    }


    Vector3 computeCohesion(GameObject agent)
    {
        for (int i = 0; i < agents.Length; i++)
        {
            if (agent != agents[i])
            {
                if (Vector3.Distance(agent.transform.position, agents[i].transform.position) < 500)
                {
                    v.x += agents[i].transform.position.x;
                    v.z+= agents[i].transform.position.z;
                    neighborCount++;
                }

            }

        }
        if (neighborCount == 0)
            return v;

        v.x /= neighborCount;
        v.z /= neighborCount;
        v = new Vector3(v.x - agent.transform.position.x, v.z - agent.transform.position.z);
        v.Normalize();
        return v;
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

        void FixedUpdate () {
        GameObject agent = this.gameObject;
        Vector3 steering = new Vector3(0, 0, 0);
        Vector3 alignment = computeAlignment(agent);
        Vector3 cohesion = computeCohesion(agent);
        if(agent.name == "bruteLeader")
        {
            steering = computeSteering(agent);
        }
        //Vector3 vel = agent.GetComponent<Rigidbody>().velocity;
        //Vector3 force = alignment + cohesion  + (separation*10)+(steering*50);
        // Vector3 force = computeSteering(agent);
        Vector3 force = Arrive();
        force.Normalize();
        agent.GetComponent<Rigidbody>().AddForce(force);
        agent.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(agent.GetComponent<Rigidbody>().velocity, max_velocity);
        //transform.rotation = Quaternion.LookRotation(agent.GetComponent<Rigidbody>().velocity);//change the rotation


        //Debug.Log(vel);

        /* vel.x += alignment.x + cohesion.x + separation.x;
         agent.GetComponent<Rigidbody>().velocity = vel;
         vel = agent.GetComponent<Rigidbody>().velocity;
         vel.z += alignment.z + cohesion.z + separation.z;

         vel.Normalize();
         agent.GetComponent<Rigidbody>().velocity = vel;*/
        //agent.GetComponent<Rigidbody>().velocity.Normalize();
        //agent.GetComponent<Rigidbody>().velocity *= 2;
        //Debug.Log("an agent starts here");
        //Debug.Log(agent.GetComponent<Rigidbody>().velocity);

        //Debug.Log(alignment);
        //Debug.Log(cohesion);
        //Debug.Log(separation);
    }
}
