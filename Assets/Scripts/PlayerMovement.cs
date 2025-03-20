using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playermovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] public float move_speed = 0;
    [SerializeField] Vector2 direction = Vector2.zero;
    [SerializeField] TMP_InputField playerNameInput;

    [SerializeField] FirstChallenge firstChallenge;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        firstChallenge = FindFirstObjectByType<FirstChallenge>();
    }

    private void Update()
    {
        if (playerNameInput != null && playerNameInput.isFocused && Input.GetKeyDown(KeyCode.Return))
        {
            playerNameInput.DeactivateInputField();
        }

        rb.linearVelocity = direction * move_speed;
    }

    public void Move_Event(InputAction.CallbackContext context)
    {
        // If player is inputting their name, disable movement but allow typing
        if (playerNameInput != null && playerNameInput.isFocused)
        {
            return;
        }

        if (firstChallenge != null && firstChallenge.disableMovement)
        {
            direction = Vector2.zero;
            return;
        }

        if (context.performed)
        {
            Vector2 input = context.ReadValue<Vector2>();
            direction.x = input.x;
        }
        else if (context.canceled) // No Input
        {
            direction = Vector2.zero;
        }
    }
}
