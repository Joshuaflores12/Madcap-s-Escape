using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] NotebookInventory notebookInventory;

    void Awake()
    {
        GameObject inventoryObject = GameObject.FindWithTag("Inventory");
        if (inventoryObject != null)
            notebookInventory = inventoryObject.GetComponent<NotebookInventory>();
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
            }
        }
    }
}
