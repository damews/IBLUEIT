// This code automatically generated by TableCodeGen
using UnityEngine;
using System.Collections.Generic;

public class SummaryCSV
{
    public class Row
    {
        public string Id;
        public string Name;
        public string Birthday;
        public string Observations;
        public string Disfunction;
    }

    List<Row> rowList = new List<Row>();
    bool isLoaded = false;

    public bool IsLoaded()
    {
        return isLoaded;
    }

    public List<Row> GetRowList()
    {
        return rowList;
    }

    public void Load(TextAsset csv)
    {
        rowList.Clear();
        string[][] grid = CsvParser2.Parse(csv.text);
        for (int i = 1; i < grid.Length; i++)
        {
            Row row = new Row();
            row.Id = grid[i][0];
            row.Name = grid[i][1];
            row.Birthday = grid[i][2];
            row.Observations = grid[i][3];
            row.Disfunction = grid[i][4];

            rowList.Add(row);
        }
        isLoaded = true;
    }

    public int NumRows()
    {
        return rowList.Count;
    }

    public Row GetAt(int i)
    {
        if (rowList.Count <= i)
            return null;
        return rowList[i];
    }

    public Row Find_Id(string find)
    {
        return rowList.Find(x => x.Id == find);
    }
    public List<Row> FindAll_Id(string find)
    {
        return rowList.FindAll(x => x.Id == find);
    }
    public Row Find_Name(string find)
    {
        return rowList.Find(x => x.Name == find);
    }
    public List<Row> FindAll_Name(string find)
    {
        return rowList.FindAll(x => x.Name == find);
    }
    public Row Find_Birthday(string find)
    {
        return rowList.Find(x => x.Birthday == find);
    }
    public List<Row> FindAll_Birthday(string find)
    {
        return rowList.FindAll(x => x.Birthday == find);
    }
    public Row Find_Observations(string find)
    {
        return rowList.Find(x => x.Observations == find);
    }
    public List<Row> FindAll_Observations(string find)
    {
        return rowList.FindAll(x => x.Observations == find);
    }
    public Row Find_Disfunction(string find)
    {
        return rowList.Find(x => x.Disfunction == find);
    }
    public List<Row> FindAll_Disfunction(string find)
    {
        return rowList.FindAll(x => x.Disfunction == find);
    }

}