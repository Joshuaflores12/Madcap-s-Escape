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
    [SerializeField] private GameObject journalClownMask;
    [SerializeField] private GameObject checkpointContinueDialogue;
    [SerializeField] private Transform nurseStartTransform;
    [SerializeField] private Transform nurseTargetTransform;
    [SerializeField] private Transform guardStartTransform;
    [SerializeField] private Transform guardTargetTransform;
    [SerializeField] private Transform mcTargetTransform;
    [SerializeField] private Transform mcDoorwayTransform;
    [SerializeField] private Transform mcHallwayTransform;
    [SerializeField] private Transform mcIsolationTransform;
    [SerializeField] private float nurseLerpSpeed = 0.09f;
    [SerializeField] private float guardLerpSpeed = 0.09f;
    [SerializeField] private Animator animFadeout;
    [SerializeField] private GameObject fadeoutObject;

    [SerializeField] private Button clownMaskButton;
    [SerializeField] private Flowchart flowchart;

    public void TriggerFadeoutAnimation()
    {
        fadeoutObject.SetActive(true);
        animFadeout.SetBool("FadeOut", true);
    }

    public void FadeoutFalse()
    {
        animFadeout.SetBool("FadeOut", false);
    }

    public void UnhideJournalClownMask()
    {
        journalClownMask.SetActive(true);
    }
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
        StartCoroutine(MoveNurse(nurseStartTransform.position));
    }

    public void NurseIsLeaving()
    {
        Debug.Log("Nurse leaving");
        StartCoroutine(MoveNurse(nurseTargetTransform.position));
    }

    private IEnumerator MoveNurse(Vector3 targetPosition)
    {
        float timeElapsed = 0f;
        Vector3 startPos = nurse.transform.position;

        while (timeElapsed < 8f)
        {
            float moveT = timeElapsed / nurseLerpSpeed;
            nurse.transform.position = Vector3.Lerp(startPos, targetPosition, moveT);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        nurse.transform.position = targetPosition;
    }

    public void GuardDraggingMC()
    {
        Debug.Log("guard dragging mc");
        StartCoroutine(MoveGuard(guardTargetTransform.position));
        StartCoroutine(DelayMCFollow(1f));
    }

    public void GuardReturnToOGPos()
    {
        StartCoroutine(MoveGuard(guardStartTransform.position));
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
        StartCoroutine(MoveMC(mcTargetTransform.position));
    }

    private IEnumerator MoveMC(Vector3 targetPosition)
    {
        float timeElapsed = 0f;
        float duration = 5f; 
        Vector3 startPos = mc.transform.position;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            mc.transform.position = Vector3.Lerp(startPos, targetPosition, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        mc.transform.position = targetPosition;
    }



    public void MCToDoorway()
    {
        Debug.Log("moving mc to door");
        StartCoroutine(MoveMCDoorway(mcDoorwayTransform.position));
    }

    private IEnumerator MoveMCDoorway(Vector3 targetPosition)
    {
        float timeElapsed = 0f;
        Vector3 startPos = mc.transform.position;

        while (timeElapsed < 8f)
        {
            mc.transform.position = Vector3.Lerp(startPos, targetPosition, timeElapsed / 1f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        mc.transform.position = targetPosition;
    }

    public void MCToHallway()
    {
        Debug.Log("moving mc to hallway");
        StartCoroutine(MoveMCHallway(mcHallwayTransform.position));
    }

    private IEnumerator MoveMCHallway(Vector3 targetPosition)
    {
        float timeElapsed = 0f;
        Vector3 startPos = mc.transform.position;

        while (timeElapsed < 8f)
        {
            mc.transform.position = Vector3.Lerp(startPos, targetPosition, timeElapsed / 1f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        mc.transform.position = targetPosition;
    }

    public void MCBackToIsolation()
    {
        Debug.Log("back to isolation");
        StartCoroutine(MoveMCBackIsolation(mcIsolationTransform.position));
    }

    private IEnumerator MoveMCBackIsolation(Vector3 targetPosition)
    {
        float timeElapsed = 0f;
        Vector3 startPos = mc.transform.position;

        while (timeElapsed < 8f)
        {
            mc.transform.position = Vector3.Lerp(startPos, targetPosition, timeElapsed / 1f);
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
