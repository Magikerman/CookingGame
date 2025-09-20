using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MichaelCameraMovement : MonoBehaviour
{
    private float xRotation;
    private float yRotation;

    [SerializeField] private Vector2 sensitivity;

    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform playerModel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity.x;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity.y;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        playerModel.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
