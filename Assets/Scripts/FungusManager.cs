using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Fungus;

public class FungusManager : MonoBehaviour
{
    [SerializeField] private GameObject slider;
    [SerializeField] private GameObject weakpoints;
    [SerializeField] private GameObject instruction;
    [SerializeField] private GameObject letter;
    [SerializeField] private GameObject openedletterScreen;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject nurse;
    [SerializeField] private GameObject guard;
    [SerializeField] private GameObject mc;
    [SerializeField] private GameObject checkpointContinueDialogue;
    [SerializeField] private Vector3 nurseStartPosition;
    [SerializeField] private Vector3 nurseTargetPosition;
    [SerializeField] private Vector3 guardStartPosition;
    [SerializeField] private Vector3 guardTargetPosition;
    [SerializeField] private Vector3 mcTargetPosition;
    [SerializeField] private float nurseLerpSpeed = 0.09f;
    [SerializeField] private float guardLerpSpeed = 0.09f;

    [SerializeField] private Button clownMaskButton;
    [SerializeField] private Flowchart flowchart;

    public void UnhideChallengeObjects()
    {
        slider.SetActive(true);
        weakpoints.SetActive(true);
        instruction.SetActive(true);
        Debug.Log("sobrang latina");
    }

    public void HideChallengeObjects()
    {
        slider.SetActive(false);
        weakpoints.SetActive(false);
        instruction.SetActive(false);
    }

    public void UnhideLetter()
    {
        letter.SetActive(true);
    }

    public void HideDoor()
    {
        door.SetActive(false);
    }
    public void UnhideDoor()
    {
        door.SetActive(true);
    }
    public void HideLetter()
    {
        letter.SetActive(false);
        openedletterScreen.SetActive(false);
        Debug.Log("hide letter");
    }
    public void UnhideFirstCheckpoint()
    {
        checkpointContinueDialogue.SetActive(true);
    }
    public void HideFirstCheckpoint()
    {
        checkpointContinueDialogue.SetActive(false);
    }
    public void NurseIsEntering()
    {
        Debug.Log("Nurse entering");
        StartCoroutine(MoveNurse(nurseStartPosition)); 
    }

    public void NurseIsLeaving()
    {
        Debug.Log("Nurse leaving");
        StartCoroutine(MoveNurse(nurseTargetPosition)); 
    }

    private IEnumerator MoveNurse(Vector3 targetPosition)
    {
        float timeElapsed = 0f;
        Vector3 startPos = nurse.transform.position;
        while (timeElapsed < 8f)
        {
            nurse.transform.position = Vector3.Lerp(startPos, targetPosition, timeElapsed / nurseLerpSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        nurse.transform.position = targetPosition;
        if (targetPosition == nurseTargetPosition)
        {
            nurse.SetActive(false);
        }
    }

    public void GuardDraggingMC()
    {
        Debug.Log("guard dragging mc");
        StartCoroutine(MoveGuard(guardTargetPosition));
        StartCoroutine(DelayMCFollow(1f));
    }

    public void GuardReturnToOGPos()
    {
        StartCoroutine(MoveGuard(guardStartPosition));
    }

    private IEnumerator MoveGuard(Vector3 targetPosition)
    {
        float timeElapsed = 0f;
        Vector3 startPos = guard.transform.position;
        while (timeElapsed < 8f)
        {
            guard.transform.position = Vector3.Lerp(startPos, targetPosition, timeElapsed / guardLerpSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        guard.transform.position = targetPosition;
    }

    private IEnumerator DelayMCFollow(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(MoveMC(mcTargetPosition));
    }

    private IEnumerator MoveMC(Vector3 targetPosition)
    {
        float timeElapsed = 0f;
        Vector3 startPos = mc.transform.position;
        while (timeElapsed < 8f)
        {
            mc.transform.position = Vector3.Lerp(startPos, targetPosition, timeElapsed / guardLerpSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        mc.transform.position = targetPosition;
    }

    public void ExecuteFungusBlock(string blockName)
    {
        if (flowchart != null && clownMaskButton != null)
        {
            clownMaskButton.interactable = false;
            NurseIsEntering(); 
            flowchart.ExecuteBlock(blockName);
            StartCoroutine(WaitForBlockToEnd());
        }
    }

    private IEnumerator WaitForBlockToEnd()
    {
        while (flowchart.HasExecutingBlocks())
        {
            yield return null;
        }
        clownMaskButton.interactable = true;
        NurseIsLeaving(); 
    }
}
