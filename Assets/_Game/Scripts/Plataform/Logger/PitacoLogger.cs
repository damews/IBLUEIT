﻿using UnityEngine;

public class PitacoLogger : Logger
{
    protected override void Awake()
    {
        base.Awake();
        sb.AppendLine("time;value");
        FindObjectOfType<SerialController>().OnSerialMessageReceived += OnSerialMessageReceived;
    }

    protected override void Flush()
    {
        if (sb.Length < 0)
            return;

        var path = @"savedata/pacients/" + Pacient.Loaded.Id + @"/" + $"{recordStart:yyyyMMdd-HHmmss}_" + FileName + ".csv";
        FileReader.WriteAllText(path, sb.ToString());
    }

    private void OnSerialMessageReceived(string msg)
    {
        if (!isRecording || msg.Length < 1)
            return;

        sb.AppendLine($"{Time.time:F};{Parsers.Float(msg):F}");
    }
}