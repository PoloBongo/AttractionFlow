using System.Collections;
using UnityEngine;

enum Emotion
{
    HAPPY,
    NEUTRAL,
    ANGRY,
}

public class Character1 : MonoBehaviour
{
    [SerializeField]
    private Emotion currentEmotion = Emotion.HAPPY;

    [SerializeField]
    private float timeUntilNeutral = 15f;
    [SerializeField]
    private float timeUntilAngry = 30f;

    private void Start()
    {
        StartCoroutine(MoodChecker());
    }

    private IEnumerator MoodChecker()
    {
        yield return new WaitForSeconds(timeUntilNeutral);
        SetEmotion(Emotion.NEUTRAL);

        yield return new WaitForSeconds(timeUntilAngry - timeUntilNeutral);
        SetEmotion(Emotion.ANGRY);
    }

    private void SetEmotion(Emotion newEmotion)
    {
        if (currentEmotion != newEmotion)
        {
            currentEmotion = newEmotion;
            Debug.Log("New mood: " + newEmotion);
        }
    }

    public void ResetTimer()
    {
        StopAllCoroutines();
        currentEmotion = Emotion.HAPPY;
        StartCoroutine(MoodChecker());
    }
}
