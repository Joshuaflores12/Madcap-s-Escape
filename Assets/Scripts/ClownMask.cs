using UnityEngine;
using UnityEngine.SceneManagement;

public class ClownMask : MonoBehaviour
{
    [SerializeField] private GameObject SoberStateIsolationBG;
    public GameObject ManicStateIsolationBG;
    [SerializeField] private GameObject SoberStateHallwayBG;
    [SerializeField] private GameObject ManicStateHallwayBG;

    [SerializeField] private Transform mc; 
    [SerializeField] private Transform hallwayCheckpoint; 
    [SerializeField] private SecondChallenge secondChallenge; 

    private bool isManic = false;
    private bool isInHallway = false;

    void Start()
    {
        ManicStateIsolationBG.SetActive(false);
        SoberStateIsolationBG.SetActive(true);
    }

    void Update()
    {
        if (mc.position.x > hallwayCheckpoint.position.x)
        {
            isInHallway = true;
        }
        if (SceneManager.GetActiveScene().name == "1_IsolationChamber")
        {
            if (secondChallenge.isSecondChallengeCompleted == true)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    isManic = !isManic;
                    SwitchStateBG();
                }
            }
        }

        
    }
    public void SwitchStateIsolationBG()
    {
        isManic = !isManic;

        SoberStateIsolationBG.SetActive(!isManic);
        ManicStateIsolationBG.SetActive(isManic);
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
            SoberStateIsolationBG.SetActive(!isManic);
            ManicStateIsolationBG.SetActive(isManic);
        }
    }

    public void SetInHallwayTrue()
    {
        isInHallway = true;
    }
/*    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInHallway = true;
            SwitchStateBG();
        }
    }*/

/*    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInHallway = false;
            SwitchStateBG();
        }
    }*/
}
