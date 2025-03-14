using System;
using System.Collections;
using UnityEditor.DeviceSimulation;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [Header("Input Actions")]
    [SerializeField] private OpenAttraction selectedAttraction;
    private Camera camera;

    private void Awake()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnEnable()
    {
        TouchSimulation.Enable();
    }

    private void Start()
    {
        camera = Camera.main;
    }

    public void Update()
    {
        foreach (var touch in Touch.activeTouches)
        {
            if (touch.ended)
            {
                Ray ray = camera.ScreenPointToRay(touch.screenPosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    Debug.Log("Objet touché : " + hit.collider.gameObject.name);
                    if (selectedAttraction && selectedAttraction.name != hit.collider.gameObject.name) selectedAttraction.Close();
                
                    selectedAttraction = hit.collider.gameObject.GetComponent<OpenAttraction>();

                    if (selectedAttraction)
                    {
                        selectedAttraction.InteractAttraction();
                    }
                    else
                    {
                        Debug.Log("n'arrive pas a get le comp");
                    }
                }
            }
        }
    }
}
