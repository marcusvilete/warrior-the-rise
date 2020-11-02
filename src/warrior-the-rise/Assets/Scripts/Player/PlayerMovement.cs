using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    public GameObject toBeControlled;
    public float movementSpeed = 1f;
    private Joystick joystick;

    private Vector2 upperBound;
    private Vector2 lowerBound;

    private void Awake()
    {
        if (toBeControlled == null)
        {
            Debug.LogError("[MobileInputHandler] toBeControlled is null!");
        }

        //Look for a joystick in the scene
        joystick = FindObjectOfType<Joystick>();

        if (joystick == null)
        {
            Debug.LogError("[MobileInputHandler] joystrick is null!");
        }

        upperBound = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        lowerBound = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float horizontal = 0f;
        float vertical = 0f;

        if (Mathf.Abs(joystick.Horizontal) > 0.3)
        {
            horizontal = joystick.Horizontal;
        }

        if (Mathf.Abs(joystick.Vertical) > 0.3)
        {
            vertical = joystick.Vertical;
        }

        if (Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0)
        {
            Vector3 v = new Vector3(horizontal, vertical, 0);

            v *= movementSpeed * Time.deltaTime;
            
            toBeControlled.transform.Translate(v);

            FixBounds(toBeControlled.transform.position);
        }

    }

    private void FixBounds(Vector3 position)
    {
        float playerSizeOffset = toBeControlled.transform.lossyScale.x / 2;

        if (position.x + playerSizeOffset > upperBound.x)
        {
            position.x = upperBound.x - playerSizeOffset;
        }

        if (position.x - playerSizeOffset < lowerBound.x)
        {
            position.x = lowerBound.x + playerSizeOffset;
        }

        if (position.y + playerSizeOffset > upperBound.y)
        {
            position.y = upperBound.y - playerSizeOffset;
        }

        if (position.y - playerSizeOffset < lowerBound.y)
        {
            position.y = lowerBound.y + playerSizeOffset;
        }

        toBeControlled.transform.position = position;

    }
    
}
