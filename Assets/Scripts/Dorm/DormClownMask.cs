using UnityEngine;

public class DormClownMask : MonoBehaviour
{
    [SerializeField] public GameObject SoberStateDormBG;
    [SerializeField] public GameObject ManicStateDormBG;

    [SerializeField] private bool isManic = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ManicStateDormBG.SetActive(false);
        SoberStateDormBG.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isManic = !isManic;

            SoberStateDormBG.SetActive(!isManic);
            ManicStateDormBG.SetActive(isManic);
        }
    }


    public void SwitchStateCanteenBG()
    {
        isManic = !isManic;

        SoberStateDormBG.SetActive(!isManic);
        ManicStateDormBG.SetActive(isManic);
    }
}
