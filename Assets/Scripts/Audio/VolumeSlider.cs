using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{

    [SerializeField] private AudioMixer _AudioMixer;
    [SerializeField] private string _ParameterName;
    [SerializeField] private Vector2 _VolumeRange = new Vector2(-80f, 20f);
    [SerializeField] private int _DefaultValue = 100;

    private Slider _slider;

    // Use this for initialization
    private void Start()
    {
        _slider = GetComponent<Slider>();

        if (!PlayerPrefs.HasKey(_ParameterName))
        {
            PlayerPrefs.SetInt(_ParameterName, _DefaultValue);
        }

        //  getting our slider object and setting the player pref
        _slider.value = PlayerPrefs.GetInt(_ParameterName);
    }

    // Update is called once per frame
    private void Update()
    {
        //  create volume slider between min and max range (x and y)
        float mixerValue = Mathf.Lerp(_VolumeRange.x, _VolumeRange.y, _slider.value / 100);
        _AudioMixer.SetFloat(_ParameterName, mixerValue);

        //  saves the player pref to the disk 
        PlayerPrefs.SetInt(_ParameterName, (int)_slider.value);
    }
}
