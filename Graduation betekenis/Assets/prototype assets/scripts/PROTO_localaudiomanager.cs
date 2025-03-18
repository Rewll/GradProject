using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class localAudiomanager : MonoBehaviour
{
    public PROTO_geluid[] geluiden;

    void geluidsLaagInit()
    {
        foreach (PROTO_geluid g in geluiden)
        {
            g.source = gameObject.AddComponent<AudioSource>();
            g.source.clip = g.geluidsClip;
            g.source.volume = g.volume;
            g.source.loop = g.loop;
            g.source.mute = g.muted;
            g.source.playOnAwake = g.playOnAwake;
        }
    }

    void Awake()
    {
        geluidsLaagInit();
    }

    public PROTO_geluid vindGeluid(string geluidsNaam)
    {

        PROTO_geluid g = Array.Find(geluiden, item => item.NaamClip == geluidsNaam);
        if (g == null)
        {
            //Debug.LogWarning("Geluid: " + name + " niet gevonden!");
            return null;
        }
        else if (g.source == null)
        {
            //Debug.LogWarning("Source van geluid niet gevonden!");
            return null;
        }
        else
        {
            return g;
        }
    }

    public void Speel(string geluidsIndex)
    {
        //Debug.Log("spelen!");
        PROTO_geluid g = vindGeluid(geluidsIndex);
        g.source.Play();
    }

    public void StopSpelen(string geluidsIndex)
    {
        PROTO_geluid g = vindGeluid(geluidsIndex);
        g.source.Stop();
    }
}