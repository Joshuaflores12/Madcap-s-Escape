using UnityEngine;

public class FungusManager : MonoBehaviour
{
    [SerializeField] GameObject slider;
    [SerializeField] GameObject weakpoints;
    [SerializeField] GameObject instruction;

    public void UnhideChallengeObjects()
    {
        slider.SetActive(true);
        weakpoints.SetActive(true);
        instruction.SetActive(true);
        Debug.Log("sobrang latina");
    }

    public void HideChallengeObjects()
    {
        slider.SetActive(false);
        weakpoints.SetActive(false);
        instruction.SetActive(false);
    }
}
