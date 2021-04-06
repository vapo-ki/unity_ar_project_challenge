using PolyToolkit;
using UnityEngine;

public class ModelManager : MonoBehaviour
{
    public Material selectedMaterial;
    public Controller controller;

    public void AddModel(PolyStatusOr<PolyImportResult> result, PolyAsset polyAsset)
    {
        //Take take the child (actual model) of the imported gameobject (container) and prepare it, then add them to the ModelContainer as a child

        GameObject polyModel = result.Value.gameObject;
        GameObject model = polyModel.transform.GetChild(0).gameObject;

        model.AddComponent<MeshCollider>();

        model.AddComponent<Model>();
        model.GetComponent<Model>().selectedMaterial = selectedMaterial;
        model.GetComponent<Model>().modelManager = this;
        model.GetComponent<Model>().polyAsset = polyAsset;
        model.GetComponent<Model>().controller = controller;

        polyModel.transform.SetParent(transform);
        SelectModel(model);
    }

    public void SelectModel(GameObject model)
    {
        //Unselect all, then select the wanted model
        UnselectAll();

        model.GetComponent<Model>().SetSelected();
        controller.SelectModel(model);
    }

    public void UnselectAll()
    {
        controller.UnselectModel();
        foreach (Transform child in transform) 
        {
            Model model = child.GetChild(0).GetComponent<Model>();
            model.SetUnselected();
        }
    }
}
