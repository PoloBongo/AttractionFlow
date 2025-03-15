using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Property")]
    public static AudioManager InstanceAudioManager; 
    [SerializeField] private AudioClip actualClip;
    [SerializeField] private AudioSource audioSource;
    private float currentVolume = 0.1f;

    private void Awake()
    {
        Time.timeScale = 1f;
        
        if (InstanceAudioManager != null && InstanceAudioManager != this)
        {
            Destroy(gameObject);
        }
        else
        {
            InstanceAudioManager = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void PlayAudioClip()
    {
        audioSource.PlayOneShot(actualClip);
    }
    
    public void SetAudioClip(AudioClip _clip)
    {
        actualClip = _clip;
        PlayAudioClip();
    }
    
    public void OnVolumeSliderChangedOverride(float _volume)
    {
        currentVolume = _volume;
        ApplyVolume();
    }

    private void ApplyVolume()
    {
        AudioListener.volume = currentVolume;
    }

    public float GetCurrentVolume()
    {
        return currentVolume;
    }
}
