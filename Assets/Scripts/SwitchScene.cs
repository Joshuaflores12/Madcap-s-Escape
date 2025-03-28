using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string scene;
    [SerializeField] string scene2;
    [SerializeField] TextMeshProUGUI chapterTitleText;
    [SerializeField] float fadeDuration = 2f;
    [SerializeField] float delayBeforeFadeOut = 2f;
    [SerializeField] private Curtains curtains;
    [SerializeField] private GameObject fadeOutObject;

    void Start()
    {
        chapterTitleText.alpha = 0f;
        StartCoroutine(FadeInAndOutChapterTitle());
        fadeOutObject.SetActive(false);
    }

    public void OnPlayButtonClicked()
    {
        StartCoroutine(LoadSceneAfterTextFade());
    }

    public void SwitchSceneFromFungus()
    {

        StartCoroutine(LoadSceneAfterTextFade());
        Debug.Log("Switching scene to: " + scene2);
    }

    private IEnumerator FadeInAndOutChapterTitle()
    {
        float timeElapsed = 0f;

        // Fade in
        while (timeElapsed < fadeDuration)
        {
            chapterTitleText.alpha = Mathf.Lerp(0f, 1f, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        chapterTitleText.alpha = 1f;

        yield return new WaitForSeconds(delayBeforeFadeOut);

        // Fade out
        timeElapsed = 0f;
        while (timeElapsed < fadeDuration)
        {
            chapterTitleText.alpha = Mathf.Lerp(1f, 0f, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        chapterTitleText.alpha = 0f;

        chapterTitleText.gameObject.SetActive(false);

        if (curtains != null)
        {
            curtains.OpenCurtains();
        }
    }

    private IEnumerator LoadSceneAfterTextFade()
    {
        fadeOutObject.SetActive(true);
        yield return new WaitForSeconds(fadeDuration + delayBeforeFadeOut);

        animator.SetBool("FadeOut", true);

        float fadeOutTime = 4f; 
        yield return new WaitForSeconds(fadeOutTime);

        SceneManager.LoadScene(scene2);
    }

}
