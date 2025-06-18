using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup audioMixerGroup;
    [SerializeField] private string volumeParameterName;
    [SerializeField] private Slider audioSlider;
    
    public void Awake()
    {
        audioMixerGroup.audioMixer.GetFloat(volumeParameterName, out float volume);
        audioSlider.value = Mathf.Pow(10, volume/20);
        audioSlider.onValueChanged.AddListener(SetMixerVolume);
    }

    public void SetMixerVolume(float sliderValue)
    {
        audioMixerGroup.audioMixer.SetFloat(volumeParameterName,Mathf.Log10(sliderValue) * 20);
    }
}