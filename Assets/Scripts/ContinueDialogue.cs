using UnityEngine;
using Fungus;
using UnityEngine.SceneManagement;

public class ContinueDialogue : MonoBehaviour
{
    [SerializeField] private Flowchart flowchart;
    [SerializeField] private string blockName;
    [SerializeField] private Transform checkpoint;
    [SerializeField] private string blockName2;
    [SerializeField] private Transform checkpoint2;
    [SerializeField] private string requiredTag = "door";

    [SerializeField] private Transform toCanteen;
    [SerializeField] private Transform toDoctors;
    [SerializeField] private Transform toHallwayLeft;
    [SerializeField] private Transform toHallwayRight;

    [SerializeField] private ClownMask clownMask;
    private Transform player;
    private bool hasTriggeredFirstCheckpoint = false;
    private bool hasTriggeredSecondCheckpoint = false;
    private bool isBackToIsolation = true;
    [SerializeField] bool hasTriggeredCanteenTransition = false;
    [SerializeField] bool hasTriggeredHallwayTransition = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "1_IsolationChamber")
        {
/*            if (!hasTriggeredFirstCheckpoint && PlayerHasPassedCheckpoint(checkpoint) && clownMask.ManicStateIsolationBG.activeSelf)
            {
                hasTriggeredFirstCheckpoint = true;
                ExecuteFungusBlock(blockName);
            }*/

            if (PlayerHasPassedCheckpoint(checkpoint2) && isBackToIsolation == true)
            {
                if (!hasTriggeredSecondCheckpoint)
                {
                    hasTriggeredSecondCheckpoint = true;
                    ExecuteFungusBlock(blockName2);
                }
            }
            else
            {
                hasTriggeredSecondCheckpoint = false;
            }

            if (Input.GetMouseButtonDown(0) && PlayerHasThreeKeys())
            {
                Debug.Log("clicked door");
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                if (hit.collider != null && hit.collider.CompareTag(requiredTag))
                {
                    Debug.Log("Door clicked with 3 keys. Executing dialogue block.");
                    ExecuteFungusBlock("DoneTutorial (Copy) (Copy) (Copy) (Copy) (Copy) (Copy)");
                }
            }

/*            if (!hasTriggeredCanteenTransition && PlayerHasPassedCheckpoint(toCanteen))
            {
                hasTriggeredCanteenTransition = true;
                TriggerCanteenTransition();
            }*/
        }

/*        if (SceneManager.GetActiveScene().name == "2_CanteenDorm") 
        {
            if (!hasTriggeredHallwayTransition && PlayerHasPassedCheckpoint(toHallwayLeft))
            {
                hasTriggeredHallwayTransition = true;
                TriggerHallwayLeftTransition();
            }
            if (!hasTriggeredHallwayTransition && PlayerHasPassedCheckpoint(toHallwayRight))
            {
                hasTriggeredHallwayTransition = true;
                TriggerHallwayDormTransition();
            }
        }

        if (SceneManager.GetActiveScene().name == "HallwayDorm")
        {
            if (!hasTriggeredCanteenTransition && PlayerHasPassedCheckpoint(toCanteen))
            {
                hasTriggeredCanteenTransition = true;
                TriggerCanteenTransition();
            }

            if (!hasTriggeredCanteenTransition && PlayerHasPassedCheckpoint(toDoctors))
            {
                hasTriggeredCanteenTransition = true;
                TriggerDoctorsOfficeTransition();
            }
        }

        if (SceneManager.GetActiveScene().name == "HallwayLeft")
        {
            if (!hasTriggeredCanteenTransition && PlayerHasPassedCheckpoint(toCanteen))
            {
                hasTriggeredCanteenTransition = true;
                TriggerCanteenTransition();
            }

        }*/





    }

    private bool PlayerHasPassedCheckpoint(Transform checkpointTransform)
    {
        return player != null && checkpointTransform != null &&
               Mathf.Abs(player.position.x - checkpointTransform.position.x) < 0.1f ||
               (player.position.x > checkpointTransform.position.x && checkpointTransform.position.x >= 0) || 
               (player.position.x < checkpointTransform.position.x && checkpointTransform.position.x < 0);  
    }

    private void ExecuteFungusBlock(string block)
    {
        if (flowchart != null && !string.IsNullOrEmpty(block))
        {
            flowchart.ExecuteBlock(block);
        }
    }

    private bool PlayerHasThreeKeys()
    {
        if (NotebookInventory.Instance != null)
        {
            int keyCount = NotebookInventory.Instance.CountItems("Door Key");
            return keyCount >= 1;
        }
        return false;
    }

    public void setTriggerCheckpointBoolean()
    {
        isBackToIsolation = true;
    }
    public void setTriggerCheckpointBooleanFalse()
    {
        isBackToIsolation = false;
    }


    
    public void TriggerHallwayDoctorsTransition()
    {
        SwitchScene switchScene = FindObjectOfType<SwitchScene>();
        if (switchScene != null)
        {
            switchScene.SwitchSceneToHallwayDoctors();
        }
    }

    public void TriggerWaitingAreaTransition()
    {
        SwitchScene switchScene = FindObjectOfType<SwitchScene>();
        if (switchScene != null)
        {
            switchScene.SwitchSceneToWaitingArea();
        }
    }
    public void TriggerCanteenTransition()
    {
        SwitchScene switchScene = FindObjectOfType<SwitchScene>();
        if (switchScene != null)
        {
            switchScene.SwitchSceneToCanteen();
        }
    }
    public void TriggerHallwayDormTransition()
    {
        SwitchScene switchScene = FindObjectOfType<SwitchScene>();
        if (switchScene != null)
        {
            switchScene.SwitchSceneToHallwayDorm();
        }
    }

    public void TriggerJanitorsClosetTransition()
    {
        SwitchScene switchScene = FindObjectOfType<SwitchScene>();
        if (switchScene != null)
        {
            switchScene.SwitchSceneToJanitorsCloset();
        }
    }

    public void TriggerDoctorsOfficeTransition()
    {
        SwitchScene switchScene = FindObjectOfType<SwitchScene>();
        if (switchScene != null)
        {
            switchScene.SwitchSceneToDoctorsOffice();
        }
    }
}
