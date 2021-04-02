using PolyToolkit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModelThumbnail : MonoBehaviour
{
    public GameObject modelPreview;

    public Texture2D modelPreviewTexture;
    public bool isEmpty;
    public PolyAsset polyModel;

    private RawImage modelThumbnail;
    private TextMeshProUGUI modelName;
    private TextMeshProUGUI modelAuthor;

    void Start()
    {
        modelThumbnail = GetComponent<RawImage>();
        modelName = transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        modelAuthor = transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        SetEmpty();
    }

    public void SetPolyModel(PolyAsset pa)
    {
        polyModel = pa;

        modelName.SetText(polyModel.displayName);
        modelAuthor.SetText(polyModel.authorName);
        PolyApi.FetchThumbnail(polyModel, SetThumbnail);

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
    }

    public void _OnClick()
    {
        modelPreview.SetActive(true);
        modelPreview.GetComponent<ModelPreview>().Initialize(polyModel);
    }
}
