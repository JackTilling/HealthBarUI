using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {

    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private int currentHealth;

    [SerializeField]
    private Material healthMat;

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
		if (Input.GetKeyDown(KeyCode.Alpha1) & currentHealth > 0) {
            ChangeHealth(-5, true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) & currentHealth > 0) {
            ChangeHealth(-20, true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) & currentHealth > 0) {
            ChangeHealth(-30, true);
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            int toMaxHealth = maxHealth - currentHealth;
            ChangeHealth(toMaxHealth, false);
        }
    }

    private void ChangeHealth(int hpChange, bool shake) {
        currentHealth += hpChange;
        if (currentHealth <= 20) {
            healthMat.SetFloat("Vector1_D8FB485D", 1f);
        }
        if (currentHealth < 0) {
            currentHealth = 0;
        } 
        else {
            healthMat.SetFloat("Vector1_D8FB485D", 0f);
        }
        

        // Must convert to float in order to get result out of 1 (percentage for fill amount)
        float currentHealthPer = (float)currentHealth / (float)maxHealth;
        
        healthBar.HandleHealthChanged(currentHealthPer, hpChange, shake);
    }
}
