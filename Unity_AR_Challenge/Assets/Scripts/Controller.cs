using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public GameObject selectedModelUI;
    public Button rotationSlider;

    public GameObject selectedModel;
    private float rotationSpeed = 10f;
    private float scaleSpeed = 25f;

    public void SelectModel(GameObject model)
    {
        selectedModel = model;
        selectedModelUI.SetActive(true);
    }


    public void RotateModel(float touchDeltaX)
    {
        if (selectedModel != null)
        {
            float rotation = touchDeltaX * rotationSpeed * Mathf.Deg2Rad;
            selectedModel.transform.Rotate(Vector3.up, -rotation);
        }
    }

    public void ScaleModel(float touchDeltaY)
    {
        if (selectedModel != null)
        {
            float scale = touchDeltaY / scaleSpeed;

            Vector3 scaleVector = selectedModel.transform.localScale;
            selectedModel.transform.localScale = new Vector3(scaleVector.x + scale, scaleVector.y + scale, scaleVector.z + scale);
        }
    }

}
