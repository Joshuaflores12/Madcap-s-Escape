using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string scene;
    [SerializeField] string hallwayLeft;
    [SerializeField] string hallwayRight;
    [SerializeField] string scene2;
    [SerializeField] string scene3;
    [SerializeField] string scene4;
    [SerializeField] TextMeshProUGUI chapterTitleText;
    [SerializeField] GameObject titleText;
    [SerializeField] float fadeDuration = 0f;
    [SerializeField] float delayBeforeFadeOut = 0f;
    [SerializeField] private Curtains curtains;
    [SerializeField] private GameObject fadeOutObject;

    void Start()
    {
        titleText.SetActive(false);
        if (!titleText.activeSelf)
        {
            titleText.SetActive(true);
            chapterTitleText.alpha = 0f;
            StartCoroutine(FadeInAndOutChapterTitle());
            fadeOutObject.SetActive(false);
        }
    }

    public void OnPlayButtonClicked()
    {
        StartCoroutine(LoadSceneAfterTextFade(scene2));
    }

    public void SwitchSceneToHallwayLeft()
    {
        StartCoroutine(LoadSceneAfterFadeOut(hallwayLeft));
        Debug.Log("Switching scene to: " + hallwayLeft);
    }
    public void SwitchSceneToHallwayRight()
    {
        StartCoroutine(LoadSceneAfterFadeOut(hallwayRight));
        Debug.Log("Switching scene to: " + hallwayRight);
    }

    public void SwitchSceneToChapter2()
    {
        StartCoroutine(LoadSceneAfterFadeOut(scene2));
        Debug.Log("Switching scene to: " + scene2);
    }

    public void SwitchSceneToChapter4()
    {
        StartCoroutine(LoadSceneAfterTextFade(scene4));
        Debug.Log("Switching scene to: " + scene4);
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

    private IEnumerator LoadSceneAfterTextFade(string sceneName)
    {
        fadeOutObject.SetActive(true);
        yield return new WaitForSeconds(fadeDuration + delayBeforeFadeOut);

        animator.SetBool("FadeOut", true);

        float fadeOutTime = 4f;
        yield return new WaitForSeconds(fadeOutTime);

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator LoadSceneAfterFadeOut(string sceneName)
    {
        fadeOutObject.SetActive(true);
        yield return new WaitForSeconds(0f + 0f);

        animator.SetBool("FadeOut", true);

        curtains.CloseCurtains();

        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !animator.IsInTransition(0));

        if(curtains.isClosing == true)
        {
        SceneManager.LoadScene(sceneName);

        }
    }
}
