using UnityEngine;
using TMPro;
public class PlayerNameHandler : MonoBehaviour
{
    [SerializeField] public TMP_InputField playerNameInput;
    [SerializeField] public GameObject playerInputUI;

    public void SavePlayerName() 
    {
        string playerName = playerNameInput.text;
        PlayerPrefs.SetString("Player Name", playerName);
        PlayerPrefs.Save();
        Debug.Log("Player Name Saved: " + playerName);
        playerInputUI.SetActive(false);
    }

    // accesing the player name in other scripts
    //string playerName = PlayerPrefs.GetString("PlayerName", "Patient");
    //Debug.Log("Player name loaded: " + playerName);

    
}
