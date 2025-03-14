using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class GestionTouch : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private OpenAttraction selectedAttraction;
    private Camera _camera;
    [SerializeField] private float speed = 1f;
    private Vector3 velocity = Vector3.zero;
    private bool isTouching = false;

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
        _camera = Camera.main;
        Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
    }

    public void Update()
    {
        isTouching = false;

        foreach (var touch in Touch.activeTouches)
        {
            if (touch.isTap)
            {
                Ray ray = _camera.ScreenPointToRay(touch.screenPosition);
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
            else if (touch.inProgress)
            {
                isTouching = true;
                Vector2 delta = touch.delta;
                Vector3 targetPosition = new Vector3(delta.y * Time.deltaTime * speed, 0, -delta.x * Time.deltaTime * speed);
                velocity = Vector3.Lerp(velocity, targetPosition, 25f * Time.deltaTime);
            }
        }

        if (!isTouching)
        {
            velocity = Vector3.Lerp(velocity, Vector3.zero, 5f * Time.deltaTime);
        }

        _camera.transform.position += velocity;
    }
}