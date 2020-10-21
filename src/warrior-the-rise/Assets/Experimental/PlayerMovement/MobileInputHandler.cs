using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class MobileInputHandler : MonoBehaviour
{
    public GameObject toBeControlled;
    public float movementSpeed = 1f;
    public Joystick joystick;

    private Vector2 upperBound;
    private Vector2 lowerBound;

    private void Awake()
    {
        if (toBeControlled == null)
        {
            Debug.LogError("[MobileInputHandler] toBeControlled is null!");
        }

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

        if (Mathf.Abs(joystick.Horizontal) > 0 || Mathf.Abs(joystick.Vertical) > 0)
        {


            Vector3 v = new Vector3(joystick.Horizontal, joystick.Vertical, 0);

            v *= movementSpeed * Time.deltaTime;



            //newPos += toBeControlled.transform.position * movementSpeed * Time.deltaTime;
            toBeControlled.transform.Translate(v);

            FixBounds(toBeControlled.transform.position);






            //Vector3 newPos = toBeControlled.transform.position;
            //newPos.x += (movementSpeed * Time.deltaTime * joystick.Horizontal);
            //newPos.y += (movementSpeed * Time.deltaTime * joystick.Vertical);

            //toBeControlled.transform.position = newPos;
        }
        

        
    }

    private void FixBounds(Vector3 position)
    {
        //float playerSizeOffset = gameObject.transform.localScale.x / 2;

        //if (position.x + playerSizeOffset > upperBound.x ||
        //    position.y + playerSizeOffset > upperBound.y ||
        //    position.x - playerSizeOffset < lowerBound.x ||
        //    position.y - playerSizeOffset < lowerBound.y)
        //{
        //    return false;
        //}

        //return true;

        float playerSizeOffset = toBeControlled.transform.localScale.x / 2;

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
