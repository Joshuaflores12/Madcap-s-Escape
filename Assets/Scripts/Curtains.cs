using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtains : MonoBehaviour
{
    [SerializeField] public GameObject curtainLeft;
    [SerializeField] public GameObject curtainRight;
    [SerializeField] public Transform  endCurtainLeft;
    [SerializeField] public Transform  endCurtainRight;
    [SerializeField] private float Speed = 5f;
    [SerializeField] private bool isOpening = false;
    

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
    }
}
