using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class PROTO_geluid
{
    public string NaamClip;
    public AudioClip geluidsClip;
    [Range(0f, 1f)]
    public float volume;
    public bool loop;
    public bool playOnAwake;
    public bool muted;

    [HideInInspector]
    public AudioSource source;
}
