using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Emotion
{
    HAPPY,
    NEUTRAL,
    ANGRY,
    RAGE,
    GONE
}

public class CharacterMood : MonoBehaviour
{
    [SerializeField]
    private Emotion currentEmotion = Emotion.NEUTRAL;

    [SerializeField]
    private float timeUntilAngry = 15f;
    [SerializeField]
    private float timeUntilRage = 25f;
    [SerializeField]
    private float timeUntilGone = 30f;
    [SerializeField]
    private Dictionary<Emotion, float> emotionSliderDecrease = new Dictionary<Emotion, float>
{
    { Emotion.ANGRY, -0.05f },
    { Emotion.RAGE, -0.1f },
    { Emotion.GONE, -0.2f }
};
    private ParentQueue currentQueue;

    private SliderMood slider;

    private Coroutine moodCoroutine;
    private void Start()
    {
        StartCoroutine(MoodChecker());
        slider = FindObjectOfType<SliderMood>();
    }

    private IEnumerator MoodChecker()
    {
        yield return new WaitForSeconds(timeUntilAngry);
        SetEmotion(Emotion.ANGRY);

        yield return new WaitForSeconds(timeUntilRage - timeUntilAngry);
        SetEmotion(Emotion.RAGE);

        yield return new WaitForSeconds(timeUntilGone - timeUntilRage);
        SetEmotion(Emotion.GONE);
        Leave();
    }

    void Leave()
    {
        if (currentQueue != null)
        {
            currentQueue.RemoveCharacterFromQueue(GetComponent<Character>());
        }
        else
        {
            Debug.LogWarning("CharacterMood: currentQueue is null, cannot remove character.");
        }
    }

    private void SetEmotion(Emotion newEmotion)
    {
        if (currentEmotion != newEmotion)
        {
            currentEmotion = newEmotion;
            Debug.Log("New mood: " + newEmotion);

            if (slider != null && emotionSliderDecrease.ContainsKey(currentEmotion))
            {
                slider.SetTargetValue(emotionSliderDecrease[currentEmotion]);
            }
            else
            {
                Debug.LogWarning("SliderMood not found or emotion not in dictionary.");
            }
        }
    }

    public void ResetTimer()
    {
        if (moodCoroutine != null)
        {
            StopCoroutine(moodCoroutine);
        }
        currentEmotion = Emotion.HAPPY;
        moodCoroutine = StartCoroutine(MoodChecker());
    }

    public void SetQueue(ParentQueue queue)
    {
        currentQueue = queue;
    }
}
