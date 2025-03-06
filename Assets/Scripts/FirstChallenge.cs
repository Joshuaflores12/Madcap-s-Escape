using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class FirstChallenge : MonoBehaviour
{
    [SerializeField] private Slider spamMeterSlider;
    [SerializeField] private float spamIncreaseRate = 5f;
    [SerializeField] private float spamDecreaseRate = 90f;
    [SerializeField] private float vignetteIncreaseRate = 0.3f;
    [SerializeField] private float vignetteRecoverRate = 0.5f;
    [SerializeField] private float chromaticAberrationMax = 1f;
    [SerializeField] private float maxSpamValue = 100f;
    [SerializeField] GameObject miniGameScreen;
    [SerializeField] private Button weakpointsButton;
    [SerializeField] private GameObject secondSlider;
    [SerializeField] private GameObject firstSlider;

    public bool isChallengeCompleted = false;
    
    private Volume globalVolume;
    private Vignette vignette;
    private ChromaticAberration chromaticAberration;
    private bool isSpamming = false;
    private bool isBlackingOut = false;
    private bool isPanicking = false;
    private float lastSpamTime;
    private float timeToBlackout = 2f;

    void Start()
    {
        miniGameScreen.SetActive(false);
        secondSlider.SetActive(false);
        globalVolume = FindFirstObjectByType<Volume>();

        if (globalVolume != null && globalVolume.profile != null)
        {
            globalVolume.profile.TryGet(out vignette);
            globalVolume.profile.TryGet(out chromaticAberration);

            if (vignette != null) vignette.intensity.value = 0.2f;
            if (chromaticAberration != null) chromaticAberration.intensity.value = 0f;
        }

        spamMeterSlider.maxValue = maxSpamValue;
        spamMeterSlider.value = 0f;
    }

    void Update()
    {
        if (isChallengeCompleted) return;

        if (Input.GetMouseButtonDown(0))
        {
            
        }

        if (Time.time - lastSpamTime > 0.2f)
        {
            isSpamming = false;
            spamMeterSlider.value -= spamDecreaseRate * Time.deltaTime;
            spamMeterSlider.value = Mathf.Clamp(spamMeterSlider.value, 0, maxSpamValue);
        }

        // Stop panic if spamMeterSlider drops below half
        if (spamMeterSlider.value < maxSpamValue / 2 && isPanicking)
        {
            StopPanic();
        }

        if (!isSpamming && spamMeterSlider.value > 0)
        {
            float blackoutProgress = (Time.time - lastSpamTime) / timeToBlackout;
            if (vignette != null)
            {
                vignette.intensity.value += vignetteIncreaseRate * blackoutProgress * Time.deltaTime;
                vignette.intensity.value = Mathf.Clamp(vignette.intensity.value, 0.2f, 1f);
            }
        }

        if (!isSpamming && vignette.intensity.value >= 1f && !isBlackingOut)
        {
            isBlackingOut = true;
            StartCoroutine(RestartChallenge());
        }

        if (isChallengeCompleted == true)
        {
            StartCoroutine(ShowMiniGameScreen());
            Debug.Log("show mini game screen");

        }
    }

    public void ClickablePoints()
    {
        isSpamming = true;
        lastSpamTime = Time.time;
        spamMeterSlider.value += spamIncreaseRate;
        spamMeterSlider.value = Mathf.Clamp(spamMeterSlider.value, 0, maxSpamValue);

        if (vignette != null)
        {
            vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0.2f, vignetteRecoverRate);
        }

        if (chromaticAberration != null)
        {
            chromaticAberration.intensity.value = Mathf.Lerp(0f, chromaticAberrationMax, spamMeterSlider.value / maxSpamValue);
        }

        // Trigger Panic Effect when slider reaches half
        if (spamMeterSlider.value >= maxSpamValue / 2 && !isPanicking)
        {
            StartCoroutine(PanicEffect());
        }

        if (spamMeterSlider.value >= maxSpamValue)
        {
            CompleteChallenge();
            Debug.Log("complete first challenge");
        }
    }
    void CompleteChallenge()
    {
        isChallengeCompleted = true;
        Debug.Log("Challenge Completed!");
        weakpointsButton.interactable = false;
        StartCoroutine(ShowMiniGameScreen());

    }


    IEnumerator RestartChallenge()
    {
        Debug.Log("Blackout! Restarting Challenge...");
        yield return new WaitForSeconds(1f);

        spamMeterSlider.value = 0;
        if (vignette != null) vignette.intensity.value = 0.2f;
        if (chromaticAberration != null) chromaticAberration.intensity.value = 0f;

        isBlackingOut = false;
        isSpamming = false;
        lastSpamTime = Time.time;
    }

    IEnumerator PanicEffect()
    {
        isPanicking = true;

        while (isPanicking)
        {
            float randomDelay = Random.Range(0.1f, 0.5f);
            chromaticAberration.intensity.value = 1f;

            yield return new WaitForSeconds(0.5f);
            chromaticAberration.intensity.value = 0.4f;

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

        chromaticAberration.intensity.value = targetValue;
    }

    IEnumerator ShowMiniGameScreen()
    {
        yield return new WaitForSeconds(3f);
        miniGameScreen.SetActive(true);
        secondSlider.SetActive(true);
        firstSlider.SetActive(false);

    }
}
