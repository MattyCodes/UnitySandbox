using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float jumpHeight = 3.5f;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed = 15f;
    // [SerializeField] private float playerRadius = 0.7f;
    // [SerializeField] private float playerHeight = 2f;
    [SerializeField] private GameInput gameInput;

    private CharacterController controller;
    private Vector3 velocity;

    private bool isWalking = false;
    private bool isJumping = false;
    private bool isGrounded = true;

    public bool IsWalking() => isWalking;
    public bool IsJumping() => isJumping;

    private void Awake() {
        controller = GetComponent<CharacterController>();
    }

    private void Update() {
        HandleMovement();
    }

    private void HandleMovement() {
        // Manage the state of player velocity based on whether
        // or not they are grounded/jumping.
        isGrounded = controller.isGrounded;

        // If they're grounded and not jumping, just set a baseline
        // downward force to keep 'em that way.
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
            isJumping = false;
        }

        // If they are grounded and we've received a jump button-press
        // during this frame, the player is now jumping and their
        // upward velocity should be updated accordingly.
        if (gameInput.GetJumpingValue() && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            isJumping = true;
        }

        // As a general rule, if the player is in the air, then their downward
        // velocity should increase according to gravity until they are grounded.
        if (!isGrounded) {
            velocity.y += gravity * Time.deltaTime;
        }

        // Gather the directional input (WASD, or equivalent) from the user, and
        // derive a 3D vector from it.
        Vector2 inputVector = gameInput.GetMovementVector().normalized;
        Vector3 inputDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        // Rotate the player in the direction they are trying to
        // move, even if a collision prevents them from moving there.
        transform.forward = Vector3.Slerp(
            transform.forward,
            inputDirection,
            rotationSpeed * Time.deltaTime
        );

        // The player should only be considered "walking" if the derived
        // input-vector is non-zero in at least one direction.
        if (inputDirection != Vector3.zero) {
            isWalking = true;
        } else {
            isWalking = false;
        };

        // Instantiate the movement vector as a result of the input
        // direction, and then apply upward/downward velocity to it.
        Vector3 playerMovement = inputDirection * movementSpeed;
        playerMovement.y = velocity.y;

        // Affect the character controller with the desired player movement.
        controller.Move(playerMovement * Time.deltaTime);
    }
}
