using UnityEngine;
using UnityEngine.Rendering;

public class AmbiencePlaylist : MonoBehaviour
{
    AudioSource ambience;

    [Range(0, 1)] public float soundVolume = 0.5f;

    [SerializeField] AudioClip insane, sane;

    float t;
    void Start()
    {
        ambience = this.GetComponent<AudioSource>();


        if(ambience.clip == null) ambience.clip = sane; // Remove this when Sane is not the default mode when Audio Clip is empty
        ambience.Play(); // Remove this when you don't want the  music to play by default
    }

    // Update is called once per frame
    void Update()
    {
        volumeAdjust();
    }

    void volumeAdjust()
    {
        ambience.volume = soundVolume;
    }

    public void SaneAmbience()
    {
        ambience.volume = soundVolume;

        ambience.clip = sane;
        ambience.Play();
    }

    public void InsaneAmbience()
    {
        ambience.volume = soundVolume;

        ambience.clip = insane;
        ambience.Play();
    }
}
