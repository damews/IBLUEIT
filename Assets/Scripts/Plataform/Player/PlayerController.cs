﻿using UnityEngine;

/// <summary>
/// This script is set to the player
/// </summary>
public class PlayerController : MonoBehaviour
{
    private Transform _transform;
    private SerialListener _serialMessager;

    [Header("Settings")]
    public float Sensitivity;
    public ControlBehaviour Behaviour;

    private void Start()
    {
        _transform = this.GetComponent<Transform>();
        _serialMessager = GetComponent<SerialListener>();
    }

    private void Update()
    {
        #region Running Toggles

        ToggleControlBehaviour();

        #endregion

        SetPlayerPosition();
    }


    private void SetPlayerPosition()
    {
        var message = _serialMessager.MessageReceived;
        if (message.Length < 1) return;

        message = message.Replace('.', ',');

        float newYPos;

        try { newYPos = float.Parse(message); }
        catch { return; }

        newYPos -= 440f; //ToDo - Automatic Offset

        PitacoRecorder.Instance.Add(newYPos);

        newYPos *= (Sensitivity / 10f);

        if (Behaviour == ControlBehaviour.Absolute)
        {
            _transform.position = Vector3.Lerp(_transform.position,
                new Vector3(_transform.position.x, newYPos, _transform.position.z), Time.deltaTime * 10f);
        }
        else
        {
            if (newYPos > 3f || newYPos < -3f)
            {
                //ToDo - relative control
                _transform.position += new Vector3(0f, newYPos * 0.1f, 0f);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogWarningFormat("{0} has been hit!", collision.gameObject.name);
        Destroy(collision.gameObject);
    }

    private void OnEnable()
    {
        PitacoRecorder.Instance.Start();
    }

    private void OnDisable()
    {
        PitacoRecorder.Instance.Stop();

        if (GameManager.Instance?.Player != null)
        {
            PitacoRecorder.Instance.WriteData(GameManager.Instance.Player,
            GameConstants.GetSessionsPath(GameManager.Instance.Player), true);
        }
        else
        {
            PitacoRecorder.Instance.WriteData();
        }
    }

    #region Toggles

    private void ToggleControlBehaviour()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Behaviour == ControlBehaviour.Absolute)
            {
                Behaviour = ControlBehaviour.Relative;
            }
            else if (Behaviour == ControlBehaviour.Relative)
            {
                Behaviour = ControlBehaviour.Absolute;
            }

            Debug.LogFormat("Behaviour: {0}", Behaviour);
        }
    }

    #endregion

}
