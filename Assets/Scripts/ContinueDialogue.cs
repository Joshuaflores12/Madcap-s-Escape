using UnityEngine;
using Fungus;

public class ContinueDialogue : MonoBehaviour
{
    [SerializeField] private Flowchart flowchart;
    [SerializeField] private string blockName;  
    [SerializeField] private Transform checkpoint;
    [SerializeField] private string blockName2; 
    [SerializeField] private Transform checkpoint2;
    [SerializeField] private string requiredTag = "door";

    private Transform player;
    private bool hasTriggeredFirstCheckpoint = false;
    private bool hasTriggeredSecondCheckpoint = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (!hasTriggeredFirstCheckpoint && PlayerHasPassedCheckpoint(checkpoint))
        {
            hasTriggeredFirstCheckpoint = true;
            ExecuteFungusBlock(blockName);
        }

        if (!hasTriggeredSecondCheckpoint && PlayerHasPassedCheckpoint(checkpoint2))
        {
            hasTriggeredSecondCheckpoint = true;
            ExecuteFungusBlock(blockName2);
        }

        if (Input.GetMouseButtonDown(0) && PlayerHasThreeKeys())
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag(requiredTag))
            {
                Debug.Log("Door clicked with 3 keys. Executing dialogue block.");
                ExecuteFungusBlock("DoneTutorial (Copy) (Copy) (Copy) (Copy) (Copy) (Copy)");
            }
        }
    }

    private bool PlayerHasPassedCheckpoint(Transform checkpointTransform)
    {
        return player != null && checkpointTransform != null && player.position.x > checkpointTransform.position.x;
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
            int keyCount = NotebookInventory.Instance.CountItems("Key");
            return keyCount >= 3;
        }
        return false;
    }
}
