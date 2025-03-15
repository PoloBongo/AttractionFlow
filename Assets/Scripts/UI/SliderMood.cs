using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SliderMood : MonoBehaviour
{
    [Header("Property")] [SerializeField] private Slider sliderMood;
    [Header("Property")] [SerializeField] private Image handle;
    [Header("Emoji")] [SerializeField] private List<Sprite> moodTexture;

    private const float duration = 0.5f;
    private float targetValue;

    [SerializeField] private GameObject win;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject defeat;
    [SerializeField] private GameObject canvas;
    [SerializeField] private ShowScoreEndGame showScoreEndGame;
    [SerializeField] private ShowScoreEndGame showScoreEndGame2;

    private void Start()
    {
        targetValue = sliderMood.value;
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

        switch (slider)
        {
            case <= 0f:
                canvas.SetActive(true);
                defeat.SetActive(true);
                pause.SetActive(false);
                showScoreEndGame.ShowText();
                Time.timeScale = 0f;
                break;
            case >= 1f:
                canvas.SetActive(true);
                win.SetActive(true);
                pause.SetActive(false);
                showScoreEndGame2.ShowText();
                Time.timeScale = 0f;
                break;
        }
    }

    private IEnumerator SmoothSlider()
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

    public void SetTargetValue(float targetValueDelta)
    {
        targetValue += targetValueDelta;
        StopAllCoroutines();
        StartCoroutine(SmoothSlider());
    }
}
