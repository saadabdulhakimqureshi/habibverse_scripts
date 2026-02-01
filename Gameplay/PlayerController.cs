/// <summary>
/// PlayerController - Local Player Input & Movement
/// 
/// Handles all local player input processing and movement logic.
/// Manages keyboard/controller input, camera follow, and networked position updates.
/// 
/// Key Responsibilities:
/// - Process player input (movement, jumping, actions)
/// - Calculate and apply movement velocity
/// - Network movement synchronization
/// - Weapon/shooting mechanics
/// - Interaction with Cinemachine camera
/// 
/// Dependencies: Cinemachine, Netcode, Unity Services Authentication
/// </summary>

using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using Unity.Services.Authentication;

public class PlayerController : NetworkBehaviour
{
    // Start is called before the first frame update

    public enum State { IDLE, WALK, SPRINT, JUMP, FALL, ROLL };
    [Header("Player Variables")]
    public State currentState;

    [Header("Player References")]
    public CharacterController characterController;
    public Rigidbody rigidBody;
    public Animator Animator;
    public AudioSource bounceSound;
    public Transform FollowCameraTransform;
    public Camera MainCamera;
    public CinemachineVirtualCameraBase FreelookCamera;
    public CinemachineVirtualCameraBase AimCamera;
    public GameObject PlayerMovingParticles;
    public PlayerCustomizer PlayerCustomizer;
    public PlayerCard PlayerCard;
    public GameObject Character;
    public GameObject PlayerName;

    [Header("Scene References")]
    public LocalPlayerGameManager PlayerGameManager;


    [Header("Bullet")]
    public Transform Boomerang;
    public Transform BoomerangSpawn;
    public int ammo;

