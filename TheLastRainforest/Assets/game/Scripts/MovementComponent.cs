using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour {

    public float maxSpeed;
    public float currentSpeed;
    public Vector3 target;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MoveTo(Vector3 t)
    {
        target = t;
    }
}
