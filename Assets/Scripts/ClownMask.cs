using UnityEngine;

public class ClownMask : MonoBehaviour
{
    [SerializeField] public GameObject SoberStateBG;
    [SerializeField] public GameObject ManicStateBG;
    [SerializeField] private  bool isManic = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ManicStateBG.SetActive(false);
        SoberStateBG.SetActive(true);
    }

    // update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isManic = !isManic;
            
            SoberStateBG.SetActive(!isManic); 
            ManicStateBG.SetActive(isManic);
        }
    }
}
