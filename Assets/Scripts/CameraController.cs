using UnityEngine;S

public class CameraController : MonoBehaviour
{
    public float sensibility = 2f;

    // Variables para acumular la rotación de los ejes
    private float rotationX = 0f;
    private float rotationY = 0f;


    public Transform carPoint;
    public float followCarSpeed;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        Vector3 currentRotation = transform.localEulerAngles;
        rotationX = currentRotation.y;
        rotationY = currentRotation.x;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibility;
        float mouseY = Input.GetAxis("Mouse Y") * sensibility;


        rotationX += mouseX; 
        rotationY -= mouseY; 

        rotationY = Mathf.Clamp(rotationY, -45f, 45f);

        transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0f);

        transform.position = Vector3.Lerp(transform.position, carPoint.position, followCarSpeed * Time.deltaTime);
    }
}
