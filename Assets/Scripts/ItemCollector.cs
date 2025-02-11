using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))  
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("test1")) 
                {
                    string itemTag = hit.collider.tag;
                    NotebookInventory.Instance.AddToInventory(itemTag);

                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
