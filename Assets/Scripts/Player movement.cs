using UnityEngine;
using UnityEngine.InputSystem;

public class Playermovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float move_speed = 0;
    Vector2 direction = Vector2.zero;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.linearVelocity = direction * move_speed;
    }

    public void Move_Event(InputAction.CallbackContext context) 
    {
        if(context.performed) 
        {
            Vector2 input = context.ReadValue<Vector2>();
            direction.x = input.x;
        }
        // no input
        else
        {
            direction = Vector2.zero;
        }
    }
}
