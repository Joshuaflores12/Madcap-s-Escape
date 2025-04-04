using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NotebookInventory : MonoBehaviour
{
    public static NotebookInventory Instance;  
    [SerializeField] private List<string> inventory = new List<string>();
    [SerializeField] private Transform inventoryUI;
    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] public GameObject notebook;
    [SerializeField] public SecondChallenge secondChallenge;

    [SerializeField] private string[] allowedSceneNames = { "2_CanteenDorm", "3_Dorm", "4_Doctors" };


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Update()
    {
        if (secondChallenge.isSecondChallengeCompleted == true || IsSceneAllowed())
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                notebook.SetActive(!notebook.activeSelf);
            }
        }
    }

    private bool IsSceneAllowed()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        foreach (string scene in allowedSceneNames)
        {
            if (currentScene == scene)
            {
                return true;
            }
        }
        return false;
    }

    public void OpenNotebook()
    {
        bool isActive = !notebook.activeSelf;
        notebook.SetActive(isActive);

        foreach (Transform child in notebook.transform)
        {
            child.gameObject.SetActive(isActive);
        }


    }

    public GameObject AddToInventory(string itemTag)
    {
        inventory.Add(itemTag);
        return UpdateInventoryUI();
    }

    GameObject UpdateInventoryUI()
    {
        foreach (Transform child in inventoryUI)
        {
            Destroy(child.gameObject);
        }

        float xOffset = 200f;
        GameObject lastSlot = null;

        for (int i = 0; i < inventory.Count; i++)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryUI);
            TMP_Text slotText = slot.GetComponentInChildren<TMP_Text>();

            if (slotText != null)
                slotText.text = inventory[i];

            RectTransform slotRect = slot.GetComponent<RectTransform>();
            if (slotRect != null)
            {
                slotRect.anchoredPosition = new Vector2(xOffset * i, 0);
            }

            lastSlot = slot; // Store the last created slot
        }

        return lastSlot; // Return the most recently created inventory slot
    }

    public int CountItems(string itemTag)
    {
        int count = 0;
        foreach (string item in inventory)
        {
            if (item == itemTag)
            {
                count++;
            }
        }
        return count;
    }
}
