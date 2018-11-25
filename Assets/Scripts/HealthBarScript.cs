using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {

    // Creates key variables for the enemies health system
    // SerializeField enables us to view and assign these in the inspector

    [SerializeField]
    private Image foregroundImg;

    [SerializeField]
    private Text healthText;

    [SerializeField]
    private float updateSpeedSeconds = 0.5f;

    [SerializeField]
    private Transform canvasTransform;

    // These are the lerp colour values which make up the health bar

    [SerializeField]
    private Color fullColour;

    [SerializeField]
    private Color emptyColour;

    // These control the default values of the health bar shake
    private float shakeDuration = 0f;
    private float shakeAmount = 0.1f;
    private float decreaseFactor = 1.0f;
    private Vector3 originalPos;

    // Gets the original health bar postion so it knows what location to return to
    void OnEnable() {
        originalPos = canvasTransform.localPosition;
    }

    // Tansforms to look at the camera and flips UI elements so they are correct direction
    private void LateUpdate() {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }

    // Controls how much the health bar changes by, how much damage has been dealt and whether to shake or not
    // Usualy called by the script which controls this enemies health values
    public void HandleHealthChanged(float per, int dmg, bool shake) {
        StartCoroutine(ChangeToPercentage(per)); // Enumerator used to move the values overtime
        float dmgF = (float)dmg;

        // Small calculation to decide how much the canvas shakes
        if (shake) {
            shakeAmount = (dmgF / 120) * -1.0f;
        } else {
            shakeAmount = 0;
        }

        // Adds the duration time of the shake
        shakeDuration = +0.1f;
    }

    // Controls which values change overtime and by how much
    // Used in conjunction with Lerp functionality to control colour and fill amount
    private IEnumerator ChangeToPercentage(float per) {
        float preChangePer = foregroundImg.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds) {
            elapsed += Time.deltaTime;  // Delta time calculates how long it took for the last frame to complete executing
            foregroundImg.fillAmount = Mathf.Lerp(preChangePer, per, elapsed / updateSpeedSeconds);
            foregroundImg.color = Color.Lerp(emptyColour, fullColour, per);
            healthText.text = Mathf.Round(foregroundImg.fillAmount*100).ToString();
            yield return null;
        }

        // Updates the current values to the newly calculated ones
        healthText.text = (per*100).ToString();
        foregroundImg.fillAmount = per;
    }

    // Shakes the health bar over a period of time given the shake amount and duration necessary
    void Update() {
        if (shakeDuration > 0) {

            // Uses unity random library to return a point within a small sphere which the bar will move to in quick succession 
            canvasTransform.localPosition = originalPos + UnityEngine.Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        } else { // Returns it to original position
            shakeDuration = 0f;
            canvasTransform.localPosition = originalPos;
        }
    }
}
