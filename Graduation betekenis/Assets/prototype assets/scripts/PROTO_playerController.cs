using UnityEngine;

public class PROTO_playerController : MonoBehaviour
{
    [Header("Loop dingen")]
    CharacterController controller;
    float xAxis;
    float zAxis;
    public float speed;
    public bool lopenEnabled;
    public bool magLopen;
    [Space]
    [Header("Kijk dingen")]
    public Transform cameraTransform;
    public float mouseSens;
    public bool magKijken;
    float xRotation = 0;
    [Space]
    public bool cameraModus;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (lopenEnabled)
        {
            if (magLopen)
            {
                spelerBeweging();
            }
        }
        if (magKijken && !cameraModus)
        {
            spelerKijken();
        }
        else if (cameraModus)
        {
            if (Input.GetMouseButton(1))
            {
                Cursor.lockState = CursorLockMode.Locked;
                spelerKijken();
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
    public void spelerBeweging()
    {
        xAxis = Input.GetAxis("Horizontal");
        zAxis = Input.GetAxis("Vertical");

        Vector3 move = transform.right * xAxis + transform.forward * zAxis;
        controller.Move(move * speed * Time.deltaTime);
    }

    public void spelerKijken()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
    public void resetSpelerKijk()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}