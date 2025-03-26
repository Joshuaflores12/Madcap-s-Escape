using UnityEngine;

public class ClownMask : MonoBehaviour
{
    [SerializeField] public GameObject SoberStateIsolationBG;
    [SerializeField] public GameObject ManicStateIsolationBG;
    [SerializeField] private  bool isManic = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ManicStateIsolationBG.SetActive(false);
        SoberStateIsolationBG.SetActive(true);
    }

    // update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isManic = !isManic;
            
            SoberStateIsolationBG.SetActive(!isManic); 
            ManicStateIsolationBG.SetActive(isManic);
        }
    }

    public void SwitchStateIsolationBG()
    {
        isManic = !isManic;

        SoberStateIsolationBG.SetActive(!isManic);
        ManicStateIsolationBG.SetActive(isManic);
    }

}
