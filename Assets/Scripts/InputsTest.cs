using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsTest : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference holdAction;
    [SerializeField] private InputActionReference positionAction;
    [SerializeField] private Vector2 inputPosition;
    [SerializeField] private float holdingDown;
    [SerializeField] private GameObject selectedObject;
    
    private void OnEnable()
    {
        holdAction.action.Enable();
        positionAction.action.Enable();
    }
    
    private void OnDisable()
    {
        holdAction.action.Disable();
        positionAction.action.Disable();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputPosition = positionAction.action.ReadValue<Vector2>();
        holdingDown = holdAction.action.ReadValue<float>();
        if (holdingDown == 0f) selectedObject = null;
        
        if (holdingDown != 0f)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(inputPosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("selection");
                selectedObject = hit.collider.gameObject;
                // selectedObject.transform.position = Camera.main.ScreenToWorldPoint(inputPosition);
            }
        }
        
        if (selectedObject != null)
        {
            Vector3 screenPosition = new Vector3(inputPosition.x, inputPosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            selectedObject.transform.position = worldPosition;
        }
    }
}
