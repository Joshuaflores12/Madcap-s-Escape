using UnityEngine;
using Fungus;

public class TriggerFungusBlock : MonoBehaviour
{
    [SerializeField] private Flowchart flowchart;
    [SerializeField] private string blockName;
    [SerializeField] private Transform checkpoint;
    private Transform player;
    private bool hasTriggered = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!hasTriggered && PlayerHasPassedCheckpoint())
        {
            hasTriggered = true;
            ExecuteFungusBlock();
        }
    }

    private bool PlayerHasPassedCheckpoint()
    {
        return player != null && player.position.x > checkpoint.position.x;
    }

    private void ExecuteFungusBlock()
    {
        if (flowchart != null)
        {
            flowchart.ExecuteBlock(blockName);
        }
    }
}
