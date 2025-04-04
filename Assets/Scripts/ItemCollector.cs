using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private GameObject letter;
    [SerializeField] private GameObject Patient1Prefab;
    [SerializeField] private TextMeshProUGUI TextUpdater;
    [SerializeField] private Flowchart flowchart;
    [SerializeField] private Button clownMaskButton;
    [SerializeField] private string FoodBlock;
    [SerializeField] private string JuiceBlock;
    [SerializeField] private string Food2Block;
    [SerializeField] private string WaterBlock;
    [SerializeField] private string Chapter2IntroBlock;
    [SerializeField] private string HallwayBlock;
    [SerializeField] private string KitchenLadyBlock;
    [SerializeField] private string Patient1_1Block;
    [SerializeField] private string Patient2Block;
    [SerializeField] private string Patient3Block;
    [SerializeField] private string Patient4Block;
    [SerializeField] private string Patient5Block;
    [SerializeField] private string Patient6Block;


    private NotebookInventory notebookInventory;
    private SecondChallenge secondChallenge;
    private bool isExecutingBlock = false;


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

        if (flowchart.HasExecutingBlocks())
        {
            return; 
        }


        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);


            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("candle") || hit.collider.CompareTag("bobbyPin") || hit.collider.CompareTag("pingPongBall") || hit.collider.CompareTag("Key") || hit.collider.CompareTag("Food") || hit.collider.CompareTag("OddColoredJuice") || hit.collider.CompareTag("Food2") || hit.collider.CompareTag("Water"))
                {
                    Debug.Log("Picked up: " + hit.collider.gameObject.name);
                    string itemTag = hit.collider.tag;
                    NotebookInventory.Instance.AddToInventory(itemTag);
                    Destroy(hit.collider.gameObject);

                }
                if (hit.collider.CompareTag("Food"))
                {
                    flowchart.ExecuteBlock(FoodBlock);
                    Destroy(hit.collider.gameObject);
                    TextUpdater.text = "Food!";
                }

                if (hit.collider.CompareTag("OddColoredJuice"))
                {
                    flowchart.ExecuteBlock(JuiceBlock);
                    Destroy(hit.collider.gameObject);
                    TextUpdater.text = "Juice!";
                }

                if (hit.collider.CompareTag("Food2"))
                {
                    flowchart.ExecuteBlock(Food2Block);
                    Destroy(hit.collider.gameObject);
                    TextUpdater.text = "Food2!";
                }

                if (hit.collider.CompareTag("Water"))
                {
                    flowchart.ExecuteBlock(WaterBlock);
                    Destroy(hit.collider.gameObject);
                    TextUpdater.text = "Water!";
                }

                if (hit.collider.CompareTag("Patient1"))
                {
                    flowchart.ExecuteBlock(Chapter2IntroBlock);
                    Destroy(hit.collider.gameObject);
                    Patient1Prefab.SetActive(true);
                    TextUpdater.text = "Patient1";
                }

                if (hit.collider.CompareTag("CanteenDoor"))
                {
                    flowchart.ExecuteBlock(HallwayBlock);
                    Destroy(hit.collider.gameObject);
                    TextUpdater.text = "HallwayDoor";
                }

                if (hit.collider.CompareTag("KitchenLady"))
                {
                    flowchart.ExecuteBlock(KitchenLadyBlock);
                    TextUpdater.text = "KitchenLady";
                }

                if (hit.collider.CompareTag("Patient1.1"))
                {
                    flowchart.ExecuteBlock(Patient1_1Block);
                    TextUpdater.text = "Patient1";
                }

                if (hit.collider.CompareTag("Patient2"))
                {
                    flowchart.ExecuteBlock(Patient2Block);
                    TextUpdater.text = "Patient2";
                }

                if (hit.collider.CompareTag("Patient3"))
                {
                    flowchart.ExecuteBlock(Patient3Block);
                    TextUpdater.text = "Patient3";
                }

                if (hit.collider.CompareTag("Patient4"))
                {
                    flowchart.ExecuteBlock(Patient4Block);
                    TextUpdater.text = "Patient4";
                }

                if (hit.collider.CompareTag("Patient5"))
                {
                    flowchart.ExecuteBlock(Patient5Block);
                    TextUpdater.text = "Patient5";             
                }

                if (hit.collider.CompareTag("Patient6"))
                {
                    flowchart.ExecuteBlock(Patient6Block);
                    TextUpdater.text = "Patient6";
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
