using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FirstPersonController : MonoBehaviour
{
    #region Variables

    //To check player is in control or not
    public bool CamMove { get; private set; } = true;

    //Can player sprint or not (=> | Lamda Operator) (Subject to change)
    //It will become true if canSprint is true and(&&) if user will press sprint key
    //For PC Sprint
    //private bool IsSprinting => canSprint && Input.GetKey(sprintKey);
    //For Mobile
    private bool IsSprinting => canSprint && ((new Vector2(joystick.Horizontal, joystick.Vertical).magnitude)>0.9f);

    //Jump functionality (Its checking if the player is on ground then only he can jump)
    //For PC Jump
    //private bool ShouldJump => Input.GetKeyDown(JumpKey) && characterController.isGrounded;
    //For Mobile
    private bool ShouldJump => characterController.isGrounded && !isCrouching;

	//Crouch check (Its checking if user has pressed crouch key && animation is not player && our player is on ground) then it will become true
    //For PC Crouch
	//private bool ShouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchAnimation && characterController.isGrounded;
    //For Mobile
    private bool ShouldCrouch => !duringCrouchAnimation && characterController.isGrounded;

    //Options that you can turn off and on based on gameplay
    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canCrouch = true;
    [SerializeField] private bool canUseHeadbob = true;
    [SerializeField] private bool WillSlideOnSlopes = true;
    [SerializeField] private bool canInteract = true;
    [SerializeField] private bool useFootsteps = true;

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 1.5f;
    [SerializeField] private float sprintSpeed = 2.0f;
    [SerializeField] private float crouchSpeed = 0.5f;
    [SerializeField] private float sloopSpeed = 8f;

    [Header("Look Parameters")]
    
    //For PC
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

    /*
    //For Mobile
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;*/
    [SerializeField, Range(1, 20)] public float mouseSensitivity = 5f;
    
    [Header("Controls")]
    //Keys for input (Subject to change)!!
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode JumpKey = KeyCode.Space;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode interactKey = KeyCode.Mouse0;

    [Header("Jumping Parameters")]
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 30.0f;

    [Header("Crouch Parameters")]
    //Crouch Height
    [SerializeField] private float crouchHeight = 0.5f;
    //Stand Height
    [SerializeField] private float standingHeight = 2f;
    //Time to crouch/stand
    [SerializeField] private float timetoCrouch = 0.25f;
    //Standing center point
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    //Crouching center point
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 0, 0);
    //Is Crouching check
    private bool isCrouching;
    //Is in crouch animation check
    private bool duringCrouchAnimation;

    [Header("Headbob Parameters")]
    //Headbob effect while walking
    [SerializeField] private float walkBobSpeed = 10f;
    [SerializeField] private float walkBobAmount = 0.02f;
    //Headbob effect sprinting
    [SerializeField] private float sprintBobSpeed = 16f;
    [SerializeField] private float sprintBobAmount = 0.04f;
    //Headbob effect while crouching
    [SerializeField] private float crouchBobSpeed = 8f;
    [SerializeField] private float crouchBobAmount = 0.01f;
    //For Camera Position
    private float defaultYPos = 0;
    //For Camera Verticle Movement
    private float timer;

    [Header("FootStep Parameters")]
    [SerializeField] private float baseStepSpeed = 0.6f;
    [SerializeField] private float crouchStepMultipler = 1.5f;
    [SerializeField] private float sprintStepMultipler = 0.5f;
    [SerializeField] private AudioSource footstepAudioSource = default;
    [SerializeField] private AudioClip[] floorClips = default;
    [SerializeField] private AudioClip[] tileClips = default;
    [SerializeField] private AudioClip[] stairClips = default;
    [SerializeField] private AudioClip[] carpetClips = default;
    private float footstepTimer = 0;
    private float GetCurrentOffset => isCrouching ? baseStepSpeed * crouchStepMultipler : IsSprinting ? baseStepSpeed * sprintStepMultipler : baseStepSpeed;

    [Header("Interaction Parameters")]
    [SerializeField] private Vector3 interactionRayPoint = default;
    [SerializeField] private float interactionDistance = default;
    [SerializeField] private LayerMask interactionLayer = default;
    private Interactable currentInteractable;

    //Sliding Parameters
    private Vector3 hitPointNormal;
    private bool IsSliding
	{
		get
		{
            //Change 2f to play around with is ground functionality
            if(characterController.isGrounded && Physics.Raycast(transform.position,Vector3.down, out RaycastHit slopeHit, 2f))
			{
                hitPointNormal = slopeHit.normal;
                return Vector3.Angle(hitPointNormal, Vector3.up) > characterController.slopeLimit;
			}
			else
			{
                return false;
			}
		}
	}

    /// <summary>
    /// Player Objects
    /// </summary>

    //Player Camera Object
    private Camera playerCamera;
    //Player Controller
    private CharacterController characterController;

    //Variables for current input values(W,A,S,D) and MoveDirection(X,Y) 
    private Vector3 moveDirection;
    private Vector2 currentInput;
    public FloatingJoystick joystick;

    /*To keep check of current rotation, it will be used to clamp
      or restrict player from rotating camera above
      or below its neck level, So he cant break his neck xD*/
    private float rotationX = 0;

    //InteractUI Object
    //private InteractUI InteractText;
    //public TMP_Text InteractText;
    public Text SimpleInteractText;

    //Rotation Variables
    float mouseX = 0;
    float mouseY = 0;

    #endregion

    #region Singleton
    public static FirstPersonController instance;
	#endregion

	#region Unity Methods

	void Awake()
	{
        //Initializing Singleton
        instance = this;

        //Pasing Reference to Game objects
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();

        //Setting default position of headbob to starting position of camera
        defaultYPos = playerCamera.transform.localPosition.y;

        //For PC Cursor
        //To Hide Cursor
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
	}

	void Start()
    {
        //InteractText = gameObject.GetComponent<InteractUI>();
    }

    void Update()
    {
        if (Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            mouseX = Input.GetTouch(0).deltaPosition.x;
            mouseY = Input.GetTouch(0).deltaPosition.y;
        }

        //Handling Inputs
        if (CamMove)
		{
            HandleMovementInput();
            HandleLook();

            //For PC Jump
            /*if (canJump)
                HandleJump();*/

            //For PC Crouch
            /*if (canCrouch)
                HandleCrouch();*/

            if (canUseHeadbob)
                HandleHeadbob();

			if (canInteract)
			{
                HandleInteractionCheck();
                HandleInteractionInput();         
            }

            if (useFootsteps)
                Handle_Footsteps();

            ApplyFinalMovements();
        }	
    }

    #endregion
   
    #region Custom Methods

    //For Movement Inputs
    private void HandleMovementInput()
	{
        //Getting current Input values Horizontal and Verticle (Left and Right Movements) (Using Ternary Operator(?:) for ifelse condition in sprinting and walking) (Subject to change)
        //For PC Movement
        //currentInput = new Vector2((isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"), (isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));
        //For Mobile
        currentInput = new Vector2((isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * joystick.Vertical, (isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * joystick.Horizontal);

        //Getting move Direction Y value
        float moveDirectionY = moveDirection.y;

        //Calculating move Direction, Getting character orientation, MoveDirentction is storing Vector3 component (Subject to change)
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);

        //Setting Move Direction
        moveDirection.y = moveDirectionY;
	}

    //For Camera rotation Inputs
    private void HandleLook()
	{
        
        //For PC Look
        //For moving camera Up and Down(Subject to change)
        rotationX -= Input.GetAxis("Mouse Y")*lookSpeedY;
        //Restricting player to break his neck xD (Clamping values)
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        //For moving camera Up and Down
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        //For moving player left and right (Subject to change)
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
        

        /*if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            //Restricting camera to move which player will touch UI elements
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                return;

            mouseX = Input.GetTouch(0).deltaPosition.x;
            mouseY = Input.GetTouch(0).deltaPosition.y;
        }*/

        //Touchscreen.current.touches.Count = Input.touchCount
        //Touchscreen.current.touches[0].touchId.ReadValue() = Input.GetTouch(0).fingerId
        //Touchscreen.current.touches[1].isInProgress =  Input.GetTouch(1).phase == TouchPhase.Moved
        //Touchscreen.current.touches[1].delta.ReadValue(); = Input.GetTouch(1).deltaPosition;

        /*
        //For Mobile
        float mouseX = 0;
        float mouseY = 0;

        if (Input.touchCount == 0)
            return;

        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            if (Input.touchCount > 1 && Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(1).fingerId))
                    return;

                Vector2 touchDeltaPosition = Input.GetTouch(1).deltaPosition;
                mouseX = touchDeltaPosition.x;
                mouseY = touchDeltaPosition.y;
            }
        }
        else
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    return;

                Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                mouseX = touchDeltaPosition.x;
                mouseY = touchDeltaPosition.y;
            }

        }

        mouseX *= mouseSensitivity;
        mouseY *= mouseSensitivity;

        //For moving camera Up and Down(Subject to change)
        rotationX -= mouseY * Time.deltaTime;
        //Restricting player to break his neck xD (Clamping values)
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);

        //For moving camera
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        //For moving player
        transform.Rotate(Vector3.up * mouseX * Time.deltaTime);
        */
    }

    //For Jumping
    public void HandleJump()
	{
        if (ShouldJump)
            moveDirection.y = jumpForce;
	}

    public void HandleCrouch()
	{
        if (ShouldCrouch)
            StartCoroutine(CrouchStand());
	}

    private void HandleHeadbob()
	{
        if (!characterController.isGrounded) return;

        if(Mathf.Abs(moveDirection.x)>0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
		{
            timer += Time.deltaTime * (isCrouching ? crouchBobSpeed : IsSprinting ? sprintBobSpeed : walkBobSpeed);

            playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                defaultYPos + Mathf.Sin(timer)*(isCrouching ? crouchBobAmount : IsSprinting ? sprintBobAmount : walkBobAmount),playerCamera.transform.localPosition.z);
		}
	}

    /// <summary>
    /// Interaction
    /// </summary>

    //Constantly raycast and look for interactable objects
    private void HandleInteractionCheck()
	{
        if(Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance))
		{
            //Change number 8 to Interactable Layer (Subject to change)
            if(hit.collider.gameObject.layer == 8 && (currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.GetInstanceID()))
			{
                hit.collider.TryGetComponent(out currentInteractable);

                if (currentInteractable)
				{
                    currentInteractable.OnFocus();
                    //Changing name of InteractionText
                    //InteractText.SetInteractText("" + currentInteractable.name);
                    SimpleInteractText.text = "" + currentInteractable.name;
                }
                    
			}
		}
        else if (currentInteractable)
		{
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
            //Removing name of InteractionText
            //InteractText.RemoveInteractText();
            SimpleInteractText.text = "";
        }
	}

    //When we hit interact key the action will be performed
    private void HandleInteractionInput()
	{
        if(Input.GetKeyDown(interactKey) && currentInteractable != null && Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance, interactionLayer))
		{
            currentInteractable.OnInteract();
        }
	}

    private void Handle_Footsteps()
	{
        //Checking if character is grounded or not
        if (!characterController.isGrounded) return;
        //Checking for movement
        if (currentInput == Vector2.zero) return;

        footstepTimer -= Time.deltaTime;

		if (footstepTimer <= 0)
		{
            //In here 3 is the distance of raycast hit (Tweak it to decrease object/ground distace check)
            if(Physics.Raycast(playerCamera.transform.position, Vector3.down,out RaycastHit hit, 3))
			{
				switch (hit.collider.tag)
				{
                    case "Footsteps/Floor":
                        footstepAudioSource.PlayOneShot(floorClips[Random.Range(0,floorClips.Length-1)]);
                        break;
                    /*case "Footsteps/Tile":
                        footstepAudioSource.PlayOneShot(tileClips[Random.Range(0, tileClips.Length - 1)]);
                        break;
                    case "Footsteps/Stair":
                        footstepAudioSource.PlayOneShot(stairClips[Random.Range(0, stairClips.Length - 1)]);
                        break;
                    case "Footsteps/Carpet":
                        footstepAudioSource.PlayOneShot(carpetClips[Random.Range(0, carpetClips.Length - 1)]);
                        break;*/
                    default:
                        footstepAudioSource.PlayOneShot(floorClips[Random.Range(0, floorClips.Length - 1)]);
                        break;
                }
			}
            footstepTimer = GetCurrentOffset;
		}

	}

    //For applying finalmovements on our player
    private void ApplyFinalMovements()
	{
        //If condition for Jumping (Ground Check)
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        if (WillSlideOnSlopes && IsSliding)
            moveDirection += new Vector3(hitPointNormal.x, -hitPointNormal.y, hitPointNormal.z) * sloopSpeed;

        //For moving player (W,S,A,D) movements
        characterController.Move(moveDirection * Time.deltaTime);
	}
    #endregion

    #region Coroutine Functions
    private IEnumerator CrouchStand()
	{
        //Check to disable player from clipping throgh object while he will release crouching state(It will check if anything is above his head he will not stand up) (Change 1f value for distance)
        if (isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f))
            yield break;

        duringCrouchAnimation = true;

        float timeElasped = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;

        while(timeElasped < timetoCrouch)
		{
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElasped / timetoCrouch);
            characterController.center = Vector3.Lerp(currentCenter,targetCenter,timeElasped/timetoCrouch);
            timeElasped += Time.deltaTime;
            yield return null;
		}

        characterController.height = targetHeight;
        characterController.center = targetCenter;

        isCrouching = !isCrouching;

        duringCrouchAnimation = false;
	}
    #endregion
}
