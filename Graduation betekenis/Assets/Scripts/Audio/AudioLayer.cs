using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class AudioLayer : MonoBehaviour
{
    [SerializeField] private int layerNum;
    [SerializeField] AudioMixerGroup mixerGroup;
    public geluid[] soundsList;
    
    private bool dontDestroy;

    void AudioLayerInit()
    {
        foreach (geluid g in soundsList)
        {
            g.source = gameObject.AddComponent<AudioSource>();
            g.source.clip = g.geluidsClip;
            g.source.loop = g.loop;
            g.source.outputAudioMixerGroup = mixerGroup;
        }
    }

    void Awake()
    {
        if (dontDestroy)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        
        AudioLayerInit();
    }
    
    geluid FindSound(int geluidsIndex)
    {
        geluid g = soundsList[geluidsIndex];

        if (g == null)
        {
            Debug.LogWarning("Geluid: " + name + " niet gevonden!");
            return null;
        }
        else if (!g.source)
        {
            Debug.LogWarning("Source van geluid niet gevonden!");
            return null;
        }
        else
        {
            return g;
        }
    }

    public void StopAllSoundsInThisLayer()
    {
        foreach (geluid g in soundsList)
        {
            if (g.source.isPlaying)
            {
                g.source.Stop();
            }
        }
    }

    public bool Isplaying()
    {
        foreach (geluid g in soundsList)
        {
            if (g.source.isPlaying)
            {
                return true;
            }
        }

        return false;
    }
    public void PlaySound(int geluidsIndex)
    {
        geluid g = FindSound(geluidsIndex);
        g.source.volume = g.volume;
        g.source.Play();
    }

    public void StopPlaying(int geluidsIndex)
    {
        geluid g = FindSound(geluidsIndex);
        g.source.Stop();
    }
    public void FadeInSound(int geluidsIndex, float fadeTime)
    {
        geluid g = FindSound(geluidsIndex);
        StartCoroutine(FadeInRoutine(geluidsIndex, g.source, fadeTime));
    }
    
    IEnumerator FadeInRoutine(int geluidsIndex,AudioSource audioSource, float fadeTime) {
        PlaySound(geluidsIndex);
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < FindSound(geluidsIndex).volume) {
            audioSource.volume += Time.deltaTime / fadeTime;
            yield return null;
        }
    }

    public void FadeOutSound(int geluidsIndex, float fadeTime)
    {
        geluid g = FindSound(geluidsIndex);
        StartCoroutine(FadeOutRoutine(geluidsIndex, g.source, fadeTime));
    }
    
    IEnumerator FadeOutRoutine(int geluidsIndex,AudioSource audioSource, float fadeTime) {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }
        StopPlaying(geluidsIndex);
    }

    
    public float GetPlayTime(int geluidsIndex)
    {
        geluid g = FindSound(geluidsIndex);
        return g.source.time;       
    }
    public float GetClipLength(int geluidsIndex)
    {
        geluid g = FindSound(geluidsIndex);
        return g.geluidsClip.length;
    }
}