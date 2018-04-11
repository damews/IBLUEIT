﻿using Ibit.Core.Data;
using Ibit.Core.Database;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Ibit.MainMenu.UI
{
    public class FillStageList : MonoBehaviour
    {
        [SerializeField]
        private GameObject buttonPrefab;

        [SerializeField]
        private Scrollbar scrollbar;

        private bool populated;

        private void OnEnable()
        {
            if (populated)
            {
                var children = (from Transform child in transform select child.gameObject).ToList();
                children.ForEach(Destroy);
            }

            foreach (var stage in StageDb.Instance.StageList)
            {
                var item = Instantiate(buttonPrefab);
                item.transform.SetParent(this.transform);
                item.transform.localScale = Vector3.one;
                item.name = $"ITEM_F{stage.Phase}_L{stage.Level}";
                item.AddComponent<StageLoader>().stage = stage;
                item.GetComponentInChildren<Text>().text = $"Fase: {stage.Phase} - Nível:{stage.Level}";
                item.GetComponent<Button>().interactable = Pacient.Loaded.UnlockedLevels >= stage.Id;
            }

            StartCoroutine(AdjustGrip());

            populated = true;
        }

        private IEnumerator AdjustGrip()
        {
            yield return null;
            scrollbar.value = 1;
        }
    }
}