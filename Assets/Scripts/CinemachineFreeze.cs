using UnityEngine;
using Unity.Cinemachine;

public class CinemachineFreeze : MonoBehaviour
{
    private CinemachineCamera cinemachineCamera;

    void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
        FreezeCamera();
    }

    public void FreezeCamera()
    {
        if (cinemachineCamera != null)
        {
            cinemachineCamera.enabled = false; 
        }
    }

    public void UnfreezeCamera()
    {
        if (cinemachineCamera != null)
        {
            cinemachineCamera.enabled = true; 
        }
    }
}
