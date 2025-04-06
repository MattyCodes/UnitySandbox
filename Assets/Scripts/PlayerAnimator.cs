using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player player;

    private const string PLAYER_IS_WALKING = "PlayerIsWalking";
    private const string PLAYER_IS_JUMPING = "PlayerIsJumping";

    private Animator animator;

    private void ConditionallyTriggerWalkingAnimation() {
        animator = GetComponent<Animator>();

        animator.SetBool(PLAYER_IS_WALKING, player.IsWalking());
    }

    private void ConditionallyTriggerJumpingAnimation() {
        animator = GetComponent<Animator>();

        animator.SetBool(PLAYER_IS_JUMPING, player.IsJumping());
    }

    private void Update() {
        ConditionallyTriggerWalkingAnimation();
        ConditionallyTriggerJumpingAnimation();
    }
}
