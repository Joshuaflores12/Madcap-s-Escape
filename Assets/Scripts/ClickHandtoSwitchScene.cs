using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickHandtoSwitchScene : MonoBehaviour
{

    [SerializeField] private ContinueDialogue continueDialogue;
    [SerializeField] private FungusManager fungusManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            
            if (SceneManager.GetActiveScene().name == "2_CanteenDorm")
            {
                if (hit.collider != null && hit.collider.CompareTag("LeftHand"))
                {
                    Debug.Log("clicked left hand. ");
                    continueDialogue.TriggerHallwayLeftTransition();
                }

                if (hit.collider != null && hit.collider.CompareTag("RightHand"))
                {
                    Debug.Log("clicked right hand. ");

                    // condition before proceeding to transition scene: the player must interact with patient 1,2, or 3. items needed: cake or flower
                    continueDialogue.TriggerHallwayDormTransition();
                }
            }

            if (SceneManager.GetActiveScene().name == "1_IsolationChamber")
            {

                if (hit.collider != null && hit.collider.CompareTag("RightHand"))
                {
                    Debug.Log("clicked right hand. ");
                    if(fungusManager.canExitIsolationChamber == true)
                    {
                        continueDialogue.TriggerHallwayLeftTransition();

                    }
                }
            }

            if (SceneManager.GetActiveScene().name == "HallwayLeft")
            {

                if (hit.collider != null && hit.collider.CompareTag("RightHand"))
                {
                    Debug.Log("clicked right hand. ");

                    continueDialogue.TriggerCanteenTransition();
                }
            }

            if (SceneManager.GetActiveScene().name == "HallwayDorm")
            {
                if (hit.collider != null && hit.collider.CompareTag("LeftHand"))
                {
                    Debug.Log("clicked left hand. ");
                    continueDialogue.TriggerCanteenTransition();
                }

                if (hit.collider != null && hit.collider.CompareTag("RightHand"))
                {
                    Debug.Log("clicked right hand. ");

                    //continueDialogue.Trigger   Transition();
                }
            }
        }
    }
}
