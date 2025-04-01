using UnityEngine;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI NpcName;
    [SerializeField] GameObject NpcNameHoverUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseOver()
    {
        if (gameObject.CompareTag("Patient1")) 
        {
            NpcName.text = "Patient1";
            NpcNameHoverUI.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (gameObject.CompareTag("Patient1"))
        {
            NpcNameHoverUI.SetActive(false);
        }
    }
}
