using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Property")]
    public static AudioManager Instance; 
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private AudioClip actualClip;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    private void PlayAudioClip()
    {
        audioSource.PlayOneShot(actualClip);
    }
    
    public void SetAudioClip(int _index)
    {
        actualClip = audioClips[_index];
        PlayAudioClip();
    }
}
