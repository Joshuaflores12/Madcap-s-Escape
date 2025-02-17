using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System.Collections;

public class TutorialMechanics : MonoBehaviour
{
    [SerializeField] static float struggleMeter = 0f;
    [SerializeField] float struggleIncrease = 10f;
    [SerializeField] float maxStruggle = 0.6f;

    private Volume globalVolume;
    private ChromaticAberration chromaticAberration;
    private Vignette vignette;

    private bool isPanicking = false;

    void Start()
    {
        struggleIncrease = 1f;
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
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Weakpoints"))
            {
                Debug.Log("weak point is hit");
                struggleMeter += struggleIncrease;
                struggleMeter = Mathf.Clamp(struggleMeter, 0, maxStruggle);

                if (vignette != null)
                    vignette.intensity.value = 0.125f + (struggleMeter / maxStruggle) * 0.5f;

                if (vignette.intensity.value >= 0.6f)
                {
                    Debug.Log("Player escaped straitjacket");
                    Debug.Log("vignette value: " + vignette.intensity.value);
                }

                if (vignette.intensity.value >= 0.2f && !isPanicking)
                {
                    StartCoroutine(PanicEffect());
                }

                if (vignette.intensity.value >= 0.6f)
                {
                    StopPanic();
                }
            }
        }
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
        chromaticAberration.intensity.value = 0f; 
    }
}
