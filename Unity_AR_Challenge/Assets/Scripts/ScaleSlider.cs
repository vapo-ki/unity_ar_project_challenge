using UnityEngine;

public class ScaleSlider : MonoBehaviour
{
    public Controller controller;

    private bool isDragging;

    private void Update()
    {
        if (isDragging)
        {
            foreach (Touch touch in Input.touches)
            {
                controller.ScaleModel(touch.deltaPosition.y);
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
