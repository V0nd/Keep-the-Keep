﻿using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip audioClip;

    public bool loop;

    public bool playOnAwake;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch;

    [Range(0f, 1f)]
    public float spatialBlend;

    [HideInInspector]
    public AudioSource audioSource;
}
