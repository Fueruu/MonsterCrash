using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class PlayMusicOnAwake : MonoBehaviour {

    public AudioClip musicClip;
    public AudioMixerGroup mixerGroup;
    [Range(-80f, 0f)]
    public float volume;
    public bool looping;


    private void Awake()
    {
        MusicManager.PlayMusicClip(musicClip, volume, looping, mixerGroup);
    }
}
