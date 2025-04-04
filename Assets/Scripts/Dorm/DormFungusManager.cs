using UnityEngine;

public class DormFungusManager : MonoBehaviour
{
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private float moveSpeed = 2f;

    [SerializeField] private Animator animFadeout;
    [SerializeField] private GameObject fadeoutObject;

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
                MoveRoomDown();
            }
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

    public void MoveRoomDown()
    {
        if (objectToMove != null)
        {
            objectToMove.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
    }
}
