﻿using UnityEngine;
using UnityEngine.UI;

public class UIButtonOnCalibration : MonoBehaviour
{
    private Button button;

    private void Awake() => button = GetComponent<Button>();

    private void FixedUpdate() => button.interactable = PlayerData.Player != null && PlayerData.Player.CalibrationDone;
}