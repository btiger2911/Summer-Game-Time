using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 playerVelocity;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpHeight = 1f;

    private bool isGrounded;
    [SerializeField] private float gravity = -9.8f;

    public Camera camera;
    private float rotation = 0f;
    [SerializeField] private float xSensitivity = 100f;
    [SerializeField] private float ySensitivity = 100f;

    private bool cursorLocked = true;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        LockCursor();


    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = characterController.isGrounded;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLocked = false;
            UnlockCursor();
        }

        // Re-lock cursor on left-click (or any other trigger)
        if (!cursorLocked && Input.GetMouseButtonDown(0))
        {
            cursorLocked = true;
            LockCursor();
        }
    }

    public void Move(Vector2 input)
    {
        Vector3 moveDir = Vector3.zero;
        moveDir.x = input.x;
        moveDir.z = input.y;
        characterController.Move(transform.TransformDirection(moveDir) * speed * Time.deltaTime );

        playerVelocity.y += gravity * Time.deltaTime;

        if ( isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }

        characterController.Move(playerVelocity * Time.deltaTime);
        Debug.Log(playerVelocity.y);
    }

    public void Jump()
    {
        if(isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }


    public void Look(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        rotation -= mouseY * ySensitivity * Time.deltaTime;
        rotation = Mathf.Clamp(rotation, -80f, 80f);

        camera.transform.localRotation = Quaternion.Euler(rotation, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
