using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class MusicManager {

    static GameObject playbackObject;

    private static AudioSource GetPlaybackSource()
    {
        if(playbackObject == null)
        {
            playbackObject = new GameObject("Music Playback");
            playbackObject.AddComponent<AudioSource>();
            GameObject.DontDestroyOnLoad(playbackObject);
        }
        return playbackObject.GetComponent<AudioSource>();
    }

    public static void PlayMusicClip(AudioClip musicClip, float volume, bool looping, AudioMixerGroup mixerGroup)
    {
        AudioSource source = GetPlaybackSource();

        if(source.clip == musicClip)
        {
            return;
        }

        source.clip = musicClip;
        source.volume = AudioConvertionUtilities.dbtoLinear(volume);
        source.loop = looping;
        source.outputAudioMixerGroup = mixerGroup;

        source.Play();
    }

    public static void StopMusic()
    {
        AudioSource source = GetPlaybackSource();
        source.Stop();
    }
}
