using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitingAreaClownMask : MonoBehaviour
{
    [SerializeField] public GameObject SoberStateWaitingAreaBG;
    [SerializeField] public GameObject ManicStateWaitingAreaBG;

    [SerializeField] private bool isManic = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ManicStateWaitingAreaBG.SetActive(false);
        SoberStateWaitingAreaBG.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "3_WaitingArea")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isManic = !isManic;

                SoberStateWaitingAreaBG.SetActive(!isManic);
                ManicStateWaitingAreaBG.SetActive(isManic);
            }
        }

    }


    public void SwitchStateBG()
    {
        isManic = !isManic;

        SoberStateWaitingAreaBG.SetActive(!isManic);
        ManicStateWaitingAreaBG.SetActive(isManic);
    }
}
