using UnityEngine;
using UnityEngine.UI; // Added for Slider
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System.Collections;

public class TutorialMechanics : MonoBehaviour
{
    [SerializeField] static float struggleMeter = 0f;
    [SerializeField] float struggleIncrease = 3f;
    [SerializeField] float maxStruggle = 100f;
    [SerializeField] GameObject miniGameScreen;
    [SerializeField] Slider spamMeterSlider; // UI Slider for spam tracking

    private Volume globalVolume;
    private ChromaticAberration chromaticAberration;
    private Vignette vignette;
    private float lastKeyPressTime = 0f;
    private float maxSpamThreshold = 0.3f;
    private int spamCount = 0;
    private int requiredSpamCount = 10;
    private float spamDecreaseRate = 2f; // How fast the slider lowers

    private bool isPanicking = false;
    private bool isEscaped = false;
    private bool stepOneDone = false;

    public bool isMoving = true;

    void Start()
    {
        miniGameScreen.SetActive(false);
        struggleIncrease = 3f;
        globalVolume = FindFirstObjectByType<Volume>();

        if (globalVolume != null && globalVolume.profile != null)
        {
            globalVolume.profile.TryGet(out chromaticAberration);
            globalVolume.profile.TryGet(out vignette);

            struggleMeter = 0f;

            if (vignette != null)
                vignette.intensity.value = 0.125f;
        }


    }

    void Update()
    {
        if (spamMeterSlider != null)
        {
            spamMeterSlider.maxValue = requiredSpamCount;
            spamMeterSlider.value = 1f;  
            StartCoroutine(SmoothSliderDecrease(0f, 5f));  
        }
        isMoving = false;

        if (struggleMeter < maxStruggle)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                if (hit.collider != null && hit.collider.CompareTag("Weakpoints"))
                {
                    struggleMeter += struggleIncrease;
                    struggleMeter = Mathf.Clamp(struggleMeter, 0, maxStruggle);

                    if (vignette != null)
                        vignette.intensity.value = 0.125f + (struggleMeter / maxStruggle) * 0.5f;

                    if (vignette.intensity.value >= 0.6f)
                    {
                        miniGameScreen.SetActive(true);
                        StartCoroutine(SmoothVignetteTransition(0.2f, 1f));
                        stepOneDone = true;
                    }

                    if (vignette.intensity.value >= 0.4f && !isPanicking)
                    {
                        StartCoroutine(PanicEffect());
                    }
                }
            }
        }

        if (stepOneDone)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                float currentTime = Time.time;
                float timeSinceLastPress = currentTime - lastKeyPressTime;
                lastKeyPressTime = currentTime;

                if (timeSinceLastPress <= maxSpamThreshold)
                {
                    spamCount++;
                    spamCount = Mathf.Clamp(spamCount, 0, requiredSpamCount);
                }
                else
                {
                    spamCount = 0;
                }

                if (spamMeterSlider != null)
                    spamMeterSlider.value = spamCount;

                if (spamCount >= requiredSpamCount)
                {
                    isEscaped = true;
                }

                if (isEscaped)
                {
                    Debug.Log("ESCAPED");
                    isMoving = true;
                    StopPanic();
                }
            }

            

        }
    }

    IEnumerator SmoothSliderDecrease(float targetValue, float duration)
    {
        float startValue = 10f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            spamMeterSlider.value = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            yield return null;
        }

        spamMeterSlider.value = targetValue;
    }

    IEnumerator SmoothVignetteTransition(float targetValue, float duration)
    {
        float startValue = vignette.intensity.value;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            vignette.intensity.value = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            yield return null;
        }

        vignette.intensity.value = targetValue;
    }

    IEnumerator SmoothChromaticAberrationTransition(float targetValue, float duration)
    {
        float startValue = vignette.intensity.value;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            chromaticAberration.intensity.value = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            yield return null;
        }

        chromaticAberration.intensity.value = targetValue;
    }

    IEnumerator PanicEffect()
    {
        isPanicking = true;

        while (isPanicking)
        {
            float randomDelay = Random.Range(0.1f, .5f);
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


}
