using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour {


    public int maximumHealth;
    public int currentHealth;
    public int regenerationDelay; // the higher this is the less the unit will regen
    private int counter;
	// Use this for initialization
	void Start () {
        counter = 0;
		
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

    public bool IsDead()
    {
        return (currentHealth <= 0);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
