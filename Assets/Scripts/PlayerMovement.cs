using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playermovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] public float move_speed = 0;
    [SerializeField] Vector2 direction = Vector2.zero;
    [SerializeField] TMP_InputField playerNameInput;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (playerNameInput != null && playerNameInput.isFocused && Input.GetKeyDown(KeyCode.Return))
        {
            playerNameInput.DeactivateInputField();
        }
        rb.velocity = direction * move_speed;
    }

    public void Move_Event(InputAction.CallbackContext context)
    {
        // If player is inputting their name disable movement controls
        if(playerNameInput != null && playerNameInput.isFocused) 
        {
            direction = Vector2.zero;
            return;
        }

        if (context.performed) 
        {
            Vector2 input = context.ReadValue<Vector2>();
            direction.x = input.x;
        }
        else // No Input
        {
            direction = Vector2.zero;
        }
    }
}
