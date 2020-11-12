using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundProjTest : MonoBehaviour
{
    Vector3 rotateAround;
    float speed;
    bool isRunning = false;

    void Update()
    {
        if (isRunning)
        {
            transform.RotateAround(rotateAround, Vector3.forward, speed * Time.deltaTime);
        }
    }

    public void Initialize(Vector3 rotateAround, float speed)
    {
        this.rotateAround = rotateAround;
        this.speed = speed;
        isRunning = true;
    }
}
