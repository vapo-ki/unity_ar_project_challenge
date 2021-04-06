using PolyToolkit;
using UnityEngine;
using UnityEngine.EventSystems;

public class Model : MonoBehaviour
{
    public ModelManager modelManager;
    public Controller controller;

    public PolyAsset polyAsset;
    public Material selectedMaterial;

    private bool isSelected;

    public void SetSelected()
    {
        //Set selected and give model a new material
        isSelected = true;
        Material[] materialList = new Material[2];
        materialList[0] = GetComponent<MeshRenderer>().materials[0];
        materialList[1] = selectedMaterial;

        GetComponent<MeshRenderer>().materials = materialList;
    }

    public void SetUnselected()
    {
        isSelected = false;
        Material[] materialList = new Material[1];
        materialList[0] = GetComponent<MeshRenderer>().materials[0];
        GetComponent<MeshRenderer>().materials = materialList;
    }

    private void OnMouseUp()
    {
        if (!controller.isUnlocked)
        {
            if (EventSystem.current.IsPointerOverGameObject(0) == false)
            {
                modelManager.SelectModel(gameObject);
            }
        }
            
    }

}
