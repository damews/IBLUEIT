﻿using UnityEngine;

public partial class Player
{
    private void AnimationOnSerial(string msg)
    {
        if (msg.Length < 1)
            return;

        var sensorValue = Utils.ParseFloat(msg);

        sensorValue = sensorValue < -GameMaster.PitacoThreshold || sensorValue > GameMaster.PitacoThreshold * 0.6f ? sensorValue : 0f;
        Debug.Log(sensorValue);

        this.animator.Play(sensorValue < 0 ? "Dolphin-Jump" : "Dolphin-Move");
    }
}
