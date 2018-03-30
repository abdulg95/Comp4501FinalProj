using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityComponent : MonoBehaviour {


    public int cooldown;
    public int range;
    private int timer;
    public bool targetIsPosition = true;
    public Animation anim;
    public string abilityName;
    public GameObject aBuilding; // do this better
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (timer > 0)
        {
            timer--;
        }
        if (Input.GetKeyDown("space"))
        {
            Vector3 target = gameObject.transform.position;
            target += 30 * gameObject.transform.forward;
            UseOn(target);
        }
    }

    public bool UseOn(GameObject target)
    {

        if (targetIsPosition)
        {
            return UseOn(target.transform.gameObject.transform.position);
        }
        else
        {
            return false;
        }

        
    }

    public GameObject setSpawnTarget()
    {
        return GameObject.FindWithTag("ape");
    }

    public GameObject setBuildTarget()
    {
        return GameObject.FindWithTag("wall");
    }

 

    public bool UseOn(Vector3 target)
    {

        switch (abilityName)
        {
            case "build":
                aBuilding = setBuildTarget();
                Debug.Log("target is: " + target);
                aBuilding.GetComponent<HealthComponent>().Spawn(target);
                break;
            case "enterTreestand":
                break;
            case "releaseMonkey":
                break;
            case "barrage":
                break;
            case "slam":
                break;
            case "spawn":
                aBuilding = setSpawnTarget();
                aBuilding.GetComponent<HealthComponent>().Spawn(target);
                break;
            case "explode":
                break;
            case "demolish":
                break;
            case "frenzy":
                break;
            case "emptyPit":
                break;
            case "gameOver":
                break;
            case "testing":
                GetComponent<MovementComponent>().MoveTo(target);
                Debug.Log("atempting to move " + gameObject);
                break;

        }
        //need a different method for each ability, or each name. 

        return false;
    }





    public bool IsReady()
    {
        return (timer == 0);
    }
}
