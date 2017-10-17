﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

public class PitacoRecorder
{
    public static readonly PitacoRecorder Instance = new PitacoRecorder();

    private readonly StringBuilder _sb;
    private readonly Stopwatch _stopwatch;
    private readonly Dictionary<long, float> _incomingDataDictionary;
    private DateTime _recordStart, _recordFinish;

    public PitacoRecorder()
    {
        _incomingDataDictionary = new Dictionary<long, float>();
        _sb = new StringBuilder();
        _stopwatch = new Stopwatch();
    }

    public void Start()
    {
        _recordStart = DateTime.Now;
        _stopwatch.Start();
    }

    public void Stop()
    {
        _recordFinish = DateTime.Now;
        _stopwatch.Stop();
    }

    public void Add(float value)
    {
        _incomingDataDictionary.Add(_stopwatch.ElapsedMilliseconds, value);
    }

    public void WriteData(string path = null, bool clearRecords = false)
    {
        if (_incomingDataDictionary.Count == 0) return;

        UnityEngine.Debug.Log($"Writing {_incomingDataDictionary.Count} values from incoming data dictionary...");

        foreach (var pair in _incomingDataDictionary)
        {
            _sb.AppendLine($"{pair.Key};{pair.Value}");
        }

        string filepath;

        if (string.IsNullOrEmpty(path))
        {
            filepath = GameConstants.SaveDataPath + $"test_{_recordStart:yyyyMMdd_hhmmss}.csv";
        }
        else
        {
            filepath = path + _recordStart.ToString("yyyyMMdd_hhmmss") + ".csv";
        }

        GameUtilities.WriteAllText(filepath, _sb.ToString());

        if (clearRecords)
        {
            ClearRecords();
        }
    }

    public void WriteData(Player plr, Stage stg, bool clearRecords = false)
    {
        var configString = new[]
        {
            "PlayerID", "PlayerName", "PlayerDisfunction", "SessionStart", "SessionFinish", "StageId"
        };

        _sb.AppendLine(configString.Aggregate((a, b) => a + ";" + b));

        _sb.AppendLine($"{plr.Id};{plr.Name};{plr.Disfunction};{_recordStart};{_recordFinish};{stg.Id};");
        _sb.AppendLine();

        var path = GameConstants.GetSessionsPath(plr);

        WriteData(path, clearRecords);
    }

    public void ClearRecords()
    {
        _sb.Clear();
        _incomingDataDictionary.Clear();
        _stopwatch.Reset();
    }

    public void LoadData()
    {
        //ToDo
    }
}
