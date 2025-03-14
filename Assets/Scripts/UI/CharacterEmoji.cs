using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterEmoji : MonoBehaviour
{

    [Header("Property")][SerializeField] private Image handle;
    [Header("Emoji")][SerializeField] private List<Sprite> moodTexture;

    void Update()
    {
        this.transform.rotation = Quaternion.Euler(-90.0f, 0.0f, this.transform.parent.rotation.z * -1.0f + 90);
    }
    public void UpdateUI(Emotion emotion)
    {
        int emotionID = (int)emotion;
        if (emotionID > 3)
        {
            emotionID = 3;
        }
        handle.sprite = moodTexture[emotionID];
    }

    public void Reset()
    {
        handle.sprite = moodTexture[1];
    }
}
