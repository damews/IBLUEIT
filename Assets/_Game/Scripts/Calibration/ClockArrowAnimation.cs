﻿using Ibit.Core.Data;
using Ibit.Core.Serial;
using Ibit.Core.Util;
using UnityEngine;

namespace Ibit.Calibration
{
    public class ClockArrowAnimation : MonoBehaviour
    {
        public bool SpinClock { get; set; }

        private void Awake()
        {
            FindObjectOfType<SerialController>().OnSerialMessageReceived += OnSerialMessageReceived;
        }

        private void OnDestroy()
        {
            FindObjectOfType<SerialController>().OnSerialMessageReceived -= OnSerialMessageReceived;
        }

        private void OnSerialMessageReceived(string msg)
        {
            if (!SpinClock)
                return;

            if (msg.Length < 1)
                return;

            var snsrVal = Parsers.Float(msg);

            snsrVal = snsrVal < -Pacient.Loaded.PitacoThreshold || snsrVal > Pacient.Loaded.PitacoThreshold ? snsrVal : 0f;

            this.transform.Rotate(Vector3.back, snsrVal);
        }
    }
}