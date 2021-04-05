using PolyToolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class UsedModels : MonoBehaviour
{
    public List<ModelThumbnail> modelPreviewList = new List<ModelThumbnail>();
    public GameObject modelContainer;
    public TextMeshProUGUI pageText;
    public TextMeshProUGUI noResultsText;
    public  List<PolyAsset> usedAssets = new List<PolyAsset>();
    public List<GameObject> usedModels = new List<GameObject>();

    private int page;

    private void Start()
    {
        //Initialize
        page = 0;

    }

    public void _OnOpenUsedModelsMenu()
    {
        gameObject.SetActive(true);
        usedAssets = new List<PolyAsset>();
        usedModels = new List<GameObject>();
        LoadModels();
    }

    private void LoadModels()
    {
        foreach(Transform child in modelContainer.transform)
        {
            Model model = child.GetChild(0).GetComponent<Model>();
            usedAssets.Add(model.polyAsset);

            usedModels.Add(child.GetChild(0).gameObject);
        }

        Refresh();
    }

    private void Refresh()
    {
        if (usedAssets.Count == 0)
        {
            noResultsText.gameObject.SetActive(true);
        }
        else
        {
            noResultsText.gameObject.SetActive(false);

            var count = 0;
            foreach (ModelThumbnail modelThumbnail in modelPreviewList)
            {
                print(count + (page * 6));
                if ((count + (page * 6)) <= usedAssets.Count - 1)
                {
                    modelThumbnail.SetPolyModel(usedAssets[count + (page * 6)]);
                    modelThumbnail.usedModel = usedModels[count + (page * 6)];
                    count++;
                }
                else
                {
                    modelThumbnail.SetEmpty();
                }
            }
        }    
    }
    public void _OnNextPageButton()
    {
        if (((page + 1) * 6) <= (usedAssets.Count))
        {
            page++;
            pageText.SetText((page + 1).ToString());
            Refresh();
        }

    }

    public void _OnPreviousPageButton()
    {
        if (page > 0)
        {
            page--;
            pageText.SetText((page + 1).ToString());
            Refresh();
        }
    }
}
