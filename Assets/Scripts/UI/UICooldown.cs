using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UICooldown : MonoBehaviour
{
    [Header("Property")]
    [SerializeField] private Slider sliderCooldown;
    [SerializeField] private Image sliderImage;

    public void UpdateSlider(float cooldown, float maxCooldown)
    {
        if (sliderCooldown == null || sliderImage == null)
        {
            Debug.LogWarning("Slider ou Image non assignés dans l'inspecteur !");
            return;
        }

        if (maxCooldown <= 0)
        {
            Debug.Log("maxCooldown doit être supérieur à 0 !");
            return;
        }

        sliderCooldown.value = cooldown / maxCooldown;
        sliderImage.color = Color.Lerp(Color.red, Color.green, sliderCooldown.value);
    }
}
