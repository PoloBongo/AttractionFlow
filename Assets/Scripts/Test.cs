using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private InputActionReference pressActions;
    [Header("Input Actions")]
    [SerializeField] private OpenAttraction selectedAttraction;
    [SerializeField] private OpenAttraction selectedAttractionParent;
    private Camera camera;
    
    private void OnEnable()
    {
        pressActions.action.Enable();
    }
    
    private void OnDisable()
    {
        pressActions.action.Disable();
    }

    void Start()
    {
        camera = Camera.main;
    }

    public void OnTouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 touchPosition = context.ReadValue<Vector2>();

            Ray ray = camera.ScreenPointToRay(touchPosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Objet touché : " + hit.collider.gameObject.name);
                selectedAttraction = hit.collider.gameObject.GetComponent<OpenAttraction>();
                selectedAttraction = hit.collider.gameObject.GetComponentInParent<OpenAttraction>();
                
                if (selectedAttraction) selectedAttraction.InteractAttraction();
                if (selectedAttractionParent) selectedAttractionParent.InteractAttraction();
                else
                {
                    Debug.Log("n'arrive pas a get le comp");
                }
            }
        }
    }
}
