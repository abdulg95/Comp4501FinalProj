using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wanderBehaviour : MonoBehaviour {

    // Use this for initialization
    Vector3 v = new Vector3(0,0,0);
    int neighborCount = 0;
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
                    v.y += agents[i].GetComponent<Rigidbody>().velocity.y;

                    neighborCount++;
                }

            }

        }
        if (neighborCount == 0)
            return v;

        v.x /= neighborCount;
        v.y /= neighborCount;
        v.Normalize();
        return v;
    }

    Vector3 computeCohesion(GameObject agent)
    {
        for (int i = 0; i < agents.Length; i++)
        {
            if (agent != agents[i])
            {
                if (Vector3.Distance(agent.transform.position, agents[i].transform.position) < 300)
                {
                    v.x += agents[i].transform.position.x;
                    v.y+= agents[i].transform.position.x;
                    neighborCount++;
                }

            }

        }
        if (neighborCount == 0)
            return v;

        v.x /= neighborCount;
        v.y /= neighborCount;
        v = new Vector3(v.x - agent.transform.position.x, v.y - agent.transform.position.y);
        v.Normalize();
        return v;
    }

    Vector3 computeSeparation(GameObject agent)
    {
        for (int i = 0; i < agents.Length; i++)
        {
            if (agent != agents[i])
            {
                if (Vector3.Distance(agent.transform.position, agents[i].transform.position) < 150)
                {
                    v.x += agents[i].transform.position.x - agent.transform.position.x;
                    v.y += agents[i].transform.position.y - agent.transform.position.y;

                    neighborCount++;
                }

            }

        }
        if (neighborCount == 0)
            return v;

        v.x /= neighborCount;
        v.y /= neighborCount;
        v = new Vector3(v.x - agent.transform.position.x, v.y - agent.transform.position.y);
        v.x *= -1;
        v.y *= -1;
        v.Normalize();
        return v;
    }



    // Update is called once per frame
    void Update () {
        GameObject agent = this.gameObject;
        Vector3 alignment = computeAlignment(agent);
        Vector3 cohesion = computeCohesion(agent);
        Vector3 separation = computeSeparation(agent);
        Vector3 vel = agent.GetComponent<Rigidbody>().velocity;
        //Debug.Log("an agent starts here");
        //Debug.Log(vel);

        vel.x += alignment.x + cohesion.x + separation.x;
        agent.GetComponent<Rigidbody>().velocity = vel;
        vel = agent.GetComponent<Rigidbody>().velocity;
        vel.y += alignment.y + cohesion.y + separation.y;

        vel.Normalize();
        agent.GetComponent<Rigidbody>().velocity = vel;
        //agent.GetComponent<Rigidbody>().velocity.Normalize();
        agent.GetComponent<Rigidbody>().velocity *= 2;
        //Debug.Log("an agent starts here");
        //Debug.Log(agent.GetComponent<Rigidbody>().velocity);
       
        //Debug.Log(alignment);
        //Debug.Log(cohesion);
        //Debug.Log(separation);
    }
    }
