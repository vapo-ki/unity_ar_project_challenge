using PolyToolkit;
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
        // Display all information about the model
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
            Debug.Log("Fetching Thumbnail Error: " + status.errorMessage);
            return;
        }

        modelThumbnail.texture = asset.thumbnailTexture;
    }

    private void AddModel(PolyAsset asset, PolyStatusOr<PolyImportResult> result)
    {
        if (!result.Ok)
        {
            Debug.Log("Importing Model Error: " + result.Status.errorMessage);
            return;
        }

        // Add model and set menus to inactive
        modelManager.AddModel(result, asset);
        menu.SetActive(false);
        transform.gameObject.SetActive(false);
        controller.SetUnlocked();
    }

    
    // Buttons

    public void _OnAddModelToScene()
    {
        PolyApi.Import(polyModel, PolyImportOptions.Default(), AddModel);
    }
    
    public void _OnExitPreview()
    {
        gameObject.SetActive(false);
    }
}
