using UnityEngine;
using System.Collections;
using UnityEngine.Audio;


public class MixLevels : MonoBehaviour {

    public AudioMixer mixer;

    public void SetSfxLvl(float sfxLvl)
	{
        mixer.SetFloat("SFXVolControl", sfxLvl);
    }

    public void SetMusicLvl (float musicLvl)
	{
        mixer.SetFloat("MusicVolControl", musicLvl);
    }
}
