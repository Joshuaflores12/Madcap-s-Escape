using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.Rendering;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] GameObject letter;
    [SerializeField] private Flowchart flowchart;
    private NotebookInventory notebookInventory;
    private SecondChallenge secondChallenge;
    private bool hasExecutedBlock = false;

    void Awake()
    {
        notebookInventory = FindFirstObjectByType<NotebookInventory>();
        secondChallenge = FindFirstObjectByType<SecondChallenge>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))  
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("candle") || hit.collider.CompareTag("bobbyPin") || hit.collider.CompareTag("pingPongBall"))
                {
                    Debug.Log("Picked up: " + hit.collider.gameObject.name);
                    string itemTag = hit.collider.tag;
                    NotebookInventory.Instance.AddToInventory(itemTag);

                    Destroy(hit.collider.gameObject);
                }

                if (secondChallenge.isSecondChallengeCompleted == true)
                {
                    if (hit.collider.CompareTag("letter"))
                    {
                        Debug.Log("opende letter");
                        letter.SetActive(true);
                        StartCoroutine(ExecuteFungusBlockAfterDelay(2f));
                    }
                }

            }
        }
    }
    private IEnumerator ExecuteFungusBlockAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!hasExecutedBlock && flowchart != null)
        {
            flowchart.ExecuteBlock("DoneTutorial");
            hasExecutedBlock = true;
        }
    }
}
