using System.Collections.Generic;
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

        foreach (string item in inventory)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryUI);
            slot.GetComponentInChildren<Text>().text = item; 
        }
    }
}
