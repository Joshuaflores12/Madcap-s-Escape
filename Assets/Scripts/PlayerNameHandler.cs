using UnityEngine;
using UnityEngine.UI;
using Fungus;
using TMPro;

public class SavePlayerName : MonoBehaviour
{
    public TMP_InputField inputField;
    public Flowchart flowchart;
    public GameObject playerInputUI;

    void Start()
    {
        // Set the default name to "?" at the start
        if (flowchart != null)
        {
            string savedName = PlayerPrefs.GetString("PlayerName", "?"); // Load saved name or set default
            flowchart.SetStringVariable("PlayerName", savedName);
        }
    }

    public void SaveNameToFungus()
    {
        string PlayerName = inputField.text;
        if (flowchart != null)
        {
            flowchart.SetStringVariable("PlayerName", PlayerName);
            flowchart.SetBooleanVariable("NameEntered", true); // Set flag
            Debug.Log("Player Name Saved: " + PlayerName);
        }
        else
        {
            Debug.LogError("Flowchart is not assigned!");
        }

        PlayerPrefs.SetString("PlayerName", PlayerName);
        PlayerPrefs.Save();

        playerInputUI.SetActive(false);
    }
}
