using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	//declare and initialize gamemanager instance to null
	public static GameManager instance = null;

	//define startup values here e.g hp

	void Awake(){
		//enforce singleton we only need one gamemanager
		if (instance == null) {
			instance = this;		
		} else {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}
