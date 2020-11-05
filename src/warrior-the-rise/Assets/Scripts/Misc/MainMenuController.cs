using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    void Awake()
    {
        SetCamera();
    }

    public void NewGameClick()
    {
        GameManager.Instance.LoadLevel("TemplateLevel");
        //GameManager.Instance.LoadLevel("Level1");
    }

    public void QuitGameClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }


    //TODO: maybe move this to a helper class, since we are doing this every scene!
    private void SetCamera()
    {
        float width = (float)Screen.safeArea.width / Screen.width;
        float height = (float)Screen.safeArea.height / Screen.height;
        float x = (Screen.safeArea.x / Screen.width);
        float y = (Screen.safeArea.y / Screen.height);
        Camera.main.rect = new Rect(x, y, width, height);
    }


}
