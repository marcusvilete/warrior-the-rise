using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    SpriteRenderer spRenderer;

    private void Awake()
    {
        spRenderer = GetComponent<SpriteRenderer>();
    }

    public void setBossBackground()
    {
        spRenderer.material.color = new Color(159 / 255f, 39 / 255f, 38 / 255f, 1);
    }
}
