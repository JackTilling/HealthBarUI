﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {

    [SerializeField]
    private Image foregroundImg;

    [SerializeField]
    private float updateSpeedSeconds = 0.5f;
	
    public void HandleHealthChanged(float per) {
        StartCoroutine(ChangeToPer(per));
    }

    private IEnumerator ChangeToPer(float per) {
        float preChangePer = foregroundImg.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds) {
            elapsed += Time.deltaTime;
            foregroundImg.fillAmount = Mathf.Lerp(preChangePer, per, elapsed / updateSpeedSeconds);
            yield return null;
        }

        foregroundImg.fillAmount = per;
    }

    // Tansforms to look at the camera and flips UI elements so they are correct direction
    private void LateUpdate() {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }

}
