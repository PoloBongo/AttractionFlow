using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Property")]
    public static AudioManager InstanceAudioManager; 
    [SerializeField] private AudioClip actualClip;
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        Time.timeScale = 1f;
        
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
    
    public void SetAudioClip(AudioClip _clip)
    {
        actualClip = _clip;
        PlayAudioClip();
    }
}
