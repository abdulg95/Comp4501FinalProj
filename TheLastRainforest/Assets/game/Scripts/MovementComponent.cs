using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementComponent : MonoBehaviour {

    public float maxSpeed;
    public float currentSpeed;
    public Vector3 target ;
    public int arrivalThreshold = 2;
    public int followDistance = 1;
    Rigidbody rb;
    bool needsToMove = false;
    Vector3 empty = new Vector3(0, 0, 0);
    Vector3 initial_pos;
    Vector3 final_pos;
    public GameObject leader;
    bool follow = false;



    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        target = transform.position;
    }



    float Ease(float t)
    {
        return (Mathf.Sin(t * Mathf.PI - Mathf.PI / 2.0f) + 1.0f) / 2.0f;
    }

    /*
    IEnumerator MoveWithEase()
    {
        // Perform linear interpolation of positions to move
        for (float s = 0.0f; s < 1.0f; s += 0.1f)
        {
            this.transform.position = Vector3.Lerp(initial_pos, final_pos, Ease(s));
            yield return new WaitForSeconds(Time.deltaTime);
        }
        this.transform.position = final_pos;
        yield return null;
    }
    */

    GameObject findClosestBuldozer()
    {
       GameObject result = null;
        float dist = 9000f;

        foreach (GameObject temp in GameManager.instance.Leaders)
        {
            if (dist >= DistanceTo(temp))
            {
                result = temp;
                dist = DistanceTo(temp);
            }

        }
        return result;
    }

    // Update is called once per frame
    void Update () {

        int offset=0;
        NavMeshAgent agent;
        Vector3 distance;
        if (GetComponent<NavMeshAgent>() != null)
        {
            
            agent = GetComponent<NavMeshAgent>();
            //Debug.Log("end of the path is: " + agent.pathEndPosition);
            //agent.destination = target;
        }
        else
        {


            if (follow)
            {
                if (target == null)//leader has died
                {
                    leader = findClosestBuldozer();
                }
                if (leader != null)
                {
                    target = leader.transform.position;
                    target -= leader.transform.forward.normalized;
                    target.y = 0;//sterilize, prevent moving on highground
                    offset = 1;
                    distance = target - transform.position;
                    if (!needsToMove && Vector3.Magnitude(distance) > followDistance)
                    {
                        needsToMove = true;
                    }
                }
            }
            distance = target - transform.position;

            Vector3 steering;
            Vector3 temp;
            if (needsToMove)
            {


                if (Vector3.Magnitude(distance) - offset > arrivalThreshold)
                {
                    steering = Vector3.Normalize(distance) * maxSpeed - rb.velocity;
                    //while (Vector3.Magnitude(steering) < 1 * Vector3.Magnitude(rb.velocity))
                    //{
                    //    steering *= 1;
                    //}
                    //Debug.Log(rb.velocity);
                }
                else
                {
                    steering = Vector3.Normalize(distance) * maxSpeed * (Vector3.Magnitude(distance) / arrivalThreshold) - rb.velocity;
                    //Debug.Log("arriving now");
                }

                if (follow && leader!=null)
                {
                    steering += computeSeparation(gameObject);
                }
                steering.y = 0;//sterilize
                rb.AddForce(steering);
                if (Vector3.Magnitude(rb.velocity) > 1)
                {
                    temp = Vector3.Normalize(rb.velocity);
                    temp.y = 0; //sterilize
                    gameObject.transform.forward = temp;
                }
                if (Vector3.Magnitude(steering) - offset == 0)
                {
                    needsToMove = false;
                }
                //Debug.Log("force added: " + steering + " " + Vector3.Normalize(distance));
                //Debug.Log("headed from: " + gameObject.transform.position + " headed to: " + target);
            }
        }
	}

    public void MoveTo(Vector3 t)
    {
       
        target = t;
        target.y = 0;//sterilize, prevent moving on highground
       // Debug.Log("moving to: " + t);
        needsToMove = true;

      
    }

    public void MoveTo(GameObject t)
    {
        if(leader!= null)
        {
            leader.GetComponent<LeaderComponent>().followers.Remove(gameObject);
        }
        leader = t;
        leader.GetComponent<LeaderComponent>().followers.Add(gameObject);
        follow = true;
        target = t.transform.position;
        target.y = 0;//sterilize, prevent moving on highground
        needsToMove = true;

    }

    public void ClearLeader()
    {
        leader.GetComponent<LeaderComponent>().followers.Remove(gameObject);
        leader = null;
        follow = false;
    }

    public void MoveToSelection(Vector3 t)
    {
        // Set start and end positions for the linearly interpolated motion
        initial_pos = this.transform.position;
        final_pos = t;
        //Debug.Log("moving");
        // Move saucer with ease-in/ease-out function
        StartCoroutine("MoveWithEase");
    }
    Vector3 computeSeparation(GameObject agent)
    {
        int max_separation = 10;
        Vector3 v = agent.GetComponent<Rigidbody>().velocity;
        List<GameObject> agents = leader.GetComponent<LeaderComponent>().followers;
        foreach(GameObject obj in agents)
        {
            if (agent != obj)
            {
                if (Vector3.Distance(agent.transform.position, obj.transform.position) <= max_separation)
                {
                    v.x += obj.transform.position.x - agent.transform.position.x;
                    v.z += obj.transform.position.z - agent.transform.position.z;

                    //neighborCount++;
                }

            }

        }
        if (agents.Count == 1)
        {
            v.Normalize();
            v.Scale(new Vector3(max_separation, max_separation, max_separation));
            return v;
        }


        v.x /= (agents.Count - 1);
        v.z /= (agents.Count -1);
        v = new Vector3(v.x - agent.transform.position.x, v.z - agent.transform.position.z);
        v.Normalize();
        v.x *= -max_separation;
        v.z *= -max_separation;
        return v;
    }
    public float DistanceTo(GameObject target)
    {
        return (Vector3.Distance(target.transform.position, gameObject.transform.position));
    }
}
