using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class audioManager : MonoBehaviour
{
    public geluid[] soundsList;
    
    public static audioManager instance;
    
    public bool dontDestroy;

    void AudioLayerInit()
    {
        foreach (geluid g in soundsList)
        {
            g.source = gameObject.AddComponent<AudioSource>();
            g.source.clip = g.geluidsClip;
            g.source.volume = g.volume;
            g.source.loop = g.loop;
            g.source.mute = g.muted;
        }
    }

    void Awake()
    {
        /*if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }*/

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
        else if (g.source == null)
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

    public void PlaySound(int geluidsIndex)
    {
        geluid g = FindSound(geluidsIndex);
        g.source.Play();
    }

    public void StopPlaying(int geluidsIndex)
    {
        geluid g = FindSound(geluidsIndex);
        g.source.Stop();
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