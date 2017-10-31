﻿using UnityEngine;

public class ClockArrowSpin : MonoBehaviour
{
    public bool SpinClock { get; set; }

    public SerialController serialController;

    private void OnEnable()
    {
        serialController.OnSerialMessageReceived += OnSerialMessageReceived;
    }

    private void OnDisable()
    {
        serialController.OnSerialMessageReceived -= OnSerialMessageReceived;
    }

    private void OnSerialMessageReceived(string msg)
    {
        if (!SpinClock) return;

        if (!SerialGetOffset.IsUsingOffset) return;

        if (msg.Length < 1) return;

        var snsrVal = GameUtilities.ParseFloat(msg) - SerialGetOffset.Offset;

        snsrVal = snsrVal < -GameConstants.PitacoThreshold * 0.5f || snsrVal > GameConstants.PitacoThreshold * 0.5f ? snsrVal : 0f;

        this.transform.Rotate(Vector3.back, snsrVal);
    }
}
