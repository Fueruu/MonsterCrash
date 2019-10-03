using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LoadSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _AudioMixer;
    [SerializeField] private string[] _AudioParameters;
    [SerializeField] private Vector2 _VolumeRange = new Vector2(-80f, 20f);


    private void Start()
    {
        for (int i = 0; i < _AudioParameters.Length; i++)
        {
            string parameter = _AudioParameters[i];
            float getSavedValue = PlayerPrefs.GetInt(parameter);
            float mixerValue = Mathf.Lerp(_VolumeRange.x, _VolumeRange.y, getSavedValue / 100);
            _AudioMixer.SetFloat(parameter, mixerValue);
        }
    }

}
