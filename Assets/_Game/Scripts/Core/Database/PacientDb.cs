using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Ibit.Core.Data;
using Ibit.Core.Util;

namespace Ibit.Core.Database
{
    public class PacientDb
    {
        public static PacientDb Instance = new PacientDb();

        private readonly string filePath = @"savedata/pacients/_pacientList.csv";

        private PacientDb()
        {
            Instance = this;
            PacientList = new List<Pacient>();
            Load();
        }

        public List<Pacient> PacientList { get; }

        public List<Pacient> ContainsName(string find) => PacientList.FindAll(x => x.Name.Contains(find));

        public void CreatePacient(Pacient plr)
        {
            PacientList.Add(plr);
            Save();
        }

        public Pacient GetAt(int i) => PacientList.Count <= i ? null : PacientList[i];

        public Pacient GetPacient(int id) => PacientList.Find(x => x.Id == id);

        public Pacient GetPacient(string pacientName) => PacientList.Find(x => x.Name == pacientName);

        public void Load()
        {
            if (!File.Exists(filePath))
                return;

            PacientList.Clear();

            var csvData = FileManager.ReadCsv(filePath);
            var grid = CsvParser2.Parse(csvData);

            for (var i = 1; i < grid.Length; i++)
            {
                if (string.IsNullOrEmpty(grid[i][0]))
                    continue;

                var plr = new Pacient
                {
                    Id = int.Parse(grid[i][0]),
                    Name = grid[i][1],
                    Birthday = DateTime.ParseExact(grid[i][2], @"dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Observations = grid[i][3],
                    Condition = (ConditionType)Enum.Parse(typeof(ConditionType), grid[i][4]),
                    Capacities = new Capacities
                    {
                    InsPeakFlow = Parsers.Float(grid[i][5]),
                    ExpPeakFlow = Parsers.Float(grid[i][6]),
                    InsFlowDuration = Parsers.Float(grid[i][7]),
                    ExpFlowDuration = Parsers.Float(grid[i][8]),
                    RespiratoryRate = Parsers.Float(grid[i][9]),
                    },
                    UnlockedLevels = int.Parse(grid[i][10]),
                    AccumulatedScore = Parsers.Float(grid[i][11]),
                    PlaySessionsDone = int.Parse(grid[i][12]),
                    CalibrationDone = bool.Parse(grid[i][13]),
                    HowToPlayDone = bool.Parse(grid[i][14]),
                    Ethnicity = grid[i][15],
                    Height = Parsers.Float(grid[i][16]),
                    Weight = Parsers.Float(grid[i][17]),
                    PitacoThreshold = Parsers.Float(grid[i][18]),
                    Sex = (Sex)Enum.Parse(typeof(Sex), grid[i][19]),
                    CreatedOn = DateTime.Parse(grid[i][20])
                };

                PacientList.Add(plr);
            }
        }

        public void Save()
        {
            var items = new []
            {
                "Id",
                "Name",
                "Birthday",
                "Observations",
                "Condition",
                "InsPeakFlow",
                "ExpPeakFlow",
                "InsFlowDuration",
                "ExpFlowDuration",
                "RespiratoryRate",
                "UnlockedLevels",
                "AccumulatedScore",
                "PlaySessionsDone",
                "CalibrationDone",
                "HowToPlayDone",
                "Ethnicity",
                "Height",
                "Weight",
                "PitacoThreshold",
                "Sex",
                "CreatedOn"
            };

            var sb = new StringBuilder();
            sb.AppendLine(items.Aggregate((a, b) => a + ";" + b));

            for (var i = 0; i < PacientList.Count; i++)
            {
                var pacient = GetAt(i);

                sb.AppendLine(
                    $"{pacient.Id};{pacient.Name};{pacient.Birthday:dd/MM/yyyy};{pacient.Observations};{pacient.Condition};" +
                    $"{pacient.Capacities.RawInsPeakFlow};{pacient.Capacities.RawExpPeakFlow};{pacient.Capacities.RawInsFlowDuration};{pacient.Capacities.RawExpFlowDuration};" +
                    $"{pacient.Capacities.RawRespRate};{pacient.UnlockedLevels};{pacient.AccumulatedScore};{pacient.PlaySessionsDone};{pacient.CalibrationDone};{pacient.HowToPlayDone};" +
                    $"{pacient.Ethnicity};{pacient.Height};{pacient.Weight};{pacient.PitacoThreshold};{pacient.Sex};{pacient.CreatedOn};"
                );
            }

            FileManager.WriteAllText(filePath, sb.ToString());
        }
    }
}