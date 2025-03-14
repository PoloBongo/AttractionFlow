using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMood : MonoBehaviour
{
    [Header("Property")] [SerializeField] private Slider sliderMood;
    [Header("Property")] [SerializeField] private Image handle;
    [Header("Emoji")] [SerializeField] private List<Sprite> moodTexture;

    private const float duration = 0.5f;

    private void Start()
    {
        StartCoroutine(SmoothSlider(0.2f));
    }

    public void OnValueChanged()
    {
        CheckStatusMood(sliderMood.value);
    }

    private void CheckStatusMood(float slider)
    {
        if (slider > 0.8)
        {
            handle.sprite = moodTexture[0];
        }
        else if (slider < 0.8 && slider > 0.6)
        {
            handle.sprite = moodTexture[1];
        }
        else if (slider < 0.6 && slider > 0.3)
        {
            handle.sprite = moodTexture[2];
        }
        else if (slider < 0.3 && slider > 0.0)
        {
            handle.sprite = moodTexture[3];
        }
    }

    private IEnumerator SmoothSlider(float targetValue)
    {
        float startValue = sliderMood.value;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            sliderMood.value = Mathf.Lerp(startValue, targetValue, elapsedTime / duration);
            yield return null;
        }

        sliderMood.value = targetValue;
    }
}
