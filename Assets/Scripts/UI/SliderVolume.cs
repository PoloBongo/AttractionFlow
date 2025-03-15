using UnityEngine;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour
{
    private Slider sliderVolume;

    private void Start()
    {
        sliderVolume = GetComponent<Slider>();
        if (sliderVolume != null)
        {
            sliderVolume.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
            sliderVolume.value = AudioManager.InstanceAudioManager.GetCurrentVolume();
        }
    }

    public void ValueChangeCheck()
    {
        AudioManager.InstanceAudioManager.OnVolumeSliderChangedOverride(sliderVolume.value);
    }
}
