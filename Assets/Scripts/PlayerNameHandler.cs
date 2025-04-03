using UnityEngine;
using TMPro;
using Fungus;

public class SavePlayerName : MonoBehaviour
{
    public TMP_InputField inputField;
    public Flowchart flowchart;
    public Character character;

    private const string PlayerNameKey = "PlayerName";

    private void Start()
    {
        if (PlayerPrefs.HasKey(PlayerNameKey))
        {
            string savedName = PlayerPrefs.GetString(PlayerNameKey);
            inputField.text = savedName;
            character.name = savedName;

            if (flowchart != null)
            {
                flowchart.SetStringVariable("PlayerName", savedName);
            }
        }

        inputField.onSubmit.AddListener(delegate { SaveNameAndContinue(); });
    }

    private void SaveNameAndContinue()
    {
        string playerName = inputField.text.Trim();

        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerPrefs.SetString(PlayerNameKey, playerName);
            PlayerPrefs.Save();

            if (flowchart != null)
            {
                flowchart.SetStringVariable("PlayerName", playerName);
            }

            if (character != null)
            {
                character.name = playerName;
            }

            if (flowchart != null)
            {
                flowchart.SendFungusMessage("ContinueDialogue");
            }
        }
    }
}
