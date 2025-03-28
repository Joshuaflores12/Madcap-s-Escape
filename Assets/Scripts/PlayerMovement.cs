using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Fungus;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private bool isTyping = false;
    private bool isDialogueActive = false;
    private bool isFrozen = false;

    private float rotationSpeed = 8f;
    private Quaternion targetRotation;

    private float swayFrequency = 4f;
    private float swayAmplitude = 0.105f;
    private float tiltAngle = 0.1f;

    private Vector3 basePosition;

    void Start()
    {
        basePosition = transform.position;
        targetRotation = transform.rotation;
    }

    void Update()
    {
        isTyping = EventSystem.current.currentSelectedGameObject != null &&
                   EventSystem.current.currentSelectedGameObject.GetComponent<InputField>() != null;

        isDialogueActive = SayDialog.ActiveSayDialog != null && SayDialog.ActiveSayDialog.gameObject.activeInHierarchy;

        if (!isTyping && !isDialogueActive && !isFrozen)
        {
            Vector3 movement = transform.position;
            bool isMoving = false;

            if (Input.GetKey(KeyCode.A))
            {
                movement += Vector3.left * speed * Time.deltaTime;
                targetRotation = Quaternion.Euler(0, 180, 0);
                isMoving = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                movement += Vector3.right * speed * Time.deltaTime;
                targetRotation = Quaternion.Euler(0, 0, 0);
                isMoving = true;
            }

            transform.position = new Vector3(movement.x, transform.position.y, transform.position.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            if (isMoving)
            {
                float swayOffset = Mathf.Sin(Time.time * swayFrequency) * swayAmplitude;
                transform.position = new Vector3(transform.position.x, basePosition.y + swayOffset, transform.position.z);

                float tilt = Mathf.Sin(Time.time * swayFrequency) * tiltAngle;
                transform.rotation *= Quaternion.Euler(0, 0, tilt);
            }
        }
    }

    public void FreezePlayer()
    {
        isFrozen = true;
    }

    public void UnfreezePlayer()
    {
        isFrozen = false;
    }
}