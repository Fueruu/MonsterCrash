using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOneShot : MonoBehaviour
{
    [SerializeField] private AudioSourceController audioSource = null;

    private void Awake()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot();
        }
    }
}
