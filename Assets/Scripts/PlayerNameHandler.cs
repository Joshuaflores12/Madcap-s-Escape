using UnityEngine;
using TMPro;
using Fungus;

public class SavePlayerName : MonoBehaviour
{
    public TMP_InputField inputField;
    public Flowchart flowchart;
    public Character character; 

    public void SaveNameToFungus()
    {
        if (!string.IsNullOrEmpty(inputField.text))
        {
            string playerName = inputField.text;
            // Update Fungus Variable
            if (flowchart != null)
            {
                flowchart.SetStringVariable("PlayerName", playerName);
            }

            if (character != null)
            {
                character.name = playerName;
            }
        }
    }
    public void Start()
    {
        character.name = "?";
    }

    public void Update()
    {
        SaveNameToFungus();
    }
}
