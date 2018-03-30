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
    Vector3 initial_pos;
    Vector3 final_pos;



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

    // Update is called once per frame
    void Update () {
        if(needsToMove)
        {
            Vector3 distance = target - transform.position;
            Vector3 steering;
            Vector3 temp;

            if (Vector3.Magnitude(distance) > arrivalThreshold)
            {
                steering = Vector3.Normalize(distance) * maxSpeed - rb.velocity;
                //while (Vector3.Magnitude(steering) < 1 * Vector3.Magnitude(rb.velocity))
                //{
                //    steering *= 1;
                //}
                Debug.Log(rb.velocity);
            }
            else
            {
                steering = Vector3.Normalize(distance) * maxSpeed * (Vector3.Magnitude(distance) / arrivalThreshold) - rb.velocity;
                Debug.Log("arriving now");
            }
            steering.y = 0;//sterilize
            rb.AddForce(steering);
            if (Vector3.Magnitude(rb.velocity) > 1)
            {
                temp = Vector3.Normalize(rb.velocity);
                temp.y = 0; //sterilize
                gameObject.transform.forward = temp;
            }
            if (steering == empty)
            {
                needsToMove = false;
            }
            Debug.Log("force added: " + steering + " " + Vector3.Normalize(distance));
            Debug.Log("headed from: " + gameObject.transform.position + " headed to: " + target);
        }
		
	}

    public void MoveTo(Vector3 t)
    {
       
        target = t;
        target.y = 0;//sterilize, prevent moving on highground
        Debug.Log("moving to: " + t);
        needsToMove = true;

      
    }

    public void MoveToSelection(Vector3 t)
    {
        // Set start and end positions for the linearly interpolated motion
        initial_pos = this.transform.position;
        final_pos = t;
        Debug.Log("moving");
        // Move saucer with ease-in/ease-out function
        StartCoroutine("MoveWithEase");
    }
}
