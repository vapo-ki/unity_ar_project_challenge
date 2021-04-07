using PolyToolkit;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UsedModels : MonoBehaviour
{
    public GameObject modelContainer;
    public TextMeshProUGUI pageText;
    public TextMeshProUGUI noResultsText;

    public List<ModelThumbnail> modelPreviewList = new List<ModelThumbnail>();
    public List<PolyAsset> usedAssets = new List<PolyAsset>();
    public List<GameObject> usedModels = new List<GameObject>();

    private int page;

    private void Start()
    {
        //Initialize
        page = 0;

    }

    private void LoadModels()
    {
        //Add all models that are loaded in the scene into the list
        foreach (Transform child in modelContainer.transform)
        {
            Model model = child.GetChild(0).GetComponent<Model>();
            usedAssets.Add(model.polyAsset);

            usedModels.Add(child.GetChild(0).gameObject);
        }

        print(usedAssets.Count);

        Refresh();
    }

    private void Refresh()
    {
        //If there are any models loaded in, pass them into the designated thumbnail slot
        if (usedAssets.Count == 0)
        {
            noResultsText.gameObject.SetActive(true);
            foreach (ModelThumbnail modelThumbnail in modelPreviewList)
            {
                modelThumbnail.SetEmpty();
            }
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


    // Buttons

    public void _OnOpenUsedModelsMenu()
    {
        gameObject.SetActive(true);
        usedAssets = new List<PolyAsset>();
        usedModels = new List<GameObject>();
        LoadModels();
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

    public void _OnCloseUsedModels()
    {
        gameObject.SetActive(false);
    }
}
