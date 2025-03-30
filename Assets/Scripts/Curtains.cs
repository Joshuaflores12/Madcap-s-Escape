using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using Unity.VisualScripting;

public class Curtains : MonoBehaviour
{
    [SerializeField] private GameObject curtainLeft;
    [SerializeField] private GameObject curtainRight;
    [SerializeField] private GameObject CollectablesUI;
    [SerializeField] private Transform endCurtainLeft;
    [SerializeField] private Transform endCurtainRight;
    [SerializeField] private Transform startCurtainLeft;
    [SerializeField] private Transform startCurtainRight;
    [SerializeField] private float Speed = 3f;
    [SerializeField] private bool isOpening = false;
    [SerializeField] private bool isClosing = false;
    [SerializeField] private Flowchart flowchart;
    [SerializeField] private string blockToExecute;

    private bool hasExecutedBlock = false;

    void Update()
    {
        if (isOpening)
        {
            MoveCurtains(curtainLeft, endCurtainLeft, curtainRight, endCurtainRight, ref isOpening);
        }

        if (isClosing)
        {
            MoveCurtains(curtainLeft, startCurtainLeft, curtainRight, startCurtainRight, ref isClosing);
        }
    }

    private void MoveCurtains(GameObject left, Transform leftTarget, GameObject right, Transform rightTarget, ref bool state)
    {
        left.transform.position = Vector2.MoveTowards(left.transform.position, leftTarget.position, Speed * Time.deltaTime);
        right.transform.position = Vector2.MoveTowards(right.transform.position, rightTarget.position, Speed * Time.deltaTime);

        // Using a slightly larger threshold to avoid precision errors
        if (Vector3.Distance(left.transform.position, leftTarget.position) < 0.05f &&
            Vector3.Distance(right.transform.position, rightTarget.position) < 0.05f)
        {
            state = false;
            left.transform.position = leftTarget.position;  // Ensure exact positioning
            right.transform.position = rightTarget.position;
        }
    }

    public void OpenCurtains()
    {
        isOpening = true;
        isClosing = false;  // Ensure it doesn’t conflict with closing
        hasExecutedBlock = false;
        StartCoroutine(ExecuteFungusBlockAfterDelay(4f));
        
    }

    public void CloseCurtains()
    {
        isClosing = true;
        isOpening = false;  // Prevent opening and closing at the same time
        Debug.Log("Curtains closing");
    }

    private IEnumerator ExecuteFungusBlockAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!hasExecutedBlock && flowchart != null && !string.IsNullOrEmpty(blockToExecute))
        {
            flowchart.ExecuteBlock(blockToExecute);
            hasExecutedBlock = true;
            Debug.Log("Fungus block executed: " + blockToExecute);
            CollectablesUI.SetActive(true);
        }

    }
    
}
