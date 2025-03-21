using System.Collections;
using UnityEngine;
using Fungus;

public class NurseLeaving : MonoBehaviour
{
    [SerializeField] private Flowchart flowchart;
    [SerializeField] private string fungusBlockName = "Narration 4";
    [SerializeField] private GameObject nurse;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float moveDuration = 2f;

    private bool hasStartedLeaving = false;

    void Update()
    {
        if (!hasStartedLeaving && flowchart.GetExecutingBlocks().Count == 0 && flowchart.GetBooleanVariable("NurseHasLeft"))
        {
            StartCoroutine(MoveAndHideNurse());
            hasStartedLeaving = true;
        }
    }

    private IEnumerator MoveAndHideNurse()
    {
        Vector3 startPosition = nurse.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            nurse.transform.position = Vector3.Lerp(startPosition, targetPosition.position, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        nurse.transform.position = targetPosition.position;
        nurse.SetActive(false);
    }
}