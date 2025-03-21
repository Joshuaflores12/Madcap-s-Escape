using UnityEngine;
using System.Collections;

public class FungusManager : MonoBehaviour
{
    [SerializeField] GameObject slider;
    [SerializeField] GameObject weakpoints;
    [SerializeField] GameObject instruction;
    [SerializeField] GameObject letter;
    [SerializeField] GameObject nurse;
    [SerializeField] Vector3 nurseTargetPosition;
    [SerializeField] float nurseLerpSpeed = 0.09f;

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

    public void NurseIsLeaving()
    {
        Debug.Log("nurse leaving");
        StartCoroutine(MoveAndHideNurse());
    }

    private IEnumerator MoveAndHideNurse()
    {
        float timeElapsed = 0f;
        Vector3 startPos = nurse.transform.position;
        while (timeElapsed < 8f)
        {
            nurse.transform.position = Vector3.Lerp(startPos, nurseTargetPosition, timeElapsed * nurseLerpSpeed);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        nurse.transform.position = nurseTargetPosition;
        nurse.SetActive(false);
    }
}