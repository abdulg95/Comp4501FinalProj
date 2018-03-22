using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour {

    public float maxSpeed;
    public float currentSpeed;
    public Vector3 target ;
    public int arrivalThreshold = 2;
    Rigidbody rb;
    bool needsToMove = false;
    Vector3 empty = new Vector3(0, 0, 0);


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        target = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if(needsToMove)
        {
            Vector3 distance = target - transform.position;
            Vector3 steering;

            if (Vector3.Magnitude(distance) > arrivalThreshold)
            {
                steering = Vector3.Normalize(distance) * maxSpeed - rb.velocity;
                Debug.Log(rb.velocity);
            }
            else
            {
                steering = Vector3.Normalize(distance) * maxSpeed * (Vector3.Magnitude(distance) / arrivalThreshold) - rb.velocity;
                Debug.Log("arriving now");
            }
            rb.AddRelativeForce(steering);
            if(steering == empty)
            {
                needsToMove = false;
            }
            Debug.Log("force added: " + steering + " " + Vector3.Normalize(distance));
        }
		
	}

    public void MoveTo(Vector3 t)
    {
       
        target = t;
        needsToMove = true;

      
    }
}
