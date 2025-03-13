using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Espace appuyé !");
            animator.SetTrigger("EmoteYes");
        }
        
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Espace appuyé !");
            animator.SetTrigger("IsIdle");
        }
    }
}
