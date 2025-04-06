using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 15f;
    [SerializeField] private float playerRadius = 0.7f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking;
    private bool isJumping;

    private void Update()
    {
        // On every frame, check for jump-input from the player.
        isJumping = gameInput.GetJumpingValue();

        // Also get any movement inputs (WASD) from the player.
        Vector2 inputVector = gameInput.GetMovementVector().normalized;

        // Get the intended movement direction.
        Vector3 radialMoveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        // Get the X vector of the intended movement direction.
        Vector3 xMoveDirection = new Vector3(radialMoveDirection.x, 0f, 0f);

        // Get the Z vector of the intended movement direction.
        Vector3 zMoveDirection = new Vector3(0f, 0f, radialMoveDirection.z);

        // Determine the traversable distance if no collisions occur.
        float unhinderedMoveDistance = movementSpeed * Time.deltaTime;

        // If collisions do occur, but the player can still move in the
        // direction of one of the specified vectors, reduce the traversable
        // distance by 25%.
        float hinderedMoveDistance = unhinderedMoveDistance * 0.75f;

        // Determine if the user is capable of moving in the exact
        // direction that was specified without colliding with anything.
        bool canMoveRadial = !Physics.CapsuleCast(
            transform.position,
            transform.position + Vector3.up * playerHeight,
            playerRadius,
            new Vector3(inputVector.x, 0f, inputVector.y),
            unhinderedMoveDistance
        );

        // For the three possible input vectors, determine if it is possible
        // to move without a collision occurring.
        if (canMoveRadial) {
            transform.position += radialMoveDirection * unhinderedMoveDistance;
            isWalking = radialMoveDirection != Vector3.zero;
        } else {
            bool canMoveX = !Physics.CapsuleCast(
                transform.position,
                transform.position + Vector3.up * playerHeight,
                playerRadius,
                xMoveDirection,
                hinderedMoveDistance
            );

            if (canMoveX) {
                transform.position += xMoveDirection * hinderedMoveDistance;
                isWalking = xMoveDirection != Vector3.zero;
            } else {
                bool canMoveZ = !Physics.CapsuleCast(
                    transform.position,
                    transform.position + Vector3.up * playerHeight,
                    playerRadius,
                    zMoveDirection,
                    hinderedMoveDistance
                );

                if (canMoveZ) {
                    transform.position += zMoveDirection * hinderedMoveDistance;
                    isWalking = zMoveDirection != Vector3.zero;
                } else {
                    isWalking = false;
                };
            };
        };

        // Even if the player cannot move in the desired
        // direction, allow them to rotate in place.
        transform.forward = Vector3.Slerp(
            transform.forward,
            radialMoveDirection,
            rotationSpeed * Time.deltaTime
        );
    }

    public bool IsWalking() {
        return isWalking;
    }

    public bool IsJumping() {
        return isJumping;
    }
}
