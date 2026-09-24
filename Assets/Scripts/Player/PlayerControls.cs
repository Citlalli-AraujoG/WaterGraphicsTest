using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private CharacterController charCtrl;
    public Transform cameraTransform;
    public float mouseSens = 2f;
    private float xRotation = 0f;
    public float gravity = -9.81f;
    public Vector3 velocity;
    public float playerSpeed = 5f;

    public float throwForce;

    void Start()
    {
        charCtrl = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //Moving Mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSens;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);


        //Player Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 moveDir = transform.right * x + transform.forward * z;
        charCtrl.Move(moveDir * playerSpeed * Time.deltaTime);

        //Gravity
        if (charCtrl.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        charCtrl.Move(velocity *Time.deltaTime);

    }

    
}
   