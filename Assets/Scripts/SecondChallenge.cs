using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class SecondChallenge : MonoBehaviour
{
    [SerializeField] private Slider spamMeterSlider;
    [SerializeField] private float spamIncreaseRate = 5f;
    [SerializeField] private float spamDecreaseRate = 60f;
    [SerializeField] private float vignetteIncreaseRate = 0.5f;
    [SerializeField] private float vignetteRecoverRate = 0.3f;
    [SerializeField] private float chromaticAberrationMax = 0.8f;
    [SerializeField] private float maxSpamValue = 100f;
    [SerializeField] private GameObject miniGameScreen;
    [SerializeField] private GameObject secondSlider;
    [SerializeField] private GameObject firstSlider;
    [SerializeField] FirstChallenge firstChallenge;
    [SerializeField] Animator animD;
    [SerializeField] Animator animA;

    private Volume globalVolume;
    private Vignette vignette;
    private ChromaticAberration chromaticAberration;
    private bool isSecondChallengeCompleted = false;
    private bool isBlackingOut = false;
    private bool isPanicking = false;
    private float lastSpamTime;
    private float timeToBlackout = 3f;
    private bool lastKeyWasA = false;


    void Start()
    {
        miniGameScreen.SetActive(false);

        globalVolume = FindFirstObjectByType<Volume>();

        if (globalVolume != null && globalVolume.profile != null)
        {
            globalVolume.profile.TryGet(out vignette);
            globalVolume.profile.TryGet(out chromaticAberration);

            if (vignette != null) vignette.intensity.value = 0.15f;
            if (chromaticAberration != null) chromaticAberration.intensity.value = 0f;
        }

        spamMeterSlider.maxValue = maxSpamValue;
        spamMeterSlider.value = 0f;
    }

    void Update()
    {
        
        if (firstChallenge.isChallengeCompleted == true)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                animA.SetTrigger("AisPressed");
                StartCoroutine(ResetTrigger());
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                animD.SetTrigger("DisPressed");
                StartCoroutine(ResetTrigger());
            }

            if (isSecondChallengeCompleted) return;

            if (Input.GetKeyDown(KeyCode.A) && !lastKeyWasA)
            {
                IncreaseSpam();
                lastKeyWasA = true;
                animA.SetTrigger("AisPressed");
                StartCoroutine(ResetTrigger());
            }
            else if (Input.GetKeyDown(KeyCode.D) && lastKeyWasA)
            {
                IncreaseSpam();
                lastKeyWasA = false;
                animD.SetTrigger("DisPressed");
                StartCoroutine(ResetTrigger());
            }

            if (Time.time - lastSpamTime > 0.3f)
            {
                spamMeterSlider.value -= spamDecreaseRate * Time.deltaTime;
                spamMeterSlider.value = Mathf.Clamp(spamMeterSlider.value, 0, maxSpamValue);
            }

/*            if (spamMeterSlider.value < maxSpamValue / 2 && isPanicking)
            {
                StopPanic();
                Debug.Log("Stop panicking");
            }*/

            if (spamMeterSlider.value > 0)
            {
                float blackoutProgress = (Time.time - lastSpamTime) / timeToBlackout;
                if (vignette != null)
                {
                    vignette.intensity.value += vignetteIncreaseRate * blackoutProgress * Time.deltaTime;
                    vignette.intensity.value = Mathf.Clamp(vignette.intensity.value, 0.15f, 0.9f);
                }
            }

            if (!isBlackingOut && vignette.intensity.value >= 0.9f)
            {
                isBlackingOut = true;
                StartCoroutine(RestartChallenge());
            }
        }
    }
    IEnumerator ResetTrigger()
    {
        yield return new WaitForSeconds(0.05f); 
        animA.ResetTrigger("AisPressed");
        animD.ResetTrigger("DisPressed");
    }

    private void IncreaseSpam()
    {
        lastSpamTime = Time.time;
        spamMeterSlider.value += spamIncreaseRate;
        spamMeterSlider.value = Mathf.Clamp(spamMeterSlider.value, 0, maxSpamValue);

        if (vignette != null)
        {
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0.15f, vignetteRecoverRate);
        }

        if (chromaticAberration != null)
        {
            chromaticAberration.intensity.value = Mathf.Lerp(0f, chromaticAberrationMax, spamMeterSlider.value / maxSpamValue);
        }

        if (spamMeterSlider.value >= maxSpamValue / 2 && !isPanicking)
        {
            StartCoroutine(PanicEffect());
        }

        if (spamMeterSlider.value >= maxSpamValue)
        {
            CompleteChallenge();
        }
    }

    void CompleteChallenge()
    {
        isSecondChallengeCompleted = true;
        Debug.Log("Challenge Completed!");
        StartCoroutine(HideMiniGameScreen());
        secondSlider.SetActive(false);
        isPanicking = false;

        StartCoroutine(RecoverFromPanic());
    }

    IEnumerator RecoverFromPanic()
    {
        float duration = 2f; 
        float elapsedTime = 0f;

        float startVignette = vignette.intensity.value;
        float startChromatic = chromaticAberration.intensity.value;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            if (vignette != null)
            {
                vignette.intensity.value = Mathf.Lerp(startVignette, 0.15f, t);
            }

            if (chromaticAberration != null)
            {
                chromaticAberration.intensity.value = Mathf.Lerp(startChromatic, 0f, t);
            }

            yield return null;
        }

        if (vignette != null) vignette.intensity.value = 0.15f;
        if (chromaticAberration != null) chromaticAberration.intensity.value = 0f;
    }
    IEnumerator RestartChallenge()
    {
        Debug.Log("Blackout! Restarting Challenge...");
        yield return new WaitForSeconds(1f);

        spamMeterSlider.value = 0;
        if (vignette != null) vignette.intensity.value = 0.15f;
        if (chromaticAberration != null) chromaticAberration.intensity.value = 0f;

        isBlackingOut = false;
        lastSpamTime = Time.time;
    }

    IEnumerator PanicEffect()
    {
        isPanicking = true;

        while (isPanicking)
        {
            float randomDelay = Random.Range(0.1f, 0.3f);
            chromaticAberration.intensity.value = 0.7f;

            yield return new WaitForSeconds(0.3f);
            chromaticAberration.intensity.value = 0.1f;

            yield return new WaitForSeconds(randomDelay);
        }
    }

    public void StopPanic()
    {
        isPanicking = false;
        StartCoroutine(SmoothChromaticAberrationTransition(0f, 1f));
    }

    IEnumerator SmoothChromaticAberrationTransition(float targetValue, float duration)
    {
        float startValue = chromaticAberration.intensity.value;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            chromaticAberration.intensity.value = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            yield return null;
        }
    }

    IEnumerator HideMiniGameScreen()
    {
        yield return new WaitForSeconds(2f);

        miniGameScreen.SetActive(false);
    }
}
