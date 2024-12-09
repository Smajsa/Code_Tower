using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    /// <summary>
    /// ORIGINAL
    /// </summary>
    public Sprite unpressedSprite; // Assign the "unpressed" sprite in the Inspector
    public Sprite pressedSprite;   // Assign the "pressed" sprite in the Inspector
    public bool isPressed = false; // Tracks the button's state
    private Button_Container childContainer; //reference to container script
    private bool isProcessing = false;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        childContainer = GetComponentInChildren<Button_Container>();
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
        // Set the initial sprite to "unpressed"
        spriteRenderer.sprite = unpressedSprite;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player is colliding with the button
        if (collision.gameObject.CompareTag("Player") && !isPressed)
        {
            isPressed = true;
            spriteRenderer.sprite = pressedSprite; // Switch to "pressed" sprite
            StartCoroutine(OnButtonPressed(1f)); // Trigger logic for when the button is pressed
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Reset to "unpressed" when the player leaves (if needed)
        if (collision.gameObject.CompareTag("Player"))
        {
            isPressed = false;
            spriteRenderer.sprite = unpressedSprite; // Switch back to "unpressed"
        }
    }

    // Logic triggered when the button is pressed
    private IEnumerator OnButtonPressed(float delay)
    {
        if (isProcessing) yield break;//prevent multiple coroutines
        isProcessing = true;
        if (childContainer.Ready())
        {
            childContainer.startProcess();
            Debug.Log("Integer Stored in Container");
        }
        yield return new WaitForSeconds(delay);
        isProcessing = false;
    }
}
