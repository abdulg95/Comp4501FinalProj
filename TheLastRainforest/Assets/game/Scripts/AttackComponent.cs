using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour {

    public int damage;
    public int range;
    public int cooldown = 50;
    public GameObject projectile;
    public bool isEnemy = false;
    
    public GameObject target = null;
    private List<GameObject> targetsInRange;
    private int timer;
    private int needsProjectileAt;
    private GameObject temp;

	// Use this for initialization
	void Start () {
        needsProjectileAt = 7;
        targetsInRange = new List<GameObject>();
        timer = cooldown;

    }
	
	// Update is called once per frame
	void Update () {
        targetsInRange.RemoveAll(item => item == null);
        if(timer>=2*cooldown)
        {
            timer = cooldown;
        }
        if(target == null)
        {
            if(targetsInRange.Count > 0)
            {
                
                Attack(targetsInRange[0]);
                //GetComponent<MovementComponent>().MoveTo(gameObject.transform.position);
            }
        }
        else if(target != null)
        {
            Attack(target);
            ///GetComponent<MovementComponent>().MoveTo(gameObject.transform.position);

        }

    }

    public void Attack(GameObject t)
    {
        if (t != null)
        {


            target = t;
            //Debug.Log("range: " + range + " distance: " + DistanceTo(target));
            if (range <= DistanceTo(target) && (t.CompareTag("enemy") ^ isEnemy))
            {
                gameObject.transform.LookAt(target.transform.position);
                if (timer >= cooldown)
                {

                    if (range >= needsProjectileAt)
                    {
                        //code to make the projectile here
                        temp = Instantiate(projectile);
                        Vector3 vec = gameObject.transform.position;
                        vec.y += 1;
                        temp.transform.position = vec;
                        temp.GetComponent<AttackComponent>().Attack(target);

                    }
                    else
                    {
                        if (target != null)
                        {


                            target.GetComponent<HealthComponent>().TakeDamage(damage);
                            if (gameObject.CompareTag("projectile"))
                            {
                                Destroy(gameObject);
                            }
                            if (target == null)
                            {
                                targetsInRange.RemoveAll(item => item == null);
                                if (targetsInRange.Count > 0)
                                {
                                    target = targetsInRange[0];
                                }
                            }
                        }

                    }
                    timer = 0;
                }
            }
            else
            {
                GetComponent<MovementComponent>().MoveTo(target.transform.position);
            }
        }
    }

    public float DistanceTo(GameObject target)
    {
        return (Vector3.Distance(target.transform.position, gameObject.transform.position));
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Terrain")
        {

        }
        else
        {

            //Debug.Log("in range of: " + other);
            if (isEnemy)
            {
                if (other.gameObject.CompareTag("enemy"))
                {

                }
                else
                {
                    targetsInRange.Add(other.gameObject);
                    Debug.Log("target added");
                }
            }
            else
            {
                if (other.gameObject.CompareTag("enemy"))
                {
                    targetsInRange.Add(other.gameObject);
                    Debug.Log("target added");
                }
                else
                {

                }
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Terrain")
        {

        }
        else
        {
            //Debug.Log("left the range of: " + other);
            if (isEnemy)
            {
                if (other.gameObject.CompareTag("enemy"))
                {

                }
                else
                {
                    targetsInRange.Remove(other.gameObject);
                }
            }
            else
            {
                if (other.gameObject.CompareTag("enemy"))
                {
                    targetsInRange.Remove(other.gameObject);
                }
                else
                {

                }
            }
        }
    }
}
