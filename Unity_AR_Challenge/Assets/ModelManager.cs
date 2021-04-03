using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelManager : MonoBehaviour
{
    public Material selectedMaterial;
    public Controller controller;

    public void AddModel(GameObject polyModel)
    {
        GameObject model = polyModel.transform.GetChild(0).gameObject;

        model.AddComponent<MeshCollider>();

        model.AddComponent<Model>();
        model.GetComponent<Model>().selectedMaterial = selectedMaterial;
        model.GetComponent<Model>().modelManager = this;

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
            print("AAAA");
            Model model = child.GetChild(0).GetComponent<Model>();
            model.SetUnselected();
        }
    }
}
