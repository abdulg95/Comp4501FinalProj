using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour {

    public int damage;
    public int range;
    public GameObject target;
    private int needsProjectileAt;

	// Use this for initialization
	void Start () {
        needsProjectileAt = 5;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Attack(GameObject t)
    {
        target = t;
        if(range <= DistanceTo(target))
        {
            if (range >= needsProjectileAt)
            {
                //code to make the projectile here
            }
            else
            {
                target.GetComponent<HealthComponent>().TakeDamage(damage);

            }
        }
        else
        {
            GetComponent<MovementComponent>().MoveTo(target.transform.position); 
        }
        
    }

    public float DistanceTo(GameObject target)
    {
        return (Vector3.Distance(target.transform.position, gameObject.transform.position));
    }
}
