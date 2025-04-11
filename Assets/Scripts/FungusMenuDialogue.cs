using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class FungusMenuDialogue : MonoBehaviour
{
    public Flowchart flowchart;

    public void UnlockingDrawer()
    {
        StartCoroutine(DisableLockpick());
    }

    System.Collections.IEnumerator DisableLockpick()
    {
        yield return new WaitForSeconds(0.05f);

        bool hasLockpick = NotebookInventory.Instance.CountItems("Lockpick") > 0;

        MenuDialog menuDialog = FindFirstObjectByType<MenuDialog>(); 
        if (menuDialog != null)
        {
            Debug.Log("Found MenuDialog!");

            Button[] buttons = menuDialog.GetComponentsInChildren<Button>(true);
            foreach (Button btn in buttons)
            {
                Text txt = btn.GetComponentInChildren<Text>();
                if (txt != null)
                {
                    //Debug.Log("Checking button: " + txt.text);

                    if (txt.text == "USE LOCKPICK")
                    {
                        if (!hasLockpick)
                        {
                            btn.interactable = false;
                            Debug.Log("USE LOCKPICK disabled.");
                        }
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("MenuDialog not found!");
        }
    }
}
