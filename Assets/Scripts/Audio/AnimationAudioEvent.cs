using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAudioEvent : MonoBehaviour {

    [SerializeField] private AudioSourceController audioSource;

    // private void Start()
    // {
    //     audioSource = GetComponentInParent<AudioSourceController>();
    // }

    public void OnStep()
    {
        this.audioSource.Play();
    }

}
