using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderFollow : MonoBehaviour {

    // Use this for initialization
    Vector3 v = new Vector3(0, 0, 0);
    int neighborCount = 1;    
    public GameObject leader;
    GameObject agent;
    float slowingRadius = 0.5f;
    int max_velocity = 15;
    int max_force = 10;
    int LEADER_BEHIND_DIST = 20;
    Vector3 steering = Vector3.zero; // the null vector, meaning "zero force magnitude"
    GameObject[] agents;


    // Use this for initialization
    void Start () {
        agent = this.gameObject;
        v = agent.GetComponent<Rigidbody>().velocity;
        agents = GameManager.instance.GetGos();

    }

    Vector3 computeSeparation(GameObject agent)
    {
        int max_separation = 10;
        for (int i = 0; i < agents.Length; i++)
        {
            if (agent != agents[i])
            {
                if (Vector3.Distance(agent.transform.position, agents[i].transform.position) <= max_separation)
                {
                    v.x += agents[i].transform.position.x - agent.transform.position.x;
                    v.z += agents[i].transform.position.z - agent.transform.position.z;

                    neighborCount++;
                }

            }

        }
        if (neighborCount == 0)
        {
            v.Normalize();
            v.Scale(new Vector3(max_separation,max_separation,max_separation));
            return v;
        }
            

        v.x /= neighborCount;
        v.z /= neighborCount;
        v = new Vector3(v.x - agent.transform.position.x, v.z - agent.transform.position.z);
        v.Normalize();
        v.x *= -max_separation;
        v.z *= -max_separation;
        return v;
    }

    Vector3 followLeader(){
        Vector3 tv = leader.GetComponent<Rigidbody>().velocity;
        
        // Calculate the behind point
        tv.Scale(new Vector3(-1,-1,-1));
        tv.Normalize();
        tv.Scale(new Vector3(LEADER_BEHIND_DIST, LEADER_BEHIND_DIST, LEADER_BEHIND_DIST));
        Vector3 behind = leader.transform.position + tv;

        // Creates a force to arrive at the behind point
        Vector3 force = Arrive(behind);
        force += computeSeparation(agent);
 
        return force;
    }

    Vector3 Arrive(Vector3 position)
    {
        // Calculate the desired velocity
        v = agent.GetComponent<Rigidbody>().velocity;
        Vector3 desired_velocity = leader.transform.position - position;
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

    void FixedUpdate()
    {
        steering = steering + followLeader();
        steering = Vector3.ClampMagnitude(steering, max_force);
        steering = steering / agent.GetComponent<Rigidbody>().mass;
        v = v + steering;
        agent.GetComponent<Rigidbody>().velocity += v* Time.fixedDeltaTime;
        agent.GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(agent.GetComponent<Rigidbody>().velocity, max_velocity); 
    }

    // Update is called once per frame
    void Update () {
		
	}
}
