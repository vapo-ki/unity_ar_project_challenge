using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Controller : MonoBehaviour
{
    public GameObject selectedModelUI;
    public Button rotationSlider;
    public ARRaycastManager arRayManager;
    public GameObject selectedModel;
    public TextMeshProUGUI lockButtonText;
    public ModelManager modelManager;

    public bool isUnlocked;
    private float rotationSpeed = 10f;
    private float scaleSpeed = 25f;

    private void Start()
    {
        isUnlocked = true;
    }

    private void Update()
    {
        // If movement is unlocked and a model has been selected update its position 
        if (selectedModel != null && isUnlocked)
        {
            UpdateModelPosition();
        }

        // If nothing is touched unselect all
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(0) == false && selectedModel != null && !isUnlocked)
            {
                modelManager.UnselectAll();
            }
        }
    }

    private void UpdateModelPosition()
    {
        //Update Position to the center of where the phone is pointing too
        Vector3 screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        arRayManager.Raycast(screenCenter, hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            selectedModel.transform.position = hits[0].pose.position;
        }

    }

    public void SelectModel(GameObject model)
    {
        selectedModel = model;
        selectedModelUI.SetActive(true);

    }

    public void UnselectModel()
    {
        selectedModel = null;
        selectedModelUI.SetActive(false);
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

    public void SetLocked()
    {
        isUnlocked = false;
        lockButtonText.SetText("Unlock");
        modelManager.UnselectAll();
    }

    public void SetUnlocked()
    {
        isUnlocked = true;
        lockButtonText.SetText("Lock");
    }


    // Buttons

    public void _OnDelete()
    {
        Destroy(selectedModel);
        modelManager.UnselectAll();
    }

    public void _OnToggleLock()
    {
        if (selectedModel != null)
        {
            if (isUnlocked)
            {
                SetLocked();
            }
            else
            {
                SetUnlocked();
            }
        }
    }
}
