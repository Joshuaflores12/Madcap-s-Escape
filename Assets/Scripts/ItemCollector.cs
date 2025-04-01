using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private GameObject letter;
    [SerializeField] GameObject NpcHoverUI;
    [SerializeField] private Flowchart flowchart;
    [SerializeField] private Button clownMaskButton;
    [SerializeField] private string FoodBlock;
    [SerializeField] private string JuiceBlock;
    [SerializeField] private string Food2Block;
    [SerializeField] private string WaterBlock;
    [SerializeField] private string Chapter2IntroBlock;
    

    private NotebookInventory notebookInventory;
    private SecondChallenge secondChallenge;
    private bool isExecutingBlock = false;
    [SerializeField] bool isExecutingFoodBlock = false;
    [SerializeField] bool isExecutingJuiceBlock = false;
    [SerializeField] bool isExecutingFood2Block = false;
    [SerializeField] bool isExecutingWaterBlock = false;


    void Awake()
    {
        notebookInventory = FindFirstObjectByType<NotebookInventory>();
        secondChallenge = FindFirstObjectByType<SecondChallenge>();

        if (clownMaskButton != null)
        {
            clownMaskButton.interactable = true;
            clownMaskButton.onClick.AddListener(() => StartCoroutine(HandleClownMaskClick()));
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("candle") || hit.collider.CompareTag("bobbyPin") || hit.collider.CompareTag("pingPongBall") || hit.collider.CompareTag("Key"))
                {
                    Debug.Log("Picked up: " + hit.collider.gameObject.name);
                    string itemTag = hit.collider.tag;
                    NotebookInventory.Instance.AddToInventory(itemTag);
                    Destroy(hit.collider.gameObject);
                }

                if (hit.collider.CompareTag("Food")) 
                {
                    flowchart.ExecuteBlock(FoodBlock);
                    isExecutingFoodBlock = true;
                    Destroy(hit.collider.gameObject);
                }

                if (hit.collider.CompareTag("OddColoredJuice"))
                {
                    flowchart.ExecuteBlock(JuiceBlock);
                    isExecutingJuiceBlock = true;
                    Destroy(hit.collider.gameObject);
                }

                if (hit.collider.CompareTag("Food2"))
                {
                    flowchart.ExecuteBlock(Food2Block);
                    isExecutingFood2Block = true;
                    Destroy(hit.collider.gameObject);
                }

                if (hit.collider.CompareTag("Water"))
                {
                    flowchart.ExecuteBlock(WaterBlock);
                    isExecutingWaterBlock = true;
                    Destroy(hit.collider.gameObject);
                }

                if (hit.collider.CompareTag("Patient1"))
                {
                    flowchart.ExecuteBlock(Chapter2IntroBlock);
                    NpcHoverUI.SetActive(false);
                    Destroy(hit.collider.gameObject);
                }

                if (secondChallenge.isSecondChallengeCompleted)
                {
                    if (hit.collider.CompareTag("letter"))
                    {
                        Debug.Log("Opened letter");
                        letter.SetActive(true);
                        StartCoroutine(ExecuteFungusBlock("DoneTutorial"));
                    }
                }
            }
        }
    }

    private IEnumerator HandleClownMaskClick()
    {
        if (isExecutingBlock || flowchart.HasExecutingBlocks()) yield break; 

        isExecutingBlock = true;
        clownMaskButton.interactable = false; 

        Debug.Log("Clown mask clicked");
        flowchart.ExecuteBlock("ClickedMask");

        yield return new WaitUntil(() => !flowchart.HasExecutingBlocks()); 

        clownMaskButton.interactable = true; 
        isExecutingBlock = false;
    }

    private IEnumerator ExecuteFungusBlock(string blockName)
    {
        if (isExecutingBlock || flowchart.HasExecutingBlocks()) yield break;

        isExecutingBlock = true;
        flowchart.ExecuteBlock(blockName);

        yield return new WaitUntil(() => !flowchart.HasExecutingBlocks());

        isExecutingBlock = false;
    }
}
