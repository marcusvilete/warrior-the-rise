using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixCameraRatio : MonoBehaviour
{

    public int xAspect = 9;
    public int yAspect = 16;

    void Awake()
    {
        ForcePortrait();
        //Screen.SetResolution(1080, 1920, true);
        //Screen.SetResolution(1080, 1920, true);
        //Screen.SetResolution(720, 1280, true);

        //GetComponent<Camera>().rect = Screen.safeArea;
        //float screenRatio = (float)Screen.width / Screen.height;
        //float screenRatio = (float)Screen.safeArea.width / Screen.safeArea.height;
        //float bestRatio = (float)xAspect / yAspect;


        //GetComponent<Camera>().rect = new Rect(0, (1f - screenRatio / bestRatio) / 2f, 1, screenRatio / bestRatio);

        //float screenRatio = Screen.width * 1f / Screen.height;
        //float bestRatio = xAspect * 1f / yAspect;
        //if (screenRatio <= bestRatio)
        //{
        //    GetComponent<Camera>().rect = new Rect(0, (1f - screenRatio / bestRatio) / 2f, 1, screenRatio / bestRatio);
        //}
        //else if (screenRatio > bestRatio)
        //{
        //    GetComponent<Camera>().rect = new Rect((1f - bestRatio / screenRatio) / 2f, 0, bestRatio / screenRatio, 1);
        //}


        //GetComponent<Camera>().backgroundColor = Color.gray;





        //Set camera to safe area;
        float width = (float)Screen.safeArea.width / Screen.width;
        float height = (float)Screen.safeArea.height / Screen.height;
        float x = (Screen.safeArea.x / Screen.width);
        float y = (Screen.safeArea.y / Screen.height);
        GetComponent<Camera>().rect = new Rect(x, y, width, height);
    }

    private void ForcePortrait()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    


}
