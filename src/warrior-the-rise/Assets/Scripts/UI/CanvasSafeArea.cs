using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasSafeArea : MonoBehaviour
{
    public RectTransform safeAreaUI;

    void Awake()
    {

        if (safeAreaUI == null)
            return;

        var canvas = GetComponent<Canvas>();

        

        var safeArea = Screen.safeArea;

        var anchorMin = safeArea.position;
        var anchorMax = safeArea.position + safeArea.size;
        anchorMin.x /= canvas.pixelRect.width;
        anchorMin.y /= canvas.pixelRect.height;
        anchorMax.x /= canvas.pixelRect.width;
        anchorMax.y /= canvas.pixelRect.height;

        safeAreaUI.anchorMin = anchorMin;
        safeAreaUI.anchorMax = anchorMax;


    }
}
