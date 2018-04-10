using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarPosition : MonoBehaviour {
    public float disty, distz;
    public Transform Owner;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles = Camera.main.transform.eulerAngles;
        if(Owner != null)
        {
            Vector3 pos = new Vector3(Owner.position.x, Owner.position.y + disty, Owner.position.z + distz);
            transform.position = pos;
        }

    }
}
