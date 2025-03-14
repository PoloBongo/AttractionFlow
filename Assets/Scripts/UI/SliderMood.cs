using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMood : MonoBehaviour
{
    [Header("Property")] [SerializeField] private Slider sliderMood;
    void Start()
    {
        
    }

    public void OnValueChanged()
    {
        Debug.Log(sliderMood.value);
    }
}
