using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAttraction : MonoBehaviour
{
    [SerializeField]
    private bool isOpen = false;
    void Open()
    {
        isOpen = true;
        PlayOpenAnimation();
    }

    void Close()
    {
        isOpen = false;
        PlayCloseAnimation();
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
