using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotebookInventory : MonoBehaviour
{
    public static NotebookInventory Instance;  
    [SerializeField] private List<string> inventory = new List<string>();
    [SerializeField] private Transform inventoryUI;
    [SerializeField] private GameObject inventorySlotPrefab; 


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddToInventory(string itemTag)
    {
        inventory.Add(itemTag);  
        UpdateInventoryUI();
    }

    void UpdateInventoryUI()
    {
        foreach (Transform child in inventoryUI)
        {
            Destroy(child.gameObject);
        }
        float xOffset = 200f;

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
        }
    }
}
