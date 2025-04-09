using UnityEngine;
using UnityEngine.Rendering;

public class AmbiencePlaylist : MonoBehaviour
{
    AudioSource ambience;
    VolumeAdjust volumeAdjust;
    public AudioClip insane, sane;

    float t;
    void Start()
    {
        ambience = this.GetComponent<AudioSource>();
        volumeAdjust = this.GetComponent<VolumeAdjust>();

        SaneAmbience();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaneAmbience()
    {
        for(t = 0; volumeAdjust.soundVolume > 0; t=t)
        {
            ambience.volume = Mathf.Lerp(volumeAdjust.soundVolume, 0, t);

            t += 0.05f * Time.deltaTime;
        }

        ambience.volume = volumeAdjust.soundVolume;

        ambience.clip = sane;
        ambience.Play();
    }

    public void InsaneAmbience()
    {
        for(t = 0; volumeAdjust.soundVolume > 0; t=t)
        {
            ambience.volume = Mathf.Lerp(volumeAdjust.soundVolume, 0, t);

            t += 0.05f * Time.deltaTime;
        }

        ambience.volume = volumeAdjust.soundVolume;

        ambience.clip = insane;
        ambience.Play();
    }
}