    [Header("Player Parameters")]
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField] private float rotationSpeed = 5.0f; // Adjust this value as needed
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float moveHeight = 0.5f;
    [SerializeField] private float gravityValue = -9.81f;

    [SerializeField] private float normalSpeed = 2.0f;
    [SerializeField] private float sprintSpeed = 3.0f;

    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float normalHeight = 0.5f;

    [SerializeField] private float slideForce;


    [Header("Player Controls")]
    public KeyCode sprint;
    public KeyCode jump;

    [Header("Network")]
    [SerializeField] private bool isNetworked = false;
    [SerializeField] private ulong clientId;
    public bool isLocalPlayer;

    public event EventHandler OnPlayerSpawn;
    public event EventHandler OnPlayerAim;
    public event EventHandler OnPlayerStopAim;

    public override void OnNetworkSpawn()
    {
        isNetworked = true;
        isLocalPlayer = IsLocalPlayer;
    }

    public void Start()
    {
        AimCamera.Priority = 1;

        if (HabibVerse.Instance != null)
        {
            // Setting visual according to data belonging to owner client of this player.
            //Debug.Log("Client getting customization data " + OwnerClientId);

            CustomizationData customizationData = HabibVerse.Instance.GetCustomizationData(OwnerClientId);

            PlayerCustomizer.SetModel(customizationData.modelIndex);
            PlayerCustomizer.SetModelColor(customizationData.modelColor);
            PlayerCustomizer.SetParticleColor(customizationData.particlesColor);
            PlayerCustomizer.SetHat(customizationData.hatIndex);
            PlayerCustomizer.SetFace(customizationData.faceIndex);
            PlayerCustomizer.SetAura(customizationData.auraIndex);
            PlayerCard.SetPlayerName(customizationData.playerName.ToString());
            name = customizationData.playerName.ToString();
            // Hiding ready text.
            PlayerCard.HideReady();

            PlayerCustomizer.playerIndex = HabibVerse.Instance.GetPlayerIndex(OwnerClientId);
            PlayerCard.playerIndex = HabibVerse.Instance.GetPlayerIndex(OwnerClientId);
        }

        characterController = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();

        rigidBody.isKinematic = false;



        // Enabling logic for non network testing
        if (IsLocalPlayer || !isNetworked)
        {
            Cursor.visible = false;
            if (LocalPlayerGameManager.Instance != null)
            {
                PlayerGameManager = LocalPlayerGameManager.Instance;
                PlayerGameManager.Ammo = ammo;
            }
            FollowCameraTransform.gameObject.SetActive(true);
            FreelookCamera.gameObject.SetActive(true);
            AimCamera.gameObject.SetActive(true);
        }
        else
        {
            FollowCameraTransform.gameObject.SetActive(false);
            FreelookCamera.gameObject.SetActive(false);
            AimCamera.gameObject.SetActive(false);
        }
    }


    public void SetupCharacter(GameObject character)
    {
        Debug.Log(AuthenticationService.Instance.PlayerId);


        characterController.enabled = false;

        character.transform.position = transform.position;
        transform.rotation = character.transform.transform.rotation;

        characterController.enabled = true;

        character.transform.parent = transform;

    }
    // Update is called once per frame
    void Update()
    {
        if (IsClient && IsOwner || !isNetworked)
        {

            if (LocalPlayerGameManager.Instance != null)
            {
                if (LocalPlayerGameManager.Instance.State == LocalPlayerGameManager.LocalState.Resumed)
                {
                    CheckInput();
                }
            }
            else
            {
                CheckInput();
            }

            DecisionTree();
        }
    }


    void DecisionTree()
    {
        switch (currentState)
        {
            case State.IDLE:
                {
                    ControlledFall();
                    break;
                }
            case State.WALK:
                {
                    Move();
                    ControlledFall();
                    break;
                }
            case State.SPRINT:
                {
                    Move();
                    ControlledFall();
                    break;
                }
            case State.ROLL:
                {
                    Roll();
                    break;
                }
            case State.FALL:
                {

                    break;
                }

        }
    }

    void CheckInput()
    {
        if (currentState != State.ROLL)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            float inputMagnitude = direction.magnitude;

            groundedPlayer = characterController.isGrounded;

            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            if (inputMagnitude > 0.1f)
            {
                if (Input.GetKey(sprint))
                {
                    playerSpeed = sprintSpeed;
                    currentState = State.SPRINT;
                }
                else
                {
                    currentState = State.WALK;
                    playerSpeed = normalSpeed;
                }

            }
            else
            {
                currentState = State.IDLE;
            }
            if (groundedPlayer)
            {
                bounceSound.Play();
                if (Input.GetKey(jump))
                {

                    moveHeight = jumpHeight;
                    playerVelocity.y += Mathf.Sqrt(moveHeight * -3.0f * gravityValue);

                }
                else
                {
                    moveHeight = normalHeight;
                    if (currentState != State.IDLE)
                        playerVelocity.y += Mathf.Sqrt(moveHeight * -3.0f * gravityValue);
                }
            }

            /*            if (Input.GetMouseButton(1))
                        {
                            AimCamera.Priority = 1;
                            FreelookCamera.Priority = 0;
                            OnPlayerAim?.Invoke(this, EventArgs.Empty);*/

            if (Input.GetMouseButtonDown(0) && ammo > 0)
            {
                Ray ray = FollowCameraTransform.GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    // Call the Fire function with the hit point as the shoot position
                    Fire(hit.point);
                    ammo--;
                    PlayerGameManager.Ammo = ammo;
                }
            }

            /*            }
                        else
                        {
                            AimCamera.Priority = 0;
                            FreelookCamera.Priority = 1;
                            OnPlayerStopAim?.Invoke(this, EventArgs.Empty);
                        }*/
        }
    }


    void Move()
    {
        if (LocalPlayerGameManager.Instance != null && LocalPlayerGameManager.Instance.State == LocalPlayerGameManager.LocalState.Resumed)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            move = FollowCameraTransform.forward * move.z + FollowCameraTransform.right * move.x; // Update move vector

            characterController.Move(move * Time.deltaTime * playerSpeed);

            if (move != Vector3.zero)
            {
                Vector3 targetForward;

                // Check if aiming (for example, using right mouse button)
                if (Input.GetMouseButton(1))
                {
                    targetForward = FollowCameraTransform.forward;
                }
                else
                {
                    targetForward = move.normalized;
                }

                Vector3 flatForward = Vector3.ProjectOnPlane(targetForward, Vector3.up);
                Quaternion targetRotation = Quaternion.LookRotation(flatForward, Vector3.up);
                gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }

    void Roll()
    {
        if (LocalPlayerGameManager.Instance != null && LocalPlayerGameManager.Instance.State == LocalPlayerGameManager.LocalState.Resumed)
        {
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            rigidBody.AddForce(move * slideForce * Time.deltaTime);

        }
    }

    void Fire(Vector3 shootPosition)
    {
        FireServerRpc(shootPosition);
    }

    [ServerRpc]
    void FireServerRpc(Vector3 shootPosition)
    {
        Transform bulletTransform = Instantiate(Boomerang, BoomerangSpawn.position, Quaternion.identity);
        bulletTransform.GetComponent<NetworkObject>().Spawn();
        Bullet bullet = bulletTransform.GetComponent<Bullet>();
        bullet.Setup(shootPosition, (shootPosition - BoomerangSpawn.position).normalized, name);
    }

    void ControlledFall()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    public void SetClientId(ulong clientId)
    {
        this.clientId = clientId;
        SetClientIdClientRpc(clientId);
    }

    // Making sure client id is also updated on clients end.
    [ClientRpc]
    public void SetClientIdClientRpc(ulong clientId)
    {
        this.clientId = clientId;
    }

    public void EnableSlide()
    {
        if (IsClient && IsOwner)
        {
            AimCamera.Priority = 0;
            FreelookCamera.Priority = 1;
            OnPlayerStopAim?.Invoke(this, EventArgs.Empty);
        }
        currentState = State.ROLL;
        Debug.Log("Enable Slide");
        characterController.enabled = false;

        rigidBody.constraints = RigidbodyConstraints.None;


    }

    public void DisableSlide()
    {
        if (IsClient && IsOwner)
        {
            AimCamera.Priority = 1;
            FreelookCamera.Priority = 0;
            OnPlayerAim?.Invoke(this, EventArgs.Empty);
        }

        currentState = State.IDLE;
        Debug.Log("Disable Slide");

        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        characterController.enabled = true;

    }

    public void Fall(Vector3 force)
    {
        currentState = State.FALL;
        characterController.enabled = false;
        rigidBody.constraints = RigidbodyConstraints.None;
        rigidBody.AddForce(force, ForceMode.Force);
        StartCoroutine(GetUp());
    }

    IEnumerator GetUp()
    {
        yield return new WaitForSeconds(2f);
        currentState = State.IDLE;
        rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        characterController.enabled = true;
    }

    public void SizeUp()
    {
        transform.localScale = Vector3.one * 1.5f;
        StartCoroutine(SizeDown());
    }

    IEnumerator SizeDown()
    {
        yield return new WaitForSeconds(50f);
        transform.localScale = Vector3.one;
    }

    public void EnableSuperJump()
    {
        jumpHeight *= 2.0f;


    }

    public void DisableSuperJump()
    {
        jumpHeight /= 2.0f;
    }

    public bool NextClue()
    {
        if (IsOwner)
        {
            HabibVerseTreasureHuntManager.Instance.NextClueServerRpc();
            return true;
        }
        return false;
    }
}
