using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] public GameObject Inventoy_UI;
    [SerializeField] public GameObject Combine_UI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Inventoy_UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Inventoy_UI.SetActive(!Inventoy_UI.activeSelf);
        }
    }
    public void Use_Item() 
    {
        
    }

    public void Combine_Item() 
    {
        Combine_UI.SetActive(true);
        Inventoy_UI.SetActive(false);
    }

    public void Close() 
    {
        Inventoy_UI.SetActive(false);
        Combine_UI.SetActive(false);
    }

    public void Open() 
    {
        Inventoy_UI.SetActive(true);
    }
}
