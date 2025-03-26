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
    [SerializeField] private GameObject nurse;
    [SerializeField] private Vector3 nurseStartPosition;
    [SerializeField] private Vector3 nurseTargetPosition;
    [SerializeField] private float nurseLerpSpeed = 0.09f;

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
    public void HideLetter()
    {
        letter.SetActive(false);
        openedletterScreen.SetActive(false);
        Debug.Log("hide letter");
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
            nurse.transform.position = Vector3.Lerp(startPos, targetPosition, timeElapsed * nurseLerpSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        nurse.transform.position = targetPosition;
        if (targetPosition == nurseTargetPosition)
        {
            nurse.SetActive(false);
        }
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
