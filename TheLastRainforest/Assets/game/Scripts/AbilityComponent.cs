using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityComponent : MonoBehaviour {


    public int cooldown;
    public int range;
    private int timer;
    public bool targetIsPosition;
    public Animation anim;
    public string name;
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0)
        {
            timer--;
        }
	}

    public bool UseOn(Vector3 target)
    {
        //need a different method for each ability, or each name. 

        return false;
    }

    public bool IsReady()
    {
        return (timer == 0);
    }
}
