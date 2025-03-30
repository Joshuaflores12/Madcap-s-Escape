using UnityEngine;
using System.Collections;

public class TeleportMC : MonoBehaviour
{
    [SerializeField] private Vector3 teleportPosition; 
    [SerializeField] private float teleportSpeed = 5f; 

    public void StartTeleport(Vector3 newPosition)
    {
        StopAllCoroutines(); 
        StartCoroutine(MovePlayer(newPosition));
    }

    public void StartTeleportToPreset()
    {
        StartTeleport(teleportPosition); 
    }

    private IEnumerator MovePlayer(Vector3 targetPosition)
    {
        float timeElapsed = 0f;
        Vector3 startPos = transform.position;
        float duration = Vector3.Distance(startPos, targetPosition) / teleportSpeed; 

        while (timeElapsed < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPosition, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; 
    }
}
