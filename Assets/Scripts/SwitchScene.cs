using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string hallwayDoctors;
    [SerializeField] string waitingArea;
    [SerializeField] string canteen;
    [SerializeField] string hallwayDorm;
    [SerializeField] string janitorsCloset;
    [SerializeField] string doctorsOffice;
    [SerializeField] TextMeshProUGUI chapterTitleText;
    [SerializeField] GameObject titleText;
    [SerializeField] float fadeDuration = 0f;
    [SerializeField] float delayBeforeFadeOut = 0f;
    [SerializeField] private Curtains curtains;
    [SerializeField] private GameObject fadeOutObject;
    public bool textFadeDone = false;

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
        StartCoroutine(LoadSceneAfterTextFade(waitingArea));
    }

    public void SwitchSceneToHallwayDoctors()
    {
        StartCoroutine(LoadSceneAfterFadeOut(hallwayDoctors));
        Debug.Log("Switching scene to: " + hallwayDoctors);
    }

    public void SwitchSceneToWaitingArea()
    {
        StartCoroutine(LoadSceneAfterFadeOut(waitingArea));
        Debug.Log("Switching scene to: " + waitingArea);
    }

    public void SwitchSceneToCanteen()
    {
        StartCoroutine(LoadSceneAfterFadeOut(canteen));
        Debug.Log("Switching scene to: " + canteen);
    }
    public void SwitchSceneToHallwayDorm()
    {
        Debug.Log("to dorm");
        StartCoroutine(LoadSceneAfterFadeOut(hallwayDorm));
        Debug.Log("Switching scene to: " + hallwayDorm);
    }
    public void SwitchSceneToDoctorsOffice()
    {
        StartCoroutine(LoadSceneAfterFadeOut(doctorsOffice));
        Debug.Log("Switching scene to: " + doctorsOffice);
    }

    public void SwitchSceneToJanitorsCloset()
    {
        StartCoroutine(LoadSceneAfterFadeOut(janitorsCloset));
        Debug.Log("Switching scene to: " + janitorsCloset);
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

        if (chapterTitleText.gameObject.activeSelf!)
        {
            textFadeDone = true;
        }

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
