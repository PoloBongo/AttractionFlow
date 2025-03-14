using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [Header("Input Actions")]
    [SerializeField] private OpenAttraction selectedAttraction;
    private Camera camera;
    [SerializeField] private float speed = 1f;
    
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
            if (touch.isTap)
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
            else if (touch.inProgress)
            {
                Vector2 delta = touch.delta;
                camera.transform.position += new Vector3(delta.y * Time.deltaTime * speed, 0, -delta.x * Time.deltaTime * speed);
                camera.transform.position = new Vector3(Mathf.Clamp(camera.transform.position.x, -10, 10), camera.transform.position.y, Mathf.Clamp(camera.transform.position.z, -10, 10));
            }
        }
    }
}
