using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPositionManager : MonoBehaviour
{
    public static PlayerPositionManager Instance;

    public enum EntryDirection { Left, Right }
    public EntryDirection entryFrom = EntryDirection.Left;

    [Header("Position Transforms")]
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;

    private GameObject player;

    void Awake()
    {
        if (SceneManager.GetActiveScene().name != "1_IsolationChamber" || SceneManager.GetActiveScene().name != "0_aStartScreen" || SceneManager.GetActiveScene().name != "0_Intro")
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject); 
                return;
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name != "1_IsolationChamber")
        {
            Debug.Log(" Scene loaded: " + scene.name + " | Spawning player from: " + entryFrom);

            player = GameObject.FindGameObjectWithTag("Player");
            leftSpawnPoint = GameObject.Find("LeftSpawnPoint").transform;
            rightSpawnPoint = GameObject.Find("RightSpawnPoint").transform;

            if (player != null)
            {
                if (entryFrom == EntryDirection.Left)
                    player.transform.position = leftSpawnPoint.position;
                else if (entryFrom == EntryDirection.Right)
                    player.transform.position = rightSpawnPoint.position;
            }
        }
    }

    public void EnterFromLeft()
    {
        entryFrom = EntryDirection.Left;
        Debug.Log("EnterFromLeft() CALLED ");
    }
    public void EnterFromRight()
    {
        entryFrom = EntryDirection.Right;
        Debug.Log("EnterFromRight() CALLED ");
    } 
}
