using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour {


    public int maximumHealth;
    public int currentHealth;
    public int regenerationDelay; // the higher this is the less the unit will regen
    private int counter;

    public GameObject prefab;

	// Use this for initialization
	void Start () {
        counter = 0;
        //Spawn(new Vector3(320, 0, 130)); // do not do this in start, infinite loop out of RAM
		
	}
	
	// Update is called once per frame
	void Update () {
        if (IsDead())
        {
            Destroy(gameObject);
        }
        else
        {
            if(counter == regenerationDelay && currentHealth < maximumHealth)
            {
                currentHealth++;
                counter = 0;
            }
            else
            {
                counter++;
            }
        }
		
	}

    public void Spawn(Vector3 input)
    {
        GameObject newUnit = Instantiate(prefab);
        input.y = 0.1f;
        newUnit.transform.position = input;
       
    }

    public bool IsDead()
    {
        return (currentHealth <= 0);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
