using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float rotationSpeed = 15f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking;
    private bool isJumping;

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVector().normalized;
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        isWalking = moveDir != Vector3.zero;
        isJumping = gameInput.GetJumpingValue();

        transform.position += moveDir * movementSpeed * Time.deltaTime;

        transform.forward = Vector3.Slerp(
            transform.forward,
            moveDir,
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
