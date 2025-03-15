using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class DOTweenText : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TMP_Text tmp_text;

    [SerializeField] private Vector3 scaleStart;
    [SerializeField] private Vector3 scaleEnd;
    [SerializeField] private float duration;
    [SerializeField] private float maxY;
    [SerializeField] private int money;

    [SerializeField] private AudioClip moneySound;
    
    public GameObject textPrefab;
    public Transform canvasTransform;

    public void PlayText(int _money, GameObject spawnText)
    {
        GameObject newText = Instantiate(textPrefab, canvasTransform); // Crée un nouveau texte
        RectTransform rectTransform = newText.GetComponent<RectTransform>();
        TMP_Text tmpText = newText.GetComponent<TMP_Text>();

        rectTransform.position = spawnText.transform.position;
        tmpText.text = _money.ToString();

        AudioManager.InstanceAudioManager.SetAudioClip(moneySound);

        // Animation
        rectTransform.DOMoveY(rectTransform.position.y + maxY, duration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() => Destroy(newText)); // Détruit le texte après animation
    }
}
