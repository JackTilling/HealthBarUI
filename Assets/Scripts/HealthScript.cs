using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {

    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private int currentHealth;

    // Just so we can see these in the inspector for tutorial purposes
    // Doesn't need to be visable
    [SerializeField]
    private GameObject healthObject;

    [SerializeField]
    private HealthBarScript healthBar;

    private void OnEnable() {
        currentHealth = maxHealth;
        healthObject = GameObject.Find("WorldSpaceCanvas");
        healthBar = healthObject.GetComponent<HealthBarScript>();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space) & currentHealth> 0) {
            ChangeHealth(-10);
        }
	}

    private void ChangeHealth(int hpChange) {
        currentHealth += hpChange;

        // Must convert to float in order to get result out of 1 (percentage for fill amount)
        float currentHealthPer = (float)currentHealth / (float)maxHealth;
        
        healthBar.HandleHealthChanged(currentHealthPer);
    }
}
