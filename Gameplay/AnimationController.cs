/// <summary>
/// AnimationController - Networked Animation Management
/// 
/// Manages animator component and animation state synchronization across network.
/// Coordinates animation parameters with player state.
/// 
/// Features:
/// - Animator parameter management
/// - Network animation synchronization
/// - Animation state tracking
/// - Movement state integration
/// 
/// Dependencies: Netcode, Animator
/// </summary>

using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class AnimationController : NetworkBehaviour
{
    // Start is called before the first frame update
    [Header("Player References")]
    public PlayerController playerController;

    public CharacterController CharacterController;
    [Header("Animation Parameters")]
    [SerializeField] string IDLE;
    [SerializeField] string WALK;
    [SerializeField] string SPRINT;
    [SerializeField] string JUMP;

    [Header("Character References")]
    public Animator Animator;

    [SerializeField] private bool isNetworked;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        isNetworked = true;

    }

    public void Start()
    {
        playerController = GetComponent<PlayerController>();
        Animator = GetComponent<Animator>();
        CharacterController = GetComponent<CharacterController>();
    }
    public void SetupCharacter(Animator animator)
    {
        this.Animator = animator;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsClient && IsOwner || !isNetworked)
        {
            DecisionTree();
        }
    }


    void DecisionTree()
    {
        if (Animator != null)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            float inputMagnitude = direction.magnitude;
            switch (playerController.currentState)
            {
                case PlayerController.State.IDLE:
                    {
                        PlayAnimation(WALK);
                        /*                        AnimatorSetVariable("Speed", inputMagnitude);
                                                AnimatorSetVariable("SprintSpeed", 1f);*/
                        break;
                    }
                    /*                case PlayerController.State.WALK:
                                        {
                                            PlayAnimation(WALK);
                                            AnimatorSetVariable("Speed", inputMagnitude);
                                            AnimatorSetVariable("SprintSpeed", 1f);
                                            break;
                                        }
                                    case PlayerController.State.SPRINT:
                                        {
                                            PlayAnimation(WALK);
                                            AnimatorSetVariable("Speed", inputMagnitude);
                                            AnimatorSetVariable("SprintSpeed", 1.4f);
                                            break;
                                        }
                                    case PlayerController.State.JUMP:
                                        {
                                            PlayAnimation(JUMP);
                                            break;
                                        }*/
            }
        }
    }

    [ServerRpc]
    void PlayAnimationServerRpc(FixedString128Bytes animation)
    {
        if (IsServer)
        {
            Animator.Play(animation.ToString());
        }
    }

    [ServerRpc]
    void AnimatorSetVariableServerRpc(FixedString128Bytes name, float value)
    {
        Animator.SetFloat(name.ToString(), value, 0.05f, Time.deltaTime);

    }

    void PlayAnimation(FixedString128Bytes animation)
    {
        Animator.Play(animation.ToString());
    }

    void AnimatorSetVariable(FixedString128Bytes name, float value)
    {
        Animator.SetFloat(name.ToString(), value, 0.05f, Time.deltaTime);

    }
}
