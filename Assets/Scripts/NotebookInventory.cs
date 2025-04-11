using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NotebookInventory : MonoBehaviour
{
    public static NotebookInventory Instance;

    [Header("Inventory System")]
    [SerializeField] private List<string> inventory = new List<string>();
    [SerializeField] private Transform inventoryUI;
    [SerializeField] private GameObject inventorySlotPrefab;
    [SerializeField] private GameObject notebook;
    [SerializeField] private SecondChallenge secondChallenge;
    [SerializeField] private string[] allowedSceneNames = {  "5_HallwayDorm", "4_Canteen", "3_WaitingArea", "2_HallwayDorm" };
    private int currentPage = 0;
    private int itemsPerPage = 8;

    [Header("Combination System")]


    [SerializeField] private List<ItemSpriteData> itemSprites = new List<ItemSpriteData>();
    private Dictionary<string, Sprite> itemSpriteMap = new Dictionary<string, Sprite>();

    [SerializeField] private List<CombinationRecipe> combineRecipes;
    [SerializeField] private GameObject combineEntryPrefab;
    private Transform combineListContainer;
    private List<CombineDisplay> activeCombineDisplays = new List<CombineDisplay>();
    private HashSet<string> collectedIDs = new HashSet<string>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Build the map
            foreach (var data in itemSprites)
            {
                if (!itemSpriteMap.ContainsKey(data.itemTag))
                {
                    itemSpriteMap.Add(data.itemTag, data.sprite);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        string scene = SceneManager.GetActiveScene().name;

        if (scene == "2_HallwayDoctors" || scene == "3_WaitingArea" ||scene == "4_Canteen" || scene == "5_HallwayDorm" || scene == "6_Janitors"|| scene == "DoctorsOffice"||
           (scene == "1_IsolationChamber" && secondChallenge.isSecondChallengeCompleted))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                bool newState = !notebook.activeSelf;
                notebook.SetActive(newState);
                foreach (Transform child in notebook.transform)
                    child.gameObject.SetActive(newState);
            }
        }
    }
    public void MarkItemCollected(string id)
    {
        collectedIDs.Add(id);
    }

    public bool HasItemBeenCollected(string id)
    {
        return collectedIDs.Contains(id);
    }
    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject notebookObj = GameObject.FindWithTag("Notebook");
        GameObject inventoryUIObj = GameObject.FindWithTag("InventoryUI");
        combineListContainer = GameObject.FindWithTag("CombineList")?.transform;

        if (notebookObj != null && inventoryUIObj != null)
        {
            ReinitializeReferences(notebookObj, inventoryUIObj.transform);
            UpdateInventoryUI();
            PopulateCombineUI(); 
        }
        else
        {
            Debug.LogWarning("NotebookInventory: Could not find notebook or inventory UI in scene " + scene.name);
        }

        if (notebook != null)
            notebook.SetActive(false);

        RemoveCollectedItemsFromScene();
    }

    private void RemoveCollectedItemsFromScene()
    {
        string[] collectibleTags = { "Food", "Water", "pingPongBall", "OddColoredJuice", "bobbyPin", "Food2", "paperClip" };

        foreach (string tag in collectibleTags)
        {
            GameObject[] items = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject obj in items)
            {
                string uniqueID = tag + "_" + obj.name;

                if (HasItemBeenCollected(uniqueID))
                {
                    Destroy(obj);  
                }
            }
        }

        Debug.Log("removing item");
    }

    public void ReinitializeReferences(GameObject newNotebook, Transform newInventoryUI)
    {
        notebook = newNotebook;
        inventoryUI = newInventoryUI;
    }

    public bool IsSceneAllowed()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        foreach (string scene in allowedSceneNames)
        {
            if (currentScene == scene)
                return true;
        }
        return false;
    }

    public void OpenNotebook()
    {
        bool isActive = !notebook.activeSelf;
        notebook.SetActive(isActive);
        foreach (Transform child in notebook.transform)
            child.gameObject.SetActive(isActive);
    }

    public GameObject AddToInventory(string itemTag)
    {
        inventory.Add(itemTag);
        UpdateInventoryUI();
        UpdateCombineDisplays();
        return null;
    }

    public GameObject UpdateInventoryUI()
    {
        foreach (Transform child in inventoryUI)
            Destroy(child.gameObject);

        float xOffset = 220f;
        float yOffset = -220f;
        int itemsPerRow = 2;
        GameObject lastSlot = null;

        int start = currentPage * itemsPerPage;
        int end = Mathf.Min(start + itemsPerPage, inventory.Count);

        for (int i = start; i < end; i++)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, inventoryUI);
            TMP_Text slotText = slot.GetComponentInChildren<TMP_Text>();
            if (slotText != null) slotText.text = inventory[i];

            Image icon = slot.transform.Find("ItemIcon")?.GetComponent<Image>();
            if (icon != null)
            {
                Sprite itemSprite = GetItemSprite(inventory[i]);
                if (itemSprite != null)
                {
                    icon.sprite = itemSprite;
                    icon.enabled = true;
                }
                else
                {
                    icon.enabled = false; // Hide icon if not found
                }
            }

            RectTransform slotRect = slot.GetComponent<RectTransform>();
            if (slotRect != null)
            {
                int localIndex = i - start;
                int row = localIndex / itemsPerRow;
                int col = localIndex % itemsPerRow;
                float xPos = xOffset * col;
                float yPos = yOffset * row;
                slotRect.anchoredPosition = new Vector2(xPos, yPos);
            }

            lastSlot = slot;
        }

        return lastSlot;
    }

    public Sprite GetItemSprite(string itemTag)
    {
        if (itemSpriteMap.TryGetValue(itemTag, out Sprite sprite))
        {
            return sprite;
        }
        return null;
    }

    public void NextPage()
    {
        int maxPage = Mathf.CeilToInt((float)inventory.Count / itemsPerPage) - 1;
        if (currentPage < maxPage)
        {
            currentPage++;
            UpdateInventoryUI();
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdateInventoryUI();
        }
    }

    public int CountItems(string itemTag)
    {
        int count = 0;
        foreach (string item in inventory)
        {
            if (item == itemTag)
                count++;
        }
        return count;
    }

    public void PopulateCombineUI()
    {
        foreach (Transform child in combineListContainer)
            Destroy(child.gameObject);

        activeCombineDisplays.Clear();

        string currentScene = SceneManager.GetActiveScene().name;

        foreach (var recipe in combineRecipes)
        {
            if (recipe.sceneName != currentScene)
                continue;

            GameObject entry = Instantiate(combineEntryPrefab, combineListContainer);
            TMP_Text label = entry.GetComponentInChildren<TMP_Text>();
            TMP_Text count = entry.transform.Find("CountText")?.GetComponent<TMP_Text>();
            Button button = entry.GetComponentInChildren<Button>();

            if (label != null)
                label.text = recipe.outputItem;

            CombineDisplay display = new CombineDisplay
            {
                outputItem = recipe.outputItem,
                requiredItems = new List<string>(recipe.requiredItems),
                requiredAmounts = new List<int>(recipe.requiredAmounts),
                labelText = label,
                countText = count,
                labelButton = button
            };

            CombineDisplay localDisplay = display;
            button.onClick.AddListener(() => TryCombine(localDisplay));

            activeCombineDisplays.Add(display);
        }

        UpdateCombineDisplays();
    }



    public void UpdateCombineDisplays()
    {
        foreach (var display in activeCombineDisplays)
        {
            List<string> itemStatus = new List<string>();
            bool ready = true;

            for (int i = 0; i < display.requiredItems.Count; i++)
            {
                string item = display.requiredItems[i];
                int needed = display.requiredAmounts[i];
                int collected = CountItems(item);

                itemStatus.Add($"{collected}/{needed} {item}");

                if (collected < needed)
                    ready = false;
            }

            if (display.countText != null)
                display.countText.text = string.Join(", ", itemStatus);

            if (display.labelText != null)
                display.labelText.color = ready ? Color.red : Color.gray;

            if (display.labelButton != null)
                display.labelButton.interactable = ready;
        }
    }


    public void TryCombine(CombineDisplay display)
    {
        bool ready = true;

        for (int i = 0; i < display.requiredItems.Count; i++)
        {
            if (CountItems(display.requiredItems[i]) < display.requiredAmounts[i])
            {
                ready = false;
                break;
            }
        }

        if (!ready) return;

        for (int i = 0; i < display.requiredItems.Count; i++)
        {
            string item = display.requiredItems[i];
            int amount = display.requiredAmounts[i];

            for (int j = 0; j < amount; j++)
                inventory.Remove(item);
        }
        if (display.labelText != null)
        {
            display.labelText.text = $"<s>{display.outputItem}</s>";
            display.labelText.color = Color.gray;
            Debug.Log($"Combining: {display.outputItem} — applying strikethrough");
        }

        if (display.labelButton != null)
        {

            display.labelButton.interactable = false;
        }
        inventory.Add(display.outputItem);
        UpdateInventoryUI();
        UpdateCombineDisplays();


    }

}

[System.Serializable]
public class CombinationRecipe
{
    public string outputItem;
    public List<string> requiredItems = new List<string>();
    public List<int> requiredAmounts = new List<int>();

    public string sceneName;

}

[System.Serializable]
public class CombineDisplay
{
    public string outputItem;
    public List<string> requiredItems = new List<string>();
    public List<int> requiredAmounts = new List<int>();

    public TMP_Text labelText;
    public TMP_Text countText;
    public Button labelButton;
}
[System.Serializable]
public class ItemSpriteData
{
    public string itemTag;
    public Sprite sprite;
}