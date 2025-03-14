using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public float move_speed;
    [SerializeField] public Vector2 direction;
    [SerializeField] public TMP_InputField playerNameInput;
    [SerializeField] public PlayerInput playerInput;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerNameInput.onSelect.AddListener(DisablePLayerInput);
        playerNameInput.onDeselect.AddListener(EnablePLayerInput);
    }

    private void DisablePLayerInput(string text) 
    {
        if (playerInput != null) 
        {
            playerInput.enabled = false;
            direction = Vector2.zero;
        }
    }

    private void EnablePLayerInput(string text) 
    {
        if(playerInput != null) 
        {
            playerInput.enabled = true;
            rb.linearVelocity = direction * move_speed;
        }
    }
}
