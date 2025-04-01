
using Unity.Cinemachine;
using UnityEngine;

public class CameraLimiter : MonoBehaviour
{
    private CinemachineConfiner2D confiner2D;

    void Start()
    {
        confiner2D = GetComponent<CinemachineConfiner2D>();

        if (confiner2D == null)
        {
            Debug.LogError("CinemachineConfiner2D component not found!");
        }
    }

    public void EnableConfiner()
    {
        if (confiner2D != null)
        {
            confiner2D.enabled = true;  
            Debug.Log("Cinemachine Confiner enabled.");
        }
    }

    public void DisableConfiner()
    {
        if (confiner2D != null)
        {
            confiner2D.enabled = false;  
            Debug.Log("Cinemachine Confiner disabled.");
        }
    }
}
