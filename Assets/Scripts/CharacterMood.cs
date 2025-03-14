using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Emotion
{
    HAPPY = 0,
    NEUTRAL = 1,
    ANGRY = 2,
    RAGE = 3,
    GONE = 4
}

public class CharacterMood : MonoBehaviour
{
    [SerializeField] private Emotion currentEmotion = Emotion.NEUTRAL;

    [Header("Timers d'émotion")]
    [SerializeField] private float timeUntilAngry = 15f;
    [SerializeField] private float timeUntilRage = 25f;
    [SerializeField] private float timeUntilGone = 30f;

    [Header("Référence externe")]
    [SerializeField] private SliderMood slider;

    private ParentQueue currentQueue;
    private Coroutine moodCoroutine;

    private static readonly Dictionary<Emotion, float> EmotionSliderImpact = new Dictionary<Emotion, float>
    {
        { Emotion.HAPPY,  0.1f },
        { Emotion.NEUTRAL, 0.0f },
        { Emotion.ANGRY,  -0.05f },
        { Emotion.RAGE,   -0.1f },
        { Emotion.GONE,   -0.2f }
    };

    private void Start()
    {
        slider = FindAnyObjectByType<SliderMood>();
        if (slider == null)
        {
            Debug.Log("SliderMood non assigné !");
        }

        moodCoroutine = StartCoroutine(MoodChecker());
    }

    private IEnumerator MoodChecker()
    {
        yield return ChangeMoodAfterDelay(Emotion.ANGRY, timeUntilAngry);
        yield return ChangeMoodAfterDelay(Emotion.RAGE, timeUntilRage - timeUntilAngry);
        yield return ChangeMoodAfterDelay(Emotion.GONE, timeUntilGone - timeUntilRage);
        Leave();
    }

    private IEnumerator ChangeMoodAfterDelay(Emotion newEmotion, float delay)
    {
        yield return new WaitForSeconds(delay);
        SetEmotion(newEmotion);
    }

    private void SetEmotion(Emotion newEmotion)
    {
        if (currentEmotion == newEmotion) return;

        currentEmotion = newEmotion;

        if (slider != null && EmotionSliderImpact.ContainsKey(newEmotion))
        {
            slider.SetTargetValue(EmotionSliderImpact[newEmotion]);
        }
        else
        {
            Debug.LogWarning($"SliderMood non trouvé ou émotion {newEmotion} non gérée.");
        }
    }

    private void Leave()
    {
        if (currentQueue != null)
        {
            Character character = GetComponent<Character>();
            if (character != null)
            {
                currentQueue.RemoveCharacterFromQueue(character);
                character.Leave();
            }
            else
            {
                Debug.LogWarning("Impossible de retirer : Character est null.");
            }
        }
        else
        {
            Debug.LogWarning("Impossible de retirer : currentQueue est null.");
        }
    }

    public void ResetTimer()
    {
        if (moodCoroutine != null)
        {
            StopCoroutine(moodCoroutine);
        }
        moodCoroutine = StartCoroutine(MoodChecker());
    }

    public void BeHappy()
    {
        ResetTimer();
        Emotion newEmotion = (Emotion)Mathf.Max((int)currentEmotion - 1, (int)Emotion.HAPPY);
        SetEmotion(newEmotion);
    }

    public void SetQueue(ParentQueue queue)
    {
        currentQueue = queue;
    }
}
