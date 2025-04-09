using UnityEngine;
using UnityEngine.SceneManagement;

public class CanteenClownMask : MonoBehaviour
{
    [SerializeField] public GameObject SoberStateCanteenBG;
    [SerializeField] public GameObject ManicStateCanteenBG;

    [SerializeField] private bool isManic = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ManicStateCanteenBG.SetActive(false);
        SoberStateCanteenBG.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "4_Canteen")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isManic = !isManic;

                SoberStateCanteenBG.SetActive(!isManic);
                ManicStateCanteenBG.SetActive(isManic);
            }
        }

    }


    public void SwitchStateBG()
    {
        isManic = !isManic;

        SoberStateCanteenBG.SetActive(!isManic);
        ManicStateCanteenBG.SetActive(isManic);
    }
}
