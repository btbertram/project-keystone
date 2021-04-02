using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Utility class with functions for more easily manipulating UI elements.
/// </summary>
public class UIManipulationUtility
{
    Camera _cameraRef;

    void Awake()
    {

    }

    public UIManipulationUtility()
    {
        _cameraRef = GameObject.FindObjectOfType<Camera>();
    }

    public void SetAnchors(RectTransform rectTransform, EAnchorPos Vertical, EAnchorPos Horizontal)
    {
        Vector2 anchorMin = new Vector2(0,0);
        Vector2 anchorMax = new Vector2(0,0);

        switch (Vertical)
        {
            case EAnchorPos.Top:

                anchorMin.y = 1;
                anchorMax.y = 1;

                break;
            case EAnchorPos.Center:
                anchorMin.y = .5f;
                anchorMax.y = .5f;

                break;
            case EAnchorPos.Bottom:
                anchorMin.y = 0;
                anchorMax.y = 0;
                break;

        }

        switch (Horizontal)
        {
            case EAnchorPos.Left:
                anchorMin.x = 0;
                anchorMax.x = 0;
                break;
            case EAnchorPos.Center:
                anchorMin.x = .5f;
                anchorMax.x = .5f;
                break;
            case EAnchorPos.Right:
                anchorMin.x = 1;
                anchorMax.x = 1;
                break;
        }

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;
    }

    /// <summary>
    /// Uses the camera's scaledPixelWidth/height and a percentage of the screen to dynamically scale UI elements at runtime.
    /// </summary>
    /// <param name="rectTransform">The RectTransform of the UI element to set the size of.</param>
    /// <param name="percentWidth">The percentage of the camera view's width this UI element should cover.</param>
    /// <param name="percentHeight">The percentage of the camera view's height this UI element should cover.</param>
    public void SetRectTransformSizeToScreenScale(RectTransform rectTransform, float percentWidth, float percentHeight)
    {
        Mathf.Clamp(percentWidth, 0, 1);
        Mathf.Clamp(percentHeight, 0, 1);
        float width = _cameraRef.scaledPixelWidth * percentWidth;
        float height = _cameraRef.scaledPixelHeight * percentHeight;

        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }
   
}
