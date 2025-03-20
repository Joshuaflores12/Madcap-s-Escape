using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Playermovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] public float move_speed;
    [SerializeField] Vector2 direction;
    [SerializeField] FirstChallenge firstChallenge;
    [SerializeField] TMP_InputField PlayerName;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        firstChallenge = FindFirstObjectByType<FirstChallenge>();
    }

    private void Update()
    {
        if (PlayerName != null && PlayerName.isFocused) 
        {
         rb.linearVelocity = Vector2.zero;
            return;
        }

        // movement
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.A)) 
        {
            direction.x = -1;
        }

        if (Input.GetKey(KeyCode.D)) 
        {
            direction.x = 1;
        }
        rb.linearVelocity = direction * move_speed;
    }
}
