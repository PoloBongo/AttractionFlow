using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.Serialization;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class GestionTouch : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private OpenAttraction selectedAttraction;
    private Camera _camera;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float speed = 1f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private float velocityLerp;
    [SerializeField] private float velocityDecreaseLerp;
    private bool isTouching = false;
    [SerializeField] private Vector2 cameraBoundsX;
    [SerializeField] private Vector2 cameraBoundsY;
    [SerializeField] private Vector2 cameraBoundsZ;
    [SerializeField] private float cameraBoundsSpeed;

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

        if (Touch.activeTouches.Count == 2)
        {
            Touch touch0 = Touch.activeTouches[0];
            Touch touch1 = Touch.activeTouches[1];
            
            Vector2 touch0PrevPos = touch0.screenPosition - touch0.delta;
            Vector2 touch1PrevPos = touch1.screenPosition - touch1.delta;
            
            float prevTouchDeltaMag = (touch0PrevPos - touch1PrevPos).magnitude;
            float touchDeltaMag = (touch0.screenPosition - touch1.screenPosition).magnitude;
            float difference = prevTouchDeltaMag - touchDeltaMag;
            
            Vector3 targetPosition = new Vector3(0, difference * Time.deltaTime * zoomSpeed, 0);
            velocity = Vector3.Lerp(velocity, targetPosition, velocityLerp * Time.deltaTime);
        }
        
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
                velocity = Vector3.Lerp(velocity, targetPosition, velocityLerp * Time.deltaTime);
            }
        }

        if (!isTouching)
        {
            velocity = Vector3.Lerp(velocity, Vector3.zero, velocityDecreaseLerp * Time.deltaTime);
        }

        _camera.transform.position += velocity;
        ClampCamera();
    }

    private void ClampCamera()
    {
        if (_camera.transform.position.x > cameraBoundsX[1] || _camera.transform.position.x < cameraBoundsX[0])
        {
            float posX = Mathf.Lerp(_camera.transform.position.x, _camera.transform.position.x > cameraBoundsX[1] ? cameraBoundsX[1] : cameraBoundsX[0], cameraBoundsSpeed * Time.deltaTime);
            Vector3 pos = new Vector3(posX, _camera.transform.position.y, _camera.transform.position.z);
            _camera.transform.position = pos;
        }
        
        if (_camera.transform.position.y > cameraBoundsY[1] || _camera.transform.position.y < cameraBoundsY[0])
        {
            float posY = Mathf.Lerp(_camera.transform.position.y, _camera.transform.position.y > cameraBoundsY[1] ? cameraBoundsY[1] : cameraBoundsY[0], cameraBoundsSpeed * Time.deltaTime);
            Vector3 pos = new Vector3(_camera.transform.position.x, posY, _camera.transform.position.z);
            _camera.transform.position = pos;
        }
        
        if (_camera.transform.position.z > cameraBoundsZ[1] || _camera.transform.position.z < cameraBoundsZ[0])
        {
            float posZ = Mathf.Lerp(_camera.transform.position.z, _camera.transform.position.z > cameraBoundsZ[1] ? cameraBoundsZ[1] : cameraBoundsZ[0], cameraBoundsSpeed * Time.deltaTime);
            Vector3 pos = new Vector3(_camera.transform.position.x, _camera.transform.position.y, posZ);
            _camera.transform.position = pos;
        }
    }
}