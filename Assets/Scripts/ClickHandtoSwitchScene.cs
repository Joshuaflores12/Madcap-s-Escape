using Fungus;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickHandtoSwitchScene : MonoBehaviour
{

    [SerializeField] private ContinueDialogue continueDialogue;
    [SerializeField] private FungusManager fungusManager;
    [SerializeField] private Flowchart flowchart;
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

            
            if (SceneManager.GetActiveScene().name == "2_HallwayDoctors")
            {


/*                if (hit.collider != null && hit.collider.CompareTag("RightHand"))
                {
                    Debug.Log("clicked right hand. ");

                    // condition before proceeding to transition scene: the player must interact with patient 1,2, or 3. items needed: cake or flower
                    continueDialogue.TriggerHallwayDormTransition();
                }*/
            }

            if (SceneManager.GetActiveScene().name == "1_IsolationChamber")
            {

                if (hit.collider != null && hit.collider.CompareTag("RightHand"))
                {
                    Debug.Log("clicked right hand. to hallway doctors");
                    if(fungusManager.canExitIsolationChamber == true)
                    {
                        continueDialogue.TriggerHallwayDoctorsTransition();

                    }
                }
            }

            if (SceneManager.GetActiveScene().name == "2_HallwayDoctors")
            {

                if (hit.collider != null && hit.collider.CompareTag("RightHand"))
                {
                    PlayerPositionManager.Instance.EnterFromLeft();
                    continueDialogue.TriggerWaitingAreaTransition();
                    Debug.Log("clicked right hand. to waiting area");

                }


                if (hit.collider != null && hit.collider.CompareTag("LeftHand"))
                {
                    flowchart.ExecuteBlock("doctors office door locked");
                    PlayerPositionManager.Instance.EnterFromRight();

                    /*Debug.Log("clicked left hand. ");
                    continueDialogue.TriggerDoctorsOfficeTransition();*/
                }
            }

            if (SceneManager.GetActiveScene().name == "3_WaitingArea")
            {

                if (hit.collider != null && hit.collider.CompareTag("RightHand"))
                {
                    PlayerPositionManager.Instance.EnterFromLeft();
                    Debug.Log("clicked right hand. to canteen");
                    continueDialogue.TriggerCanteenTransition();
                }

                if (hit.collider != null && hit.collider.CompareTag("LeftHand"))
                {
                    PlayerPositionManager.Instance.EnterFromRight();
                    Debug.Log("clicked left hand. to hallwaydoctors");
                    continueDialogue.TriggerHallwayDoctorsTransition();
                }
            }

            if (SceneManager.GetActiveScene().name == "4_Canteen")
            {
                if (hit.collider != null && hit.collider.CompareTag("LeftHand"))
                {
                    PlayerPositionManager.Instance.EnterFromRight();
                    Debug.Log("clicked left hand. to waiting area");
                    continueDialogue.TriggerWaitingAreaTransition();
                }

                if (hit.collider != null && hit.collider.CompareTag("RightHand"))
                {
                    Debug.Log("clicked right hand. to dorms");
                    PlayerPositionManager.Instance.EnterFromLeft();
                    continueDialogue.TriggerHallwayDormTransition();
                }
            }

            if (SceneManager.GetActiveScene().name == "5_HallwayDorm")
            {
                if (hit.collider != null && hit.collider.CompareTag("LeftHand"))
                {
                    PlayerPositionManager.Instance.EnterFromRight();
                    Debug.Log("clicked left hand. ");
                    continueDialogue.TriggerCanteenTransition();
                }

                if (hit.collider != null && hit.collider.CompareTag("RightHand"))
                {
                    PlayerPositionManager.Instance.EnterFromLeft();
                    Debug.Log("clicked right hand. to janitors closet ");
                    continueDialogue.TriggerJanitorsClosetTransition(); //
                }
            }

            if (SceneManager.GetActiveScene().name == "6_Janitors")
            {

                if (hit.collider != null && hit.collider.CompareTag("LeftHand"))
                {
                    PlayerPositionManager.Instance.EnterFromRight();
                    Debug.Log("clicked right hand. to dorms ");
                    continueDialogue.TriggerHallwayDormTransition(); 
                }
            }
        }
    }
}
