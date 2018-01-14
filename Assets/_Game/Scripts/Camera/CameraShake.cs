﻿/* Original source: https://gist.github.com/ftvs/5822103 */

using NaughtyAttributes;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // How long the object should shake for.
    [SerializeField]
    private float shakeDuration;

    // Amplitude of the shake. A larger value shakes the camera harder.
    [SerializeField]
    private float shakeAmount = 0.7f;

    [SerializeField]
    private float decreaseFactor = 1.0f;

    private Vector3 originalPos;
    private bool shakeCam;
    private float originalDuration;

    private void Awake()
    {
        Player.Instance.OnEnemyHit += Shake;
        originalPos = this.transform.localPosition;
        originalDuration = shakeDuration;
    }

    private void Update()
    {
        if (!shakeCam)
            return;

        if (shakeDuration > 0)
        {
            this.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = originalDuration;
            this.transform.localPosition = originalPos;
            shakeCam = false;
        }
    }
    
    private void Shake(GameObject go)
    {
        if (go.tag.Contains("Obstacle"))
            shakeCam = true;
    }
}
