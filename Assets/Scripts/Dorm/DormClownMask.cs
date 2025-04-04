using UnityEngine;
using UnityEngine.SceneManagement;

public class DormClownMask : MonoBehaviour
{
    [SerializeField] public GameObject SoberStateDormBG;
    [SerializeField] public GameObject ManicStateDormBG;
    [SerializeField] public GameObject SoberStateHallwayBG;
    [SerializeField] public GameObject ManicStateHallwayBG;

    [SerializeField] private Transform mc;
    [SerializeField] private Transform hallwayCheckpoint;
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
        if (mc.position.x > hallwayCheckpoint.position.x)
        {
            isInHallway = true;
        }

        
        if (SceneManager.GetActiveScene().name == "HallwayDorm")
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                isManic = !isManic;

                SoberStateDormBG.SetActive(!isManic);
                ManicStateDormBG.SetActive(isManic);
            }
        }

        if (SceneManager.GetActiveScene().name == "HallwayLeft")
        {
            if (Input.GetKeyDown(KeyCode.R))
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

    public void SwitchStateBG()
    {
        if (isInHallway)
        {
            SoberStateHallwayBG.SetActive(!isManic);
            ManicStateHallwayBG.SetActive(isManic);

        }
        else
        {
            SoberStateDormBG.SetActive(!isManic);
            ManicStateDormBG.SetActive(isManic);
        }
    }
}
