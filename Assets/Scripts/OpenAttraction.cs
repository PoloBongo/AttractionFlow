using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAttraction : MonoBehaviour
{
    [SerializeField]
    private bool isOpen = false;
    [Header("Animation")] [SerializeField] private Animator fenceAnimator;
    private static readonly int IsOpen = Animator.StringToHash("IsOpen");

    void Open()
    {
        fenceAnimator.SetBool(IsOpen, isOpen);
        PlayOpenAnimation();
        
        Debug.Log("Open Attraction");
    }

    void Close()
    {
        fenceAnimator.SetBool(IsOpen, isOpen);
        PlayCloseAnimation();
        
        Debug.Log("Close Attraction");
    }

    public void InteractAttraction()
    {
        isOpen = !isOpen;
        
        if (isOpen)
        {
            Open();
        }
        else
        {
            Close();
        }
        
    }

    void PlayOpenAnimation()
    {
        
    }

    void PlayCloseAnimation()
    {

    }

    public bool GetIsOpen()
    {
        return isOpen;
    }
}
