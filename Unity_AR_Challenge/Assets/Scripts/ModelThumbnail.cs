using PolyToolkit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModelThumbnail : MonoBehaviour
{
    public GameObject modelPreview;
    public ModelManager modelManager;

    public Texture2D modelPreviewTexture;
    public bool isEmpty;
    public PolyAsset polyAsset;
    public GameObject usedModels;

    public GameObject usedModel;

    private RawImage modelThumbnail;
    private TextMeshProUGUI modelName;
    private TextMeshProUGUI modelAuthor;

    void Awake()
    {
        modelThumbnail = GetComponent<RawImage>();
        modelName = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        modelAuthor = transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        SetEmpty();
    }

    public void SetPolyModel(PolyAsset polyAsset)
    {
        this.polyAsset = polyAsset;
        modelName.SetText(polyAsset.displayName);
        modelAuthor.SetText(polyAsset.authorName);
        PolyApi.FetchThumbnail(polyAsset, SetThumbnail);

        SetFilled();
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

    private void SetFilled()
    {
        isEmpty = false;
        modelThumbnail.color = new Color(255, 255, 255, 255);
    }

    public void SetEmpty()
    {
        isEmpty = true;
        modelThumbnail.color = new Color(255, 255, 255, 0);
        modelThumbnail.texture = null;

        modelName.SetText("");
        modelAuthor.SetText("");
        usedModel = null;
    }

    public void _OnClick()
    {
        modelPreview.SetActive(true);
        modelPreview.GetComponent<ModelPreview>().Initialize(polyAsset);
    }

    public void _OnClickUsedModel()
    {
        modelManager.SelectModel(usedModel);
        usedModels.SetActive(false);
    }
}
