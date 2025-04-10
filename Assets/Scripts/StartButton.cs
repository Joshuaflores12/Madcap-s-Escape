using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class StartButton : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator;
    [SerializeField] private Animator fadeInAnimator;
    [SerializeField] private string nextSceneName;
    [SerializeField] private float fadeOutDuration = 1.5f; 
    [SerializeField] private GameObject fadeOutObject;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private VideoPlayer videoPlayerCutscene;
    [SerializeField] private SwitchScene switchScene;

    private void Start()
    {
        fadeOutObject.SetActive(false);
        blackScreen.SetActive(true);
        Invoke(nameof(FadeOutBeforeVideo),5f);
    }
    public void StartGame()
    {
        fadeOutObject.SetActive(true);
        StartCoroutine(FadeAndLoadScene());
    }

    public void FadeOutBeforeVideo()
    {
        fadeOutObject.SetActive(true);

    }

    public void FadeInCutscene()
    {

        fadeInAnimator.Play("fadeIn");
    }
    public void PlayVideo()
    {
        videoPlayerCutscene.Play();
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
