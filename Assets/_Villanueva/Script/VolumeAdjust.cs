using UnityEngine;
using UnityEngine.Rendering;

public class VolumeAdjust : MonoBehaviour
{
    AudioSource ambienceLoop;
    [Range(0, 1)] public float soundVolume = 0.5f;

    float t;
    void Start()
    {
        ambienceLoop = this.GetComponent<AudioSource>();
        ambienceLoop.Play();
    }

    // Update is called once per frame
    void Update()
    {
        volumeAdjust();
    }

    void volumeAdjust()
    {
        ambienceLoop.volume = soundVolume;
    }

    public void audioFadeOut()
    {
        for(t = 0; ambienceLoop.volume > 0; t=t)
        {
            ambienceLoop.volume = Mathf.Lerp(soundVolume, 0, t);

            Debug.Log(ambienceLoop.volume);

            t += 0.05f * Time.deltaTime;
        }
    }
}
