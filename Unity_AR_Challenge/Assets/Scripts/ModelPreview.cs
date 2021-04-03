using PolyToolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModelPreview : MonoBehaviour
{
    private PolyAsset polyModel;

    public TextMeshProUGUI modelName;
    public TextMeshProUGUI author; 
    public TextMeshProUGUI complexity;
    public TextMeshProUGUI dateAdded; 
    public TextMeshProUGUI lastEdit;
    public RawImage modelThumbnail;

    public GameObject modelContainer;
    public GameObject menu;
    public Controller controller;
    public ModelManager modelManager;
   

    public void Initialize(PolyAsset pa)
    {
        polyModel = pa;

        modelName.SetText(pa.displayName);
        author.SetText(pa.authorName);
        complexity.SetText("Tris: " + pa.formats[0].formatComplexity.triangleCount.ToString());
        dateAdded.SetText("Created at: " + pa.createTime.ToShortDateString());
        lastEdit.SetText("Last edited: " + pa.updateTime.ToShortDateString());

        PolyApi.FetchThumbnail(pa, SetThumbnail);
    }

    private void SetThumbnail(PolyAsset asset, PolyStatus status)
    {
        if (!status.ok)
        {
            // Handle error;
            return;
        }

        modelThumbnail.texture = asset.thumbnailTexture;
    }

    public void _OnAddModelToScene()
    {
        PolyApi.Import(polyModel, PolyImportOptions.Default(), AddModel);
    }

    private void AddModel(PolyAsset asset, PolyStatusOr<PolyImportResult> result)
    {
        if (!result.Ok)
        {
            // Handle error.
            return;
        }

        modelManager.AddModel(result.Value.gameObject);
        menu.SetActive(false);
        transform.gameObject.SetActive(false);
    }
}
