using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderComponent : MonoBehaviour {


    public List<GameObject> followers = new List<GameObject>();
    // Use this for initialization

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        followers.RemoveAll(item => item == null);
	}
}
