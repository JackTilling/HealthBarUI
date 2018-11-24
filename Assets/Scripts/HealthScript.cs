using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {

    // Creates key variables for the enemies health system
    // SerializeField enables us to view and assign these in the inspector

    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private int currentHealth;

    [SerializeField]
    private Material healthMat;

    [SerializeField]
    private GameObject healthObject;

    [SerializeField]
    private HealthBarScript healthBar;


    // Brings the current hp up to the max hp
    // Grabs the object and component for the canvas and health bar script
    private void OnEnable() {
        currentHealth = maxHealth;
        healthObject = GameObject.Find("WorldSpaceCanvas");
        healthBar = healthObject.GetComponent<HealthBarScript>();
    }
	
	// Manage's all the damage inputs specifically for this tutorial
    // Shows how ChangeHealth would be called in a game scenario
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
        if (Input.GetKeyDown(KeyCode.R)) { // Heals the enemy to full health but ensures no bar shake
            int toMaxHealth = maxHealth - currentHealth;
            ChangeHealth(toMaxHealth, false);
        }
    }

    // Main tool used to change the enemies health and call various functions to do with health bar and shader functionality
    private void ChangeHealth(int hpChange, bool shakeActivate) {

        // Initially sets the health to it's new value
        currentHealth += hpChange;

        // This 'less than' value is what determines the value of the shader pulse
        if (currentHealth <= 20 & currentHealth > 0) {
            healthMat.SetFloat("Vector1_D8FB485D", 1f); // This vector refers to a variable within the enemy shader
        }
        else if (currentHealth <= 0) {
            healthMat.SetFloat("Vector1_D8FB485D", 1f);
            currentHealth = 0;
        } 
        else {
            healthMat.SetFloat("Vector1_D8FB485D", 0f); // Deactivates the red pulsing shader property
        }
        
        // Must convert to float in order to get result out of 1 (percentage for fill amount)
        float currentHealthPer = (float)currentHealth / (float)maxHealth;
        
        // Calls the health bar script to control visual functionality using a collection of values
        healthBar.HandleHealthChanged(currentHealthPer, hpChange, shakeActivate);
    }
}
