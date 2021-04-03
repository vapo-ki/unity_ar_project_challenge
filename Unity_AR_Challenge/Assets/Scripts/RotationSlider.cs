using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotationSlider : MonoBehaviour
{
    public Controller controller;

    private bool isDragging;

    public void _OnDragStart()
    {
        isDragging = true;
    }

    public void _OnDragEnd()
    {
        isDragging = false;
    }

    private void Update()
    {
        if (isDragging)
        {
            foreach (Touch touch in Input.touches)
            {
                controller.RotateModel(touch.deltaPosition.x);
            }
        }
        
    }
}
