using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Curtains : MonoBehaviour
{
    [SerializeField] public GameObject curtainLeft;
    [SerializeField] public GameObject curtainRight;
    [SerializeField] public Transform  endCurtainLeft;
    [SerializeField] public Transform  endCurtainRight;
    [SerializeField] private float Speed = 5f;
    [SerializeField] private bool isOpening = false;
    [SerializeField] private Flowchart flowchart;
    private bool hasExecutedBlock = false;


    void Update() 
    {
        if (isOpening) 
        {
            curtainLeft.transform.position = Vector2.MoveTowards(curtainLeft.transform.position, endCurtainLeft.transform.position, Speed * Time.deltaTime);

            curtainRight.transform.position = Vector2.MoveTowards(curtainRight.transform.position, endCurtainRight.transform.position, Speed * Time.deltaTime);

        }

        if (Vector3.Distance(curtainLeft.transform.position, endCurtainLeft.position) < 0.01f &&
            Vector3.Distance(curtainRight.transform.position, endCurtainRight.position) < 0.01f)
        {
            isOpening = false;
        }

    }

    public void OpenCurtains() 
    {
        isOpening = true;

        hasExecutedBlock = false; 
        StartCoroutine(ExecuteFungusBlockAfterDelay(6f)); /// 6f
    }

    private IEnumerator ExecuteFungusBlockAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); 

        if (!hasExecutedBlock && flowchart != null)
        {
            flowchart.ExecuteBlock("FirstBlock"); ////firstBlock
            hasExecutedBlock = true;
            Debug.Log("Fungus block executed after 10 seconds.");
        }
    }
}
