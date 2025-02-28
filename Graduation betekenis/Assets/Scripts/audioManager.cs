using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public geluid[] geluidLaag0;
    public geluid[] geluidLaag1;

    public static audioManager instance;

    void geluidsLaagInit(geluid[] geluidLijst)
    {
        foreach (geluid g in geluidLijst)
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
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);

        geluidsLaagInit(geluidLaag0);
        geluidsLaagInit(geluidLaag1);
    }

    public geluid vindGeluid(int geluidsLaag, int geluidsIndex)
    {
        geluid[] geluidLijst;
        if (geluidsLaag == 0)
        {
            geluidLijst = geluidLaag0;
        }
        else if (geluidsLaag == 1)
        {
            geluidLijst = geluidLaag1;
        }
        else
        {
            Debug.LogWarning("Geluidslaag: " + geluidsLaag + " niet gevonden!");
            return null;
        }
        geluid g = geluidLijst[geluidsIndex];

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

    public void stopAlleGeluidenInLaag(int geluidsLaag)
    {
        geluid[] lijstOmUitTeZoeken = null;

        switch (geluidsLaag)
        {
            case 0:
                lijstOmUitTeZoeken = geluidLaag0;
                break;
            case 1:
                lijstOmUitTeZoeken = geluidLaag1;
                break;
        }

        foreach (geluid g in lijstOmUitTeZoeken)
        {
            if (g.source.isPlaying)
            {
                g.source.Stop();
            }
        }
    }

    public void Speel(int geluidsLaag, int geluidsIndex)
    {
        geluid g = vindGeluid(geluidsLaag, geluidsIndex);
        g.source.Play();
    }

    public void StopSpelen(int geluidsLaag, int geluidsIndex)
    {
        geluid g = vindGeluid(geluidsLaag, geluidsIndex);
        g.source.Stop();
    }

    public float getAfspeelTijd(int geluidsLaag, int geluidsIndex)
    {
        geluid g = vindGeluid(geluidsLaag, geluidsIndex);
        return g.source.time;       
    }
    public float getClipLength(int geluidsLaag, int geluidsIndex)
    {
        geluid g = vindGeluid(geluidsLaag, geluidsIndex);
        return g.geluidsClip.length;
    }
}