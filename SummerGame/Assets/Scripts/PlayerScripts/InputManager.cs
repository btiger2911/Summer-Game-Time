using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.PlayerActions player;

    private PlayerMovement playerMovement;
    void Awake()
    {
        playerInput = new PlayerInput();
        player = playerInput.Player;

        playerMovement = GetComponent<PlayerMovement>();

       player.Jump.performed += ctx => playerMovement.Jump();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerMovement.Move(player.Walking.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        playerMovement.Look(player.Look.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        player.Enable();
    }

    private void OnDisable()
    {
        player.Disable();
    }
}
