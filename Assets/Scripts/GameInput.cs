using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable() {
        if (playerInputActions != null) {
            playerInputActions.Player.Jump.Enable();
            playerInputActions.Player.Move.Enable();
        };
    }

    private void OnDisable() {
        if (playerInputActions != null) {
            playerInputActions.Player.Jump.Disable();
            playerInputActions.Player.Move.Disable();
        };
    }

    public Vector2 GetMovementVector() {
        return playerInputActions.Player.Move.ReadValue<Vector2>();
    }

    public bool GetJumpingValue() {
        return playerInputActions.Player.Jump.WasPressedThisFrame();
    }
}
