using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private GameObject letter;
    [SerializeField] private GameObject Patient1Prefab;
    [SerializeField] private GameObject NpcSane;
    [SerializeField] private GameObject NpcManic;
    [SerializeField] private GameObject CollectablesSane;
    [SerializeField] private GameObject CollectablesManic;
    [SerializeField] private TextMeshProUGUI TextUpdater;
    [SerializeField] private Flowchart flowchart;
    [SerializeField] private Button clownMaskButton;
    [SerializeField] private string FoodBlock;
    [SerializeField] private string JuiceBlock;
    [SerializeField] private string Food2Block;
    [SerializeField] private string WaterBlock;
    [SerializeField] private string FlourBlock;
    [SerializeField] private string ClusterOfEggsBlock;
    [SerializeField] private string MilkBlock;
    [SerializeField] private string BloodBlock;
    [SerializeField] private string WiltedFlowerBlock;
    [SerializeField] private string Chapter2IntroBlock;
    [SerializeField] private string HallwayBlock;
    [SerializeField] private string KitchenLadyBlock;
    [SerializeField] private string Patient1_1Block;
    [SerializeField] private string Patient2Block;
    [SerializeField] private string Patient3Block;
    [SerializeField] private string Patient4Block;
    [SerializeField] private string Patient5Block;
    [SerializeField] private string Patient6Block;
    [SerializeField] private string CanteenManicBlock;
    [SerializeField] private string Patient1WhisperingBlock;
    [SerializeField] private string ExitCanteenInteractionBlock;
    [SerializeField] CanteenClownMask clownMask;


    private NotebookInventory notebookInventory;
    private SecondChallenge secondChallenge;
    private bool isExecutingBlock = false;
    private bool hasExecutedCanteenManic;


    void Awake()
    {
        notebookInventory = FindFirstObjectByType<NotebookInventory>();
        secondChallenge = FindFirstObjectByType<SecondChallenge>();
        CanteenClownMask clownMask = FindAnyObjectByType<CanteenClownMask>();

        if (clownMaskButton != null)
        {
            clownMaskButton.interactable = true;
            clownMaskButton.onClick.AddListener(() => StartCoroutine(HandleClownMaskClick()));
            
        }

        
    }

    void Start()
    {
        // Remove any previously collected objects in the scene
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("brokenKey"))
            DestroyIfCollected(obj);
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Food"))
            DestroyIfCollected(obj);
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Water"))
            DestroyIfCollected(obj);
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("CatPlush"))
            DestroyIfCollected(obj);
        // repeat for other collectible tags as needed
        if (SceneManager.GetActiveScene().name == "4_Canteen")
        {
            NpcManic.SetActive(false);

        }
    }
    void DestroyIfCollected(GameObject obj)
    {
        string uniqueID = obj.name; // uses GameObject name
        if (NotebookInventory.Instance.HasItemBeenCollected(uniqueID))
        {
            Destroy(obj);
        }
    }
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "4_Canteen")
        {
            if (flowchart.HasExecutingBlocks())
            {
                return;
            }

            if (clownMask.isManic && !hasExecutedCanteenManic)
            {
                flowchart.ExecuteBlock(CanteenManicBlock);
                hasExecutedCanteenManic = true;
                flowchart.enabled = false;
                TextUpdater.text = "Manic State!";
                Debug.Log("CanteenManicExecutedOnce!");
            }

            if (clownMask.isManic)
            {
                NpcManic.SetActive(true);
                NpcSane.SetActive(false);
                CollectablesSane.SetActive(false);
                CollectablesManic.SetActive(true);
            }

            if (!clownMask.isManic)
            {
                NpcManic.SetActive(false);
                TextUpdater.text = string.Empty;
                NpcSane.SetActive(true);
                CollectablesSane.SetActive(true);
                CollectablesManic.SetActive(false);
            }
        }
            

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);


            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("candle") || hit.collider.CompareTag("bobbyPin") || hit.collider.CompareTag("pingPongBall") || hit.collider.CompareTag("brokenKey") || hit.collider.CompareTag("Food") || hit.collider.CompareTag("OddColoredJuice") || hit.collider.CompareTag("Food2") || hit.collider.CompareTag("Water") || hit.collider.CompareTag("paperClip")
                    || hit.collider.CompareTag("Flour") || hit.collider.CompareTag("ClusterOfEggs") || hit.collider.CompareTag("Milk") || hit.collider.CompareTag("Blood") || hit.collider.CompareTag("WiltedFlower"))
                {
                    Debug.Log("Picked up: " + hit.collider.gameObject.name);
                    string itemTag = hit.collider.tag;
                    string uniqueID = hit.collider.tag + "_" + hit.collider.gameObject.name;

                    NotebookInventory.Instance.MarkItemCollected(uniqueID); 
                    NotebookInventory.Instance.AddToInventory(hit.collider.tag); 
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

                if (hit.collider.CompareTag("Flour")) 
                {
                    flowchart.ExecuteBlock(FlourBlock);
                    Destroy(hit.collider.gameObject);
                    TextUpdater.text = "Flour!";
                }
                
                if (hit.collider.CompareTag("ClusterOfEggs")) 
                {
                    flowchart.ExecuteBlock(ClusterOfEggsBlock);
                    Destroy(hit.collider.gameObject);
                    TextUpdater.text = "Eggs!";
                }
                
                if (hit.collider.CompareTag("Milk")) 
                {
                    flowchart.ExecuteBlock(MilkBlock);
                    Destroy(hit.collider.gameObject);
                    TextUpdater.text = "Milk!";
                }
                
                if (hit.collider.CompareTag("Blood")) 
                {
                    flowchart.ExecuteBlock(BloodBlock);
                    Destroy(hit.collider.gameObject);
                    TextUpdater.text = "Blood!";
                }
                
                if (hit.collider.CompareTag("WiltedFlower")) 
                {
                    flowchart.ExecuteBlock(WiltedFlowerBlock);
                    Destroy(hit.collider.gameObject);
                    TextUpdater.text = "Wilted Flower!";
                }

                if (hit.collider.CompareTag("Patient1"))
                {
                    flowchart.ExecuteBlock(Chapter2IntroBlock);
                    Destroy(hit.collider.gameObject);
                    Patient1Prefab.SetActive(true);
                    TextUpdater.text = "Patient 1";

                }

                if (hit.collider.CompareTag("Patient1.1"))
                {
                    flowchart.ExecuteBlock(Patient1_1Block);
                    TextUpdater.text = "Patient 1";
                }

                if (hit.collider.CompareTag("CanteenDoor"))
                {
                    flowchart.ExecuteBlock(HallwayBlock);
                    Destroy(hit.collider.gameObject);
                    TextUpdater.text = "Hallway Door";
                }

                if (hit.collider.CompareTag("KitchenLady"))
                {
                    flowchart.ExecuteBlock(KitchenLadyBlock);
                    TextUpdater.text = "Kitchen Lady";
                }

                if (hit.collider.CompareTag("Patient2"))
                {
                    flowchart.ExecuteBlock(Patient2Block);
                    TextUpdater.text = "Patient 2";
                }

                if (hit.collider.CompareTag("Patient3"))
                {
                    flowchart.ExecuteBlock(Patient3Block);
                    TextUpdater.text = "Patient 3";
                }

                if (hit.collider.CompareTag("Patient4"))
                {
                    flowchart.ExecuteBlock(Patient4Block);
                    TextUpdater.text = "Patient 4";
                }

                if (hit.collider.CompareTag("Patient5"))
                {
                    flowchart.ExecuteBlock(Patient5Block);
                    TextUpdater.text = "Patient 5";             
                }

                if (hit.collider.CompareTag("Patient6"))
                {
                    flowchart.ExecuteBlock(Patient6Block);
                    TextUpdater.text = "Patient 6";
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



    public void AddCatPlushieToInventory()
    {
        string itemTag = "CatPlush";
        string uniqueID = itemTag + "newCatPlush";

        if (!NotebookInventory.Instance.HasItemBeenCollected(uniqueID))
        {
            NotebookInventory.Instance.MarkItemCollected(uniqueID);
            NotebookInventory.Instance.AddToInventory(itemTag);
            Debug.Log("Cat Plushie added to inventory!");

            GameObject catPlushieObject = GameObject.FindWithTag(itemTag);
            if (catPlushieObject != null)
            {
                Destroy(catPlushieObject);
                Debug.Log("Cat Plushie GameObject destroyed.");
            }
        }
        else
        {
            Debug.Log("Cat Plushie already collected.");
        }
    }

    public void AddBobbyPinToInventory()
    {
        string itemTag = "bobbyPin";
        string uniqueID = itemTag + "newBobbyPin";

        if (!NotebookInventory.Instance.HasItemBeenCollected(uniqueID))
        {
            NotebookInventory.Instance.MarkItemCollected(uniqueID);
            NotebookInventory.Instance.AddToInventory(itemTag);
            Debug.Log("bobby pin added to inventory!");

            GameObject bobbyPinObject = GameObject.FindWithTag(itemTag);
            if (bobbyPinObject != null)
            {
                Destroy(bobbyPinObject);
                Debug.Log("bobby pin GameObject destroyed.");
            }
        }
        else
        {
            Debug.Log("bobby pin already collected.");
        }
    }

    public void AddPaperClipToInventory()
    {
        string itemTag = "paperClip";
        string uniqueID = itemTag + "newPaperClip";

        if (!NotebookInventory.Instance.HasItemBeenCollected(uniqueID))
        {
            NotebookInventory.Instance.MarkItemCollected(uniqueID);
            NotebookInventory.Instance.AddToInventory(itemTag);
            Debug.Log("paper clip added to inventory!");

            GameObject paperClipObject = GameObject.FindWithTag(itemTag);
            if (paperClipObject != null)
            {
                Destroy(paperClipObject);
                Debug.Log("paper clip GameObject destroyed.");
            }
        }
        else
        {
            Debug.Log("paper clip already collected.");
        }
    }

    public void AddBatteryToInventory()
    {
        string itemTag = "battery";
        string uniqueID = itemTag + "newBattery";

        if (!NotebookInventory.Instance.HasItemBeenCollected(uniqueID))
        {
            NotebookInventory.Instance.MarkItemCollected(uniqueID);
            NotebookInventory.Instance.AddToInventory(itemTag);
            Debug.Log("Battery added to inventory!");

            GameObject batteryObject = GameObject.FindWithTag(itemTag);
            if (batteryObject != null)
            {
                Destroy(batteryObject);
                Debug.Log("bBattery GameObject destroyed.");
            }
        }
        else
        {
            Debug.Log("Battery already collected.");
        }
    }

    public void AddTicketToInventory()
    {
        string itemTag = "ticket";
        string uniqueID = itemTag + "newticket";

        if (!NotebookInventory.Instance.HasItemBeenCollected(uniqueID))
        {
            NotebookInventory.Instance.MarkItemCollected(uniqueID);
            NotebookInventory.Instance.AddToInventory(itemTag);
            Debug.Log("ticket added to inventory!");

            GameObject ticketObject = GameObject.FindWithTag(itemTag);
            if (ticketObject != null)
            {
                Destroy(ticketObject);
                Debug.Log("ticket GameObject destroyed.");
            }
        }
        else
        {
            Debug.Log("ticket already collected.");
        }
    }

    public void AddNailToInventory()
    {
        string itemTag = "nail";
        string uniqueID = itemTag + "newNail";

        if (!NotebookInventory.Instance.HasItemBeenCollected(uniqueID))
        {
            NotebookInventory.Instance.MarkItemCollected(uniqueID);
            NotebookInventory.Instance.AddToInventory(itemTag);
            Debug.Log("nail added to inventory!");

            GameObject nailObject = GameObject.FindWithTag(itemTag);
            if (nailObject != null)
            {
                Destroy(nailObject);
                Debug.Log("nail GameObject destroyed.");
            }
        }
        else
        {
            Debug.Log("nail already collected.");
        }
    }
    public void AddFoilToInventory()
    {
        string itemTag = "foil";
        string uniqueID = itemTag + "newFoil";

        if (!NotebookInventory.Instance.HasItemBeenCollected(uniqueID))
        {
            NotebookInventory.Instance.MarkItemCollected(uniqueID);
            NotebookInventory.Instance.AddToInventory(itemTag);
            Debug.Log("foil added to inventory!");

            GameObject foilObject = GameObject.FindWithTag(itemTag);
            if (foilObject != null)
            {
                Destroy(foilObject);
                Debug.Log("foil GameObject destroyed.");
            }
        }
        else
        {
            Debug.Log("foil already collected.");
        }
    }

    public void AddPapersToInventory()
    {
        string itemTag = "papers";
        string uniqueID = itemTag + "newPapers";

        if (!NotebookInventory.Instance.HasItemBeenCollected(uniqueID))
        {
            NotebookInventory.Instance.MarkItemCollected(uniqueID);
            NotebookInventory.Instance.AddToInventory(itemTag);
            Debug.Log("papers added to inventory!");

            GameObject papersObject = GameObject.FindWithTag(itemTag);
            if (papersObject != null)
            {
                Destroy(papersObject);
                Debug.Log("papers GameObject destroyed.");
            }
        }
        else
        {
            Debug.Log("papers already collected.");
        }
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
    {
        if (clownMask.isManic && collision.gameObject.tag == "NurseBoundary")
        {
            flowchart.ExecuteBlock(ExitCanteenInteractionBlock);
            TextUpdater.text = "Nurse is blocking your path!";
        }
    }

    private void OnCollisionExit2D(UnityEngine.Collision2D collision)
    {
        if (clownMask.isManic && collision.gameObject.tag == "NurseBoundary") 
        {
            TextUpdater.text = string.Empty;
        }
    }
}
