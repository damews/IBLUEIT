﻿using System;
using System.IO;
using System.Text;
using Ibit.Core.Data;
using Ibit.Core.Util;
using UnityEngine;

namespace Ibit.Core
{
    public class MinigameLogger : MonoBehaviour
    {
        [SerializeField] private string _filename;
        private StringBuilder _sb;
        private DateTime _dt;
        private string _path;
        private int _dataWritten;

        private void Awake ()
        {
            _dt = DateTime.Now;
            _sb = new StringBuilder ();

            _path = @"savedata/pacients/" + Pacient.Loaded.Id + @"/" + $"_{_filename}History.csv";

            if (!File.Exists (_path))
                _sb.AppendLine ("dateTime;value");

            Debug.Log ("MinigameLogger loaded.");
        }

        public void Write (float value)
        {
            _dataWritten++;
            _sb.AppendLine ($"{_dt};{value}");
        }

        public void Save ()
        {
            if(_dataWritten < 1)
                return;

            if (!File.Exists (_path))
                FileManager.WriteAllText (_path, _sb.ToString ());
            else
                FileManager.AppendAllText (_path, _sb.ToString ());
        }
    }
}