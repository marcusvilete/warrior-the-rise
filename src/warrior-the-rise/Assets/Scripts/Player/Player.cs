using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public SpriteRenderer shieldRenderer;
    public GameObject toBeControlled;
    public float movementSpeed = 1f;
    private Joystick joystick;
    Animator animator;
    private bool movementEnabled;

    Health health;

    private Vector2 upperBound;
    private Vector2 lowerBound;

    private void Awake()
    {
        if (toBeControlled == null)
        {
            Debug.LogError("[MobileInputHandler] toBeControlled is null!");
        }

        animator = GetComponentInChildren<Animator>();
        health = GetComponent<Health>();
        //Look for a joystick in the scene
        joystick = FindObjectOfType<Joystick>();
        
        if (joystick == null)
        {
            Debug.LogError("[MobileInputHandler] joystrick is null!");
        }

        upperBound = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        lowerBound = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        movementEnabled = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!movementEnabled) return;

        float horizontal = 0f;
        float vertical = 0f;

        if (Mathf.Abs(joystick.Horizontal) > 0.2)
        {
            horizontal = joystick.Horizontal;
        }

        if (Mathf.Abs(joystick.Vertical) > 0.2)
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

    

    public void DisableMovement()
    {
        movementEnabled = false;
    }

    public void EnableMovement()
    {
        movementEnabled = true;
    }

    public void ResetPosition(bool shouldDisableMovement)
    {
        //TODO: animate?

        //transform.position = new Vector3(0, -6, 0);

        StartCoroutine(MoveTowards(transform, transform.position, new Vector3(0, -6, 0), 0.75f, shouldDisableMovement));
    }

    IEnumerator MoveTowards(Transform tr, Vector3 from, Vector3 to, float aTime, bool shouldDisableMovement)
    {

        if (shouldDisableMovement) DisableMovement();

        health.CanBeDamaged = false;

        for (float t = 0.0f; t < 1; t += (Time.deltaTime / aTime))
        {
            tr.position = Vector3.Lerp(from, to, t);
            yield return new WaitForEndOfFrame();
        }

        if (shouldDisableMovement)  EnableMovement();
        health.CanBeDamaged = true;
    }

    public void TakeDamage(float amount)
    {
        if (health.CanBeDamaged && amount > 0f )
        {
            animator.Play("Damaged");
            health.TakeDamage(amount);
        }
        
    }
    public void PlayAttackAnimation()
    {
        animator.Play("Attack");
    }

    private void PlayDamageTakenAnimation()
    {
        animator.Play("Damaged");
    }

}
