using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Property")]
    public static AudioManager InstanceAudioManager; 
    [SerializeField] private List<AudioClip> audioClips;
    [SerializeField] private AudioClip actualClip;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (InstanceAudioManager != null && InstanceAudioManager != this)
        {
            Destroy(this);
        }
        else
        {
            InstanceAudioManager = this;
            DontDestroyOnLoad(InstanceAudioManager);
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
