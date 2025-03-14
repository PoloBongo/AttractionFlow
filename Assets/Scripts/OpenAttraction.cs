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
        isOpen = true;
        fenceAnimator.SetBool(IsOpen, isOpen);
        PlayOpenAnimation();
        
        Debug.Log("Open Attraction" + fenceAnimator);
    }

    public void Close()
    {
        isOpen = false;
        fenceAnimator.SetBool(IsOpen, isOpen);
        PlayCloseAnimation();
        
        Debug.Log("Close Attraction" + fenceAnimator);
    }

    public void InteractAttraction()
    {
        if (!isOpen)
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
