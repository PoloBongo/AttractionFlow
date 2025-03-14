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
    [SerializeField] private SliderMood slider; // Assigné dans l'Inspector pour éviter FindObjectOfType

    private ParentQueue currentQueue;
    private Coroutine moodCoroutine;

    private static readonly Dictionary<Emotion, float> EmotionSliderDecrease = new Dictionary<Emotion, float>
    {
        { Emotion.HAPPY, 0.1f },
        { Emotion.NEUTRAL, 0.0f },
        { Emotion.ANGRY, -0.05f },
        { Emotion.RAGE, -0.1f },
        { Emotion.GONE, -0.2f }
    };

    private void Start()
    {
        if (slider == null)
        {
            Debug.LogWarning("SliderMood non assigné dans l'inspecteur !");
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

        float initialEmotionValue = EmotionSliderDecrease[currentEmotion];
        float newEmotionValue = EmotionSliderDecrease[newEmotion];

        currentEmotion = newEmotion;

        if (slider != null)
        {
            slider.SetTargetValue(newEmotionValue - initialEmotionValue);
        }
        else
        {
            Debug.LogWarning($"SliderMood non trouvé ou émotion {currentEmotion} non gérée.");
        }
    }

    private void Leave()
    {
        if (currentQueue != null)
        {
            Character character = GetComponent<Character>();
            currentQueue.RemoveCharacterFromQueue(character);
            character.Leave();
        }
        else
        {
            Debug.LogWarning("Impossible de retirer le personnage : currentQueue est null.");
        }
    }

    public void ResetTimer()
    {
        if (moodCoroutine != null)
        {
            StopCoroutine(moodCoroutine);
            moodCoroutine = StartCoroutine(MoodChecker());
        }
    }

    public void BeHappy()
    {
        ResetTimer();
        SetEmotion(currentEmotion - 1);
    }

    public void SetQueue(ParentQueue queue)
    {
        currentQueue = queue;
    }
}
