﻿using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtilities
{
    public static void WriteAllText(string filepath, string contents)
    {
        var directory = Path.GetDirectoryName(filepath);

        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        File.WriteAllText(filepath, contents, Encoding.UTF8);

        Debug.LogFormat("File saved: {0}", filepath);
    }

    public static string ReadAllText(string filepath)
    {
        return File.ReadAllText(filepath, Encoding.UTF8);
    }

    public static float ParseSerialMessage(string msg) => float.Parse(msg.Replace('.', ','));

    public static float CalculateMeanFlow(List<KeyValuePair<long, float>> respirationSamples)
    {
        float cyclesPerSec = 0f;

        long startZero = 0, endZero = 0;
        long firstCurveTime = 0, secondCurveTime = 0;

        for (var i = 1; i < respirationSamples.Count; i++)
        {
            var actualTime = respirationSamples[i].Key;
            var lastTime = respirationSamples[i - 1].Key;

            var actualValue = respirationSamples[i].Value;
            var lastValue = respirationSamples[i - 1].Value;

            if (actualValue < -GameConstants.PitacoThreshold && actualValue > GameConstants.PitacoThreshold)
            {
                if (startZero == 0)
                {
                    startZero = lastTime;
                }
            }
            else
            {
                if (startZero != 0)
                {
                    endZero = actualTime;

                    if (firstCurveTime == 0)
                    {
                        firstCurveTime = startZero - endZero;
                        startZero = 0;
                        endZero = 0;
                    }

                    if (secondCurveTime == 0 && firstCurveTime != 0)
                    {
                        secondCurveTime = startZero - endZero;
                        startZero = 0;
                        endZero = 0;
                    }
                }
            }
        }

        return cyclesPerSec;
    }
}
