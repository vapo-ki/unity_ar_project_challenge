using UnityEngine;

public class RotationSlider : MonoBehaviour
{
    public Controller controller;

    private bool isDragging;

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


    // Buttons

    public void _OnDragStart()
    {
        isDragging = true;
    }

    public void _OnDragEnd()
    {
        isDragging = false;
    }
}
