using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class geluid
{
    public string NaamClip;
    public AudioClip geluidsClip;
    [Range(0f, 1f)]
    public float volume;
    public bool loop;
    public bool muted;

    [HideInInspector]
    public AudioSource source;
}
