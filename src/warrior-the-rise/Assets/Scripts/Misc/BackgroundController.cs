using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public SpriteRenderer[] spRenderers;


    public void setBossBackground()
    {

        foreach (SpriteRenderer spRenderer in spRenderers)
        {
            //spRenderer.material.color = new Color(159 / 255f, 39 / 255f, 38 / 255f, 1);
            spRenderer.material.color = new Color(255 / 255f, 77 / 255f, 73 / 255f, 1);
        }

        
    }
}
