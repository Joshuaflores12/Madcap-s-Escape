using UnityEngine;
using UnityEngine.SceneManagement;

public class HallwayDoctors : MonoBehaviour
{
    [SerializeField] public GameObject SoberStateHallwayBG;
    [SerializeField] public GameObject ManicStateHallwayBG;

    [SerializeField] private bool isManic = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ManicStateHallwayBG.SetActive(false);
        SoberStateHallwayBG.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "2_HallwayDoctors")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isManic = !isManic;

                SoberStateHallwayBG.SetActive(!isManic);
                ManicStateHallwayBG.SetActive(isManic);
            }
        }

    }


    public void SwitchStateCanteenBG()
    {
        isManic = !isManic;

        SoberStateHallwayBG.SetActive(!isManic);
        ManicStateHallwayBG.SetActive(isManic);
    }
}
