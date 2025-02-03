using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject Inventoy_UI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Inventoy_UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Inventoy_UI.SetActive(!Inventoy_UI.activeSelf);
        }
    }
    public void Use_Item() 
    {
        
    }

    public void Combine_Item() 
    {
        
    }

    public void Close() 
    {
        Inventoy_UI.SetActive(false);
    }
}
