using UnityEngine;

public class LookAround : MonoBehaviour
{
    [Header("Assign in Inspector")]
    [SerializeField] private Transform playerBody;
    [SerializeField] private Transform cameraTransform;

    [Header("Settings")]
    [SerializeField] private float sensitivity = 150f;
    [SerializeField] private float minPitch = -90f;
    [SerializeField] private float maxPitch = 90f;
    [SerializeField] private bool invertY = false;

    private float pitch;

    void Start()
    {
        if (cameraTransform == null) cameraTransform = transform;
        if (playerBody == null)
        {
            Debug.LogError("LookAround: nie przypisano playerBody (obiekt gracza)!");
            enabled = false;
            return;
        }

        float startPitch = cameraTransform.localEulerAngles.x;
        if (startPitch > 180f) startPitch -= 360f;
        pitch = Mathf.Clamp(startPitch, minPitch, maxPitch);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;


        playerBody.Rotate(Vector3.up * mouseX, Space.Self);


        float ySign = invertY ? 1f : -1f;
        pitch += mouseY * ySign;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}
