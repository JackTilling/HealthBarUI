using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {

    [SerializeField]
    private Image foregroundImg;

    [SerializeField]
    private Text healthText;

    [SerializeField]
    private float updateSpeedSeconds = 0.5f;

    [SerializeField]
    private Transform canvasTransform;

    // How long the object should shake for.
    private float shakeDuration = 0f;
    private float shakeAmount = 0.1f;
    private float decreaseFactor = 1.0f;
    private Vector3 originalPos;

    // Gets the original camera postion
    void OnEnable() {
        originalPos = canvasTransform.localPosition;
    }

    public void HandleHealthChanged(float per, int dmg) {
        StartCoroutine(ChangeToPer(per));
        float dmgF = (float)dmg;
        shakeAmount = (dmgF / 100)*-1.0f;
        //float randomAmount = UnityEngine.Random.Range(0.05f, 0.15f);
        //shakeAmount = randomAmount;
        shakeDuration = +0.1f;
    }

    private IEnumerator ChangeToPer(float per) {
        float preChangePer = foregroundImg.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds) {
            elapsed += Time.deltaTime;
            foregroundImg.fillAmount = Mathf.Lerp(preChangePer, per, elapsed / updateSpeedSeconds);
            healthText.text = Mathf.Round(foregroundImg.fillAmount*100).ToString();
            yield return null;
        }

        healthText.text = (per*100).ToString();
        foregroundImg.fillAmount = per;
    }

    // Tansforms to look at the camera and flips UI elements so they are correct direction
    private void LateUpdate() {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }

    // Shakes the camera over a given time period
    void Update() {
        if (shakeDuration > 0) {
            Debug.Log(shakeAmount);
            canvasTransform.localPosition = originalPos + UnityEngine.Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        } else { // Returns it to original position
            shakeDuration = 0f;
            canvasTransform.localPosition = originalPos;
        }
    }
}
