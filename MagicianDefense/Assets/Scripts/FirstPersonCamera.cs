using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;

    bool lockedCursor = true;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;   
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X")* mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;


        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 80f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        player.Rotate(Vector3.up * inputX);
    }
}
