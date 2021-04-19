using UnityEngine;

public class TouchDetector : MonoBehaviour
{
    public static TouchDetector Instance;

    public delegate void OnTouch(Vector3 touchPosition);
    public event OnTouch OnTouchEvent;

    private Camera mainCamera;

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 touchPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 1f));
            OnTouchEvent?.Invoke(touchPosition);
        }
    }
}
