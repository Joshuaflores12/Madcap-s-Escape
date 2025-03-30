using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Fungus;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Animator animMC;
    [SerializeField] public float speed = 5f;
    private bool isTyping = false;
    private bool isDialogueActive = false;
    private bool isFrozen = false;

    private float rotationSpeed = 8f;
    private Quaternion targetRotation;
    private Quaternion defaultRotation;

    private float swayFrequency = 4f;
    private float swayAmplitude = 0.105f;
    private float tiltAngle = 0.1f;

    private Vector3 basePosition;

    void Start()
    {
        basePosition = transform.position;
        targetRotation = transform.rotation;
        defaultRotation = transform.rotation;
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
                isMoving = true;
                animMC.Play("MC_LeftWalkSide");  // Play left walk animation
            }
            else if (Input.GetKey(KeyCode.D))
            {
                movement += Vector3.right * speed * Time.deltaTime;
                isMoving = true;
                animMC.Play("MC_RightWalkSide"); // Play right walk animation
            }

            transform.position = new Vector3(movement.x, transform.position.y, transform.position.z);

            if (isMoving)
            {
                float swayOffset = Mathf.Sin(Time.time * swayFrequency) * swayAmplitude;
                transform.position = new Vector3(transform.position.x, basePosition.y + swayOffset, transform.position.z);
            }

        }
    }
    private void ResetRotation()
    {
        transform.rotation = defaultRotation;
    }
    public void MCBackFacing()
    {
        ResetRotation();
        animMC.Play("MC_Back");
    }

    public void MC3_4BFacing()
    {
        ResetRotation();
        animMC.Play("MC_3_4B");
    }

    public void MCSideFacing()
    {
        ResetRotation();
        animMC.Play("MC_Side");
    }

    public void MC3_4FFacing()
    {
        ResetRotation();
        animMC.Play("MC_3_4F");
    }

    public void MCFrontFacing()
    {
        ResetRotation();
        animMC.Play("MC_Idle");
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
