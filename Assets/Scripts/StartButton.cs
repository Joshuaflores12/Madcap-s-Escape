using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private string nextSceneName;
    [SerializeField] private float fadeOutDuration = 1.5f; 
    [SerializeField] private GameObject fadeOutObject;

    private void Start()
    {
        fadeOutObject.SetActive(false);
    }
    public void StartGame()
    {
        fadeOutObject.SetActive(true);
        StartCoroutine(FadeAndLoadScene());
    }

    private IEnumerator FadeAndLoadScene()
    {
        if (fadeAnimator != null)
        {
            fadeAnimator.SetTrigger("FadeOut");  
        }

        yield return new WaitForSeconds(fadeOutDuration);  

        SceneManager.LoadScene(nextSceneName);
    }
}
