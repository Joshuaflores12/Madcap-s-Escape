using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class TutorialMechanics : MonoBehaviour
{
    public static float struggleMeter = 0f;
    public float struggleIncrease = 10f;
    public float maxStruggle = 100f;

    private Volume globalVolume;
    private ChromaticAberration chromaticAberration;
    private Vignette vignette;

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
                vignette.intensity.value = 0.538f;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            struggleMeter += struggleIncrease;
            struggleMeter = Mathf.Clamp(struggleMeter, 0, maxStruggle);

            if (chromaticAberration != null)
                chromaticAberration.intensity.value = struggleMeter / maxStruggle;

            if (vignette != null)
                vignette.intensity.value = 0.3f + (struggleMeter / maxStruggle) * 0.5f;
        }
    }
}
