using UnityEngine;

public class GuardDraggingMC : MonoBehaviour
{
    [SerializeField] private GameObject guard;
    [SerializeField] private GameObject MC;
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float speed = 5f;
    private bool isMoving = false;

    void Start()
    {
        transform.position = startPosition.position;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition.position) < 0.01f)
            {
                isMoving = false;
            }
        }
    }

    public void StartMoving()
    {
        isMoving = true;
    }
}
