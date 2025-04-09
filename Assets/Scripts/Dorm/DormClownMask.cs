using UnityEngine;
using UnityEngine.SceneManagement;

public class DormClownMask : MonoBehaviour
{
    [SerializeField] public GameObject SoberStateDormBG;
    [SerializeField] public GameObject ManicStateDormBG;
    [SerializeField] public GameObject SoberStateHallwayBG;
    [SerializeField] public GameObject ManicStateHallwayBG;

    [SerializeField] private bool isManic = false;

    private bool isInHallway = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ManicStateDormBG.SetActive(false);
        SoberStateDormBG.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        
        if (SceneManager.GetActiveScene().name == "5_HallwayDorm")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isManic = !isManic;

                SoberStateDormBG.SetActive(!isManic);
                ManicStateDormBG.SetActive(isManic);
                SoberStateHallwayBG.SetActive(!isManic);
                ManicStateHallwayBG.SetActive(isManic);
            }
        }

        if (SceneManager.GetActiveScene().name == "HallwayLeft")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isManic = !isManic;

                SoberStateHallwayBG.SetActive(!isManic);
                ManicStateHallwayBG.SetActive(isManic);
            }
        }
    }


    public void SwitchStateDormBG()
    {
        isManic = !isManic;

        SoberStateDormBG.SetActive(!isManic);
        ManicStateDormBG.SetActive(isManic);
    }

}
