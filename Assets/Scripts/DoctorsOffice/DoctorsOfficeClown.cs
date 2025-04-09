using UnityEngine;
using UnityEngine.SceneManagement;

public class DoctorsOfficeClown : MonoBehaviour
{
    [SerializeField] public GameObject SoberStateDoctorsOfficeBG;
    [SerializeField] public GameObject ManicStateDoctorsOfficeBG;

    [SerializeField] private bool isManic = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ManicStateDoctorsOfficeBG.SetActive(false);
        SoberStateDoctorsOfficeBG.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "DoctorsOffice")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isManic = !isManic;

                SoberStateDoctorsOfficeBG.SetActive(!isManic);
                ManicStateDoctorsOfficeBG.SetActive(isManic);
            }
        }

    }


    public void SwitchStateBG()
    {
        isManic = !isManic;

        SoberStateDoctorsOfficeBG.SetActive(!isManic);
        ManicStateDoctorsOfficeBG.SetActive(isManic);
    }
}
