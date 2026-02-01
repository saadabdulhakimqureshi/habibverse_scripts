using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class AnimatedPlayerController : NetworkBehaviour
{
    // Start is called before the first frame update

    public enum State { IDLE, WALK, SPRINT, JUMP };
    [Header("Player Variables")]
    public State currentState;

    [Header("Player References")]
    public CharacterController characterController;
    public Animator Animator;
    public Transform FollowCameraTransform;
    public CinemachineFreeLook FreelookCamera;

    /*[Header("Scene References")]
    */

    [Header("Character References")]
    public GameObject Character;

    [Header("Player Parameters")]
    [SerializeField] public float jumpDistance;
    [SerializeField] public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    Vector3 moveDirection;

    [Header("Player Controls")]
    public KeyCode sprint;
    public KeyCode jump;

    [SerializeField] private bool isNetworked = false;

    public override void OnNetworkSpawn()
    {
        Cursor.visible = false;
        isNetworked = true;
        base.OnNetworkSpawn();
    }

    public void Start()
    {
        
        currentState = State.IDLE;
        characterController = GetComponent<CharacterController>();

        // Enabling logic for non network testing
        if (IsLocalPlayer || !isNetworked)
        {
            FollowCameraTransform.gameObject.SetActive(true);
            FreelookCamera.gameObject.SetActive(true);
        }
        else
        {
            FollowCameraTransform.gameObject.SetActive(false);
            FreelookCamera.gameObject.SetActive(false);
        }
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
        switch (currentState)
        {
            case State.IDLE:
                {
                    CheckInput();
                    break;
                }
/*            case State.WALK:
                {
                    CheckInput();
                    //Move(0f, moveDirection);
                    break;
                }
            case State.SPRINT:
                {
                    CheckInput();
                    //Move(0f, moveDirection);
                    break;
                }
            case State.JUMP:
                {
                    //CheckInput();
                    //Move(jumpDistance, moveDirection);
                    break;
                }*/
        }
    }

    void CheckInput()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        float inputMagnitude = direction.magnitude;

        if (inputMagnitude >= 0.1f)
        {
            moveDirection = direction;
            if (Input.GetKey(sprint))
            {
                currentState = State.SPRINT;
            }
            else
            {
                currentState = State.WALK;
            }
            if (Input.GetKeyDown(jump))
            {
                RegisterJump();
            }
            return;
        }

        if (Input.GetKeyDown(jump))
        {
            RegisterJump();
            return;
        }

        currentState = State.IDLE;
    }

    public void RegisterJump()
    {
        currentState = State.JUMP;
        moveDirection.y = jumpDistance;
        
    }

    void Move(float movingSpeed, Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + FollowCameraTransform.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        characterController.Move(moveDir.normalized * movingSpeed * Time.deltaTime);

    }



    public void OnJumpComplete()
    {
        currentState = State.IDLE;
    }

}
