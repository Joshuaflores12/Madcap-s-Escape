using System.Collections;
using UnityEngine;
using Fungus;

public class NurseComingIn : MonoBehaviour
{
    [SerializeField] private Flowchart flowchart;
    [SerializeField] private string fungusBlockName = "Narration 2";
    [SerializeField] private GameObject nurse;
    [SerializeField] private Transform originalPosition;
    [SerializeField] private float moveDuration = 2f;

    private bool isComing = false;

    void Update()
    {
        if (!isComing && flowchart.GetExecutingBlocks().Exists(block => block.BlockName == fungusBlockName))
        {
            StartCoroutine(MoveNurseBack());
            isComing = true;
        }
    }

    private IEnumerator MoveNurseBack()
    {
        Vector3 startPosition = nurse.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            nurse.transform.position = Vector3.Lerp(startPosition, originalPosition.position, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        nurse.transform.position = originalPosition.position;
        isComing = false; 
    }
}
