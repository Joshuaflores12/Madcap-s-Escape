using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetInventory : MonoBehaviour
{
    public void QuitButton()
    {
        if (NotebookInventory.Instance != null)
        {
            NotebookInventory.Instance.ResetItemInventory();
        }
        else
        {
            Debug.LogWarning("NotebookInventory not found in scene.");
        }

        SceneManager.LoadScene("1_IsolationChamber");
    }
}
