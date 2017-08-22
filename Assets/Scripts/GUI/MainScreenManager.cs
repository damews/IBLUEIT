﻿using System;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class MainScreenManager : MonoBehaviour
{
    public GameObject PanelMessage, Panel1, Panel2, PanelNewGame, PanelLoadGame;
    public LevelLoader LevelLoader;
    public GameObject[] ObjectsToHide;

    private void HideObjects()
    {
        foreach (var obj in ObjectsToHide)
        {
            obj.SetActive(false);
        }
    }

    private void ShowObjects()
    {
        foreach (var obj in ObjectsToHide)
        {
            obj.SetActive(true);
        }
    }

    public void ButtonStartGame()
    {
        Panel1.SetActive(false);
        Panel2.SetActive(true);
    }

    public void ButtonNewGame()
    {
        Panel2.SetActive(false);
        PanelNewGame.SetActive(true);
        HideObjects();
    }

    public void ButtonLoadGame()
    {
        Panel2.SetActive(false);
        PanelLoadGame.SetActive(true);
        HideObjects();
    }

    public void ButtonInfoGame()
    {
        throw new Exception("Not yet implemented.");
    }

    public void ButtonQuitGame()
    {
        Application.Quit();
    }

    public void ReturnToNewLoadPanel()
    {
        PanelNewGame.SetActive(false);
        PanelLoadGame.SetActive(false);
        Panel2.SetActive(true);
        ShowObjects();
    }

    public void StarNewGame()
    {
        var bDay = GameObject.Find("LabelBDay").GetComponent<Text>().text;
        var bMonth = GameObject.Find("LabelBMonth").GetComponent<Text>().text;
        var bYear = GameObject.Find("LabelBYear").GetComponent<Text>().text;

        DateTime birthday;
        try
        {
            birthday = new DateTime(int.Parse(bYear), int.Parse(bMonth), int.Parse(bDay));
        }
        catch (ArgumentOutOfRangeException)
        {
            var errMsg = LocalizationManager.Instance.GetLocalizedValue("error_invalidDate");
            PanelMessage.SendMessage("ShowError", errMsg);
            return;
        }

        var playerName = GameObject.Find("InputFieldName").GetComponent<InputField>().text;

        var normal = GameObject.Find("ToggleNormal").GetComponent<Toggle>().isOn;
        var obstructive = GameObject.Find("ToggleObstructive").GetComponent<Toggle>().isOn;
        var restrictive = GameObject.Find("ToggleRestrictive").GetComponent<Toggle>().isOn;

        var disfunction = normal
            ? Disfunctions.Normal
            : (obstructive ? Disfunctions.Obstructive : Disfunctions.Restrictive);

        var observations = GameObject.Find("Observations").GetComponent<InputField>().text;

        var account = new Account
        {
            Name = playerName,
            Birthday = birthday,
            Disfunction = disfunction,
            Id = (uint)DatabaseManager.Instance.Accounts.AccountList.Count + 1,
            Observations = observations
        };

        var tmpAcc = DatabaseManager.Instance.Accounts.Find_Name(playerName);

        if(account.Equals(tmpAcc))
        {
            var errMsg = LocalizationManager.Instance.GetLocalizedValue("error_alreadyExists");
            PanelMessage.SendMessage("ShowError", errMsg);
            return;
        }

        DatabaseManager.Instance.Accounts.CreateAccount(account);

        Debug.Log($"Jogador {playerName} criado!");
    }
}
