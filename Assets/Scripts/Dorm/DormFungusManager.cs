using Fungus;
using UnityEngine;

public class DormFungusManager : MonoBehaviour
{
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Flowchart flowchart;
    [SerializeField] private Animator animFadeout;
    [SerializeField] private GameObject fadeoutObject;
    [SerializeField] private GameObject dormRoom;
    [SerializeField] private GameObject room1;
    [SerializeField] private GameObject room2;
    [SerializeField] private GameObject room3;
    [SerializeField] private GameObject room4;
    [SerializeField] private GameObject room5;
    [SerializeField] private GameObject doorsParent;

    private void Start()
    {
        dormRoom.SetActive(false);
        room1.SetActive(false);
        room2.SetActive(false);
        room3.SetActive(false);
        room4.SetActive(false);
        room5.SetActive(false);
        doorsParent.SetActive(true);
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("roomDoor1"))
            {
                Debug.Log("entering room 1.");
                TriggerFadeoutAnimation();
                //MoveRoomDown();
                Invoke(nameof(FadeoutFalse), 1.5f);
                ExecuteFungusBlock("FreezeCam");
                Invoke(nameof(UnhideRoom1), 3f);

                doorsParent.SetActive(false);
            }

            if (hit.collider != null && hit.collider.CompareTag("roomDoor2"))
            {
                Debug.Log("entering room 2.");
                TriggerFadeoutAnimation();
                //MoveRoomDown();
                Invoke(nameof(FadeoutFalse), 1.5f);
                ExecuteFungusBlock("FreezeCam");
                Invoke(nameof(UnhideRoom2), 3f);

                doorsParent.SetActive(false);
            }

            if (hit.collider != null && hit.collider.CompareTag("roomDoor3"))
            {
                Debug.Log("entering room 3.");
                TriggerFadeoutAnimation();
                //MoveRoomDown();
                Invoke(nameof(FadeoutFalse), 1.5f);
                ExecuteFungusBlock("FreezeCam");
                Invoke(nameof(UnhideRoom3), 3f);

                doorsParent.SetActive(false);
            }

            if (hit.collider != null && hit.collider.CompareTag("roomDoor4"))
            {
                Debug.Log("entering room 4.");
                TriggerFadeoutAnimation();
                //MoveRoomDown();
                Invoke(nameof(FadeoutFalse), 1.5f);
                ExecuteFungusBlock("FreezeCam");
                Invoke(nameof(UnhideRoom4), 3f);

                doorsParent.SetActive(false);
            }

            if (hit.collider != null && hit.collider.CompareTag("roomDoor5"))
            {
                Debug.Log("entering room 5.");
                TriggerFadeoutAnimation();
                //MoveRoomDown();
                Invoke(nameof(FadeoutFalse), 1.5f);
                ExecuteFungusBlock("FreezeCam");
                Invoke(nameof(UnhideRoom5), 3f);

                doorsParent.SetActive(false);
            }

            if (hit.collider != null && hit.collider.CompareTag("ExitRoom"))
            {
                Debug.Log("exit room .");
                TriggerFadeoutAnimation();
                //MoveRoomDown();
                Invoke(nameof(FadeoutFalse), 1.5f);
                ExecuteFungusBlock("UnfreezeCam");
                Invoke(nameof(HideRoom), 3f);

                doorsParent.SetActive(true);
            }
        }
    }
    private void ExecuteFungusBlock(string block)
    {
        if (flowchart != null && !string.IsNullOrEmpty(block))
        {
            flowchart.ExecuteBlock(block);
        }
    }

    public void TriggerFadeoutAnimation()
    {
        fadeoutObject.SetActive(true);
        animFadeout.SetBool("FadeOut", true);
    }

    public void FadeoutFalse()
    {
        animFadeout.SetBool("FadeOut", false);

    }
    public void UnhideRoom1()
    {
        dormRoom.SetActive(true);
        room1.SetActive(true);
    }

    public void UnhideRoom2()
    {
        dormRoom.SetActive(true);
        room2.SetActive(true);
    }
    public void UnhideRoom3()
    {
        dormRoom.SetActive(true);
        room3.SetActive(true);
    }
    public void UnhideRoom4()
    {
        dormRoom.SetActive(true);
        room4.SetActive(true);
    }
    public void UnhideRoom5()
    {
        dormRoom.SetActive(true);
        room5.SetActive(true);
    }
    public void HideRoom()
    {
        dormRoom.SetActive(false);
        room1.SetActive(false);
        room2.SetActive(false);
        room3.SetActive(false);
        room4.SetActive(false);
        room5.SetActive(false);
    }

    /*    public void MoveRoomDown()
        {
            if (objectToMove != null)
            {
                objectToMove.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
            }
        }*/
}
