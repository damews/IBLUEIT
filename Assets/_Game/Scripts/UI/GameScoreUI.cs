﻿using UnityEngine;
using UnityEngine.UI;

public class GameScoreUI : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    private void Update() => scoreText.text = Mathf.Round(Scorer.Instance.Score).ToString();
}
