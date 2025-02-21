using UnityEngine;
using UnityEngine.UI;
using Fungus;
using TMPro;

public class SavePlayerName : MonoBehaviour
{
    public TMP_InputField inputField; 
    public Flowchart flowchart;    
    public GameObject playerInputUI;  

    public void SaveNameToFungus()
    {
        string playerName = inputField.text;

        
        if (flowchart != null)
        {
            flowchart.SetStringVariable("PlayerName", playerName);
            Debug.Log("Player Name Saved: " + playerName);
        }
        else
        {
            Debug.LogError("Flowchart is not assigned!");
        }

        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();

        playerInputUI.SetActive(false);
    }
}
