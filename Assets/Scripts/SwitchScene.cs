using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string scene;
    [SerializeField] TextMeshProUGUI chapterTitleText;
    [SerializeField]  float fadeDuration = 2f;
    [SerializeField] float delayBeforeFadeOut = 2f;
    [SerializeField] private Curtains curtains;

    void Start()
    {
        chapterTitleText.alpha = 0f;

        StartCoroutine(FadeInAndOutChapterTitle());
    }

    public void OnPlayButtonClicked()
    {
        StartCoroutine(LoadSceneAfterTextFade());
    }

    private IEnumerator FadeInAndOutChapterTitle()
    {
        float timeElapsed = 0f;
        while (timeElapsed < fadeDuration)
        {
            chapterTitleText.alpha = Mathf.Lerp(0f, 1f, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        chapterTitleText.alpha = 1f; 

        yield return new WaitForSeconds(delayBeforeFadeOut);

        timeElapsed = 0f;
        while (timeElapsed < fadeDuration)
        {
            chapterTitleText.alpha = Mathf.Lerp(1f, 0f, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        chapterTitleText.alpha = 0f; 

        if (curtains != null) 
        {
            curtains.OpenCurtains();
        }
    }

    private IEnumerator LoadSceneAfterTextFade()
    {
        yield return new WaitForSeconds(fadeDuration + delayBeforeFadeOut);

        animator.SetBool("FadeOut", true);

        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);

        SceneManager.LoadScene(scene);
    }
}
