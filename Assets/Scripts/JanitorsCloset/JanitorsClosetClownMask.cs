using UnityEngine;
using UnityEngine.SceneManagement;

public class JanitorsClosetClownMask : MonoBehaviour
{
    [SerializeField] public GameObject SoberStateJanitorsClosetBG;
    [SerializeField] public GameObject ManicStateJanitorsClosetBG;

    [SerializeField] private bool isManic = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ManicStateJanitorsClosetBG.SetActive(false);
        SoberStateJanitorsClosetBG.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "6_Janitors")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isManic = !isManic;

                SoberStateJanitorsClosetBG.SetActive(!isManic);
                ManicStateJanitorsClosetBG.SetActive(isManic);
            }
        }

    }


    public void SwitchStateBG()
    {
        isManic = !isManic;

        SoberStateJanitorsClosetBG.SetActive(!isManic);
        ManicStateJanitorsClosetBG.SetActive(isManic);
    }
}
