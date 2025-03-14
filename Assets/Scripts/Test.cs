using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [Header("Input Actions")]
    [SerializeField] private InputActionReference pressActions;
    [SerializeField] private InputActionReference holdActions;
    [SerializeField] private InputActionReference deltaActions;
    [SerializeField] private OpenAttraction selectedAttraction;
    [SerializeField] private OpenAttraction selectedAttractionParent;
    private Camera camera;
    private bool isHolding = false;
    
    private void OnEnable()
    {
        pressActions.action.Enable();
        holdActions.action.Enable();
        deltaActions.action.Enable();
    }
    
    private void OnDisable()
    {
        pressActions.action.Disable();
        holdActions.action.Disable();
        deltaActions.action.Disable();
    }

    void Start()
    {
        camera = Camera.main;
    }

    public void OnTouch(InputAction.CallbackContext context)
    {
        if (context.performed && context.duration < 0.1f)
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

    public void OnHold(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isHolding = true;
        }
        else if (context.canceled)
        {
            isHolding = false;
        }
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        if (isHolding && context.performed)
        {
            Vector2 delta = context.ReadValue<Vector2>();
            camera.transform.position += new Vector3(-delta.x * Time.deltaTime * 3f, 0, -delta.y * Time.deltaTime * 3f);
            camera.transform.position = new Vector3(Mathf.Clamp(camera.transform.position.x, -10, 10), camera.transform.position.y, Mathf.Clamp(camera.transform.position.z, -10, 10));
        }
    }
}
