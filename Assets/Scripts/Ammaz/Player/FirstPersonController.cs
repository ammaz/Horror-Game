using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    #region Variables

    //To check player is in control or not
    public bool CamMove { get; private set; } = true;

    //Can player sprint or not (=> | Lamda Operator) (Subject to change)
    //It will become true if canSprint is true and(&&) if user will press sprint key
    private bool IsSprinting => canSprint && Input.GetKey(sprintKey);

    //Jump functionality (Its checking if the player is on ground then only he can jump)
    private bool ShouldJump => Input.GetKeyDown(JumpKey) && characterController.isGrounded;

    //Crouch check (Its checking if user has pressed crouch key && animation is not player && our player is on ground) then it will become true
    private bool ShouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchAnimation && characterController.isGrounded;


    //Options that you can turn off and on based on gameplay
    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canCrouch = true;
    [SerializeField] private bool canUseHeadbob = true;
    [SerializeField] private bool WillSlideOnSlopes = true;
    [SerializeField] private bool canInteract = true;

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;
    [SerializeField] private float crouchSpeed = 1.5f;
    [SerializeField] private float sloopSpeed = 8f;

    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

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
    [SerializeField] private float walkBobSpeed = 14f;
    [SerializeField] private float walkBobAmount = 0.05f;
    //Headbob effect sprinting
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float sprintBobAmount = 0.1f;
    //Headbob effect while crouching
    [SerializeField] private float crouchBobSpeed = 8f;
    [SerializeField] private float crouchBobAmount = 0.025f;
    //For Camera Position
    private float defaultYPos = 0;
    //For Camera Verticle Movement
    private float timer;

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
    ///

    //Player Camera Object
    private Camera playerCamera;
    //Player Controller
    private CharacterController characterController;

    //Variables for current input values(W,A,S,D) and MoveDirection(X,Y) 
    private Vector3 moveDirection;
    private Vector2 currentInput;

    /*To keep check of current rotation, it will be used to clamp
      or restrict player from rotating camera above
      or below its neck level, So he cant break his neck xD*/
    private float rotationX = 0;

	#endregion

	#region Singleton
	#endregion

	#region Unity Methods

	void Awake()
	{
        //Pasing Reference to Game objects
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();

        //Setting default position of headbob to starting position of camera
        defaultYPos = playerCamera.transform.localPosition.y;

        //To Hide Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}

	void Start()
    {
        
    }
    
    void Update()
    {
		//Handling Inputs
		if (CamMove)
		{
            HandleMovementInput();
            HandleLook();

            if (canJump)
                HandleJump();

            if (canCrouch)
                HandleCrouch();

            if (canUseHeadbob)
                HandleHeadbob();

            if(canInteract)


            ApplyFinalMovements();
        }	
    }

    #endregion
   
    #region Custom Methods

    //For Movement Inputs
    private void HandleMovementInput()
	{
        //Getting current Input values Horizontal and Verticle (Left and Right Movements) (Using Ternary Operator(?:) for ifelse condition in sprinting and walking) (Subject to change)
        currentInput = new Vector2((isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"), (isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));

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
        //For moving camera Up and Down(Subject to change)
        rotationX -= Input.GetAxis("Mouse Y")*lookSpeedY;
        //Restricting player to break his neck xD (Clamping values)
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        //For moving camera Up and Down
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        //For moving player left and right (Subject to change)
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);
	}

    //For Jumping
    private void HandleJump()
	{
        if (ShouldJump)
            moveDirection.y = jumpForce;
	}

    private void HandleCrouch()
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

    //Constantly raycast and look for interactable objects
    private void HandleInteractionCheck()
	{
        if(Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance))
		{
            //Change number 8 to Interactable Layer (Subject to change)
            if(hit.collider.gameObject.layer == 8 && currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.GetInstanceID())
			{
                hit.collider.TryGetComponent(out currentInteractable);

                if (currentInteractable)
                    currentInteractable.OnFocus();
			}
		}
        else if (currentInteractable)
		{
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
		}
	}

    //When we hit interact key and perform any action
    private void HandleInteractionInput()
	{
        if(Input.GetKeyDown(interactKey) && currentInteractable != null && Physics.Raycast(playerCamera.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance, interactionLayer))
		{
            currentInteractable.OnInteract();
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
