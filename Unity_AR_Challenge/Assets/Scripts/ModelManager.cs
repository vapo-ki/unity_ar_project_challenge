using PolyToolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : MonoBehaviour
{
    public Material selectedMaterial;
    public Controller controller;

    public void AddModel(PolyStatusOr<PolyImportResult> result, PolyAsset polyAsset)
    {
        GameObject polyModel = result.Value.gameObject;
        GameObject model = polyModel.transform.GetChild(0).gameObject;

        model.AddComponent<MeshCollider>();

        model.AddComponent<Model>();
        model.GetComponent<Model>().selectedMaterial = selectedMaterial;
        model.GetComponent<Model>().modelManager = this;
        model.GetComponent<Model>().polyAsset = polyAsset;

        polyModel.transform.SetParent(transform);
        SelectModel(model);
    }

    public void SelectModel(GameObject model)
    {
        UnselectAll();
        model.GetComponent<Model>().SetSelected();
        controller.SelectModel(model);
    }

    public void UnselectAll()
    {
        foreach (Transform child in transform) 
        {
            Model model = child.GetChild(0).GetComponent<Model>();
            model.SetUnselected();
        }
    }
}
