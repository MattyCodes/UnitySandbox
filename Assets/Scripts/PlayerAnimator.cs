using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Player player;

    private const string PLAYER_IS_WALKING = "PlayerIsWalking";
    private Animator animator;

    private void ConditionallyTriggerWalkingAnimation() {
        animator = GetComponent<Animator>();

        animator.SetBool(PLAYER_IS_WALKING, player.IsWalking());
    }

    private void Update() {
        ConditionallyTriggerWalkingAnimation();
    }
}
