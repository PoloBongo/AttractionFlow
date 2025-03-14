using System.Collections;
using UnityEngine;

enum Emotion
{
    HAPPY,
    NEUTRAL,
    ANGRY,
    GONE
}

public class CharacterMood : MonoBehaviour
{
    [SerializeField]
    private Emotion currentEmotion = Emotion.HAPPY;

    [SerializeField]
    private float timeUntilNeutral = 15f;
    [SerializeField]
    private float timeUntilAngry = 25f;
    [SerializeField]
    private float timeUntilGone = 35f;

    private ParentQueue currentQueue;

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

        yield return new WaitForSeconds(timeUntilGone - timeUntilAngry);
        SetEmotion(Emotion.GONE);
        Leave();
    }

    void Leave()
    {
        currentQueue.RemoveCharacterFromQueue(GetComponent<Character>());
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

    public void SetQueue(ParentQueue queue)
    {
        currentQueue = queue;
    }
}
