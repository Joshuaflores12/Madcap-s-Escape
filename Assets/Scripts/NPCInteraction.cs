using UnityEngine;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Patient1, Patient1_1,KitchenLady, Patient2, Patient3, Patient4,Patient5,Patient6;
    [SerializeField] private GameObject NpcNameHoverUI;

    private void Start()
    {
        NpcNameHoverUI.SetActive(true);
    }

    private void OnMouseOver()
    {
        if (gameObject.CompareTag("Patient1")) 
        {
            NpcNameHoverUI.SetActive(true);
            Patient1.text = "Patient1";
            Patient1_1.text = string.Empty;
            Patient2.text = string.Empty;
            Patient3.text = string.Empty;
            Patient4.text = string.Empty;
            Patient5.text = string.Empty;
            Patient6.text = string.Empty;
            KitchenLady.text = string.Empty;
        }
    }

    private void OnMouseExit()
    {
        NpcNameHoverUI.SetActive(false);
    }

    private void Update()
    {
        OnMouseExit();
        OnMouseOver();
    }
}
