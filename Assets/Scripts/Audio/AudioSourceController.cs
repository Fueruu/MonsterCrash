using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSourceController : MonoBehaviour
{

    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioMixerGroup mixerGroup;

    [SerializeField] [Range(-80f, 0f)] private float volume;
    [SerializeField] [Range(-12f, 0f)] private float randomVolMin;
    [SerializeField] [Range(0f, 12f)] private float randomVolMax;

    [SerializeField] [Range(-24f, 24f)] private float pitch;
    [SerializeField] [Range(-24f, 0f)] private float randomPitchMin;
    [SerializeField] [Range(-24f, 4f)] private float randomPitchMax;

    public void Play()
    {
        AudioClip clipToPlay = sounds[Random.Range(0, sounds.Length)];
        source.clip = clipToPlay;

        source.outputAudioMixerGroup = mixerGroup;

        float finalVolum = volume + Random.Range(randomVolMin, randomVolMax);
        finalVolum = AudioConvertionUtilities.dbtoLinear(finalVolum);   //  convert to linear for audio source
        source.volume = finalVolum;

        float finalPitch = pitch + Random.Range(randomPitchMin, randomPitchMax);
        finalPitch = AudioConvertionUtilities.stToPitch(finalPitch);   //  convert to linear for audio source
        source.pitch = finalPitch;

        source.Play();
    }

    public void PlayOneShot()
    {
        AudioClip clipToPlay = sounds[Random.Range(0, sounds.Length)];
        source.clip = clipToPlay;

        source.outputAudioMixerGroup = mixerGroup;

        float finalVolum = volume + Random.Range(randomVolMin, randomVolMax);
        finalVolum = AudioConvertionUtilities.dbtoLinear(finalVolum);   //  convert to linear for audio source
        source.volume = finalVolum;

        float finalPitch = pitch + Random.Range(randomPitchMin, randomPitchMax);
        finalPitch = AudioConvertionUtilities.stToPitch(finalPitch);   //  convert to linear for audio source
        source.pitch = finalPitch;

        source.PlayOneShot(clipToPlay);
    }

    public void Stop()
    {
        source.Stop();
    }

    public bool IsPlaying()
    {
        return source.isPlaying;
    }
}
