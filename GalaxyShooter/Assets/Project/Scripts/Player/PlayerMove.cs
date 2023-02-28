using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private PlayerInput	input;
	[Header("physics")]
	[SerializeField] private Rigidbody		playerRB;
	[SerializeField] private Vector2		moveSpeed;
	private float							maxVelocity;
	[SerializeField] private float			maxWalkVelocity;
	[Header("Jump")]
	[SerializeField] private float			jumpForce;
	[SerializeField] private LayerMask		ground;
	private bool							grounded = false;
	[Header("Sprint")]
	[SerializeField] private float			speedMultyplier;
	[SerializeField] private float			maxSprintVelocity;

	private Vector3 fixedMoveInput;

	private void Start()
	{
		maxVelocity = maxWalkVelocity;

		input.actions["Move"].performed += PlayerMove_performed;
		input.actions["Move"].canceled += PlayerMove_canceled;
		input.actions["Jump"].performed += PlayerJump_performed;
		input.actions["Sprint"].performed += PlayerSprint_performed;
		input.actions["Sprint"].canceled += PlayerSprint_canceled;
	}

	void OnDestroy()
	{
		input.actions["Move"].performed -= PlayerMove_performed;
	}

	private void PlayerMove_performed(InputAction.CallbackContext obj)
	{
		Vector2 moveInput = obj.ReadValue<Vector2>();
		fixedMoveInput = new Vector3(moveInput.x * moveSpeed.x, 0, moveInput.y * moveSpeed.y);
	}

	private void PlayerMove_canceled(InputAction.CallbackContext obj)
	{
		fixedMoveInput = Vector3.zero;
	}

	private void PlayerJump_performed(InputAction.CallbackContext obj)
	{
		RaycastHit hit;
		if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, ground))
		{
			if (hit.distance <= 1.5f)
			{
				playerRB.AddForce(transform.up * jumpForce, ForceMode.Impulse);
				grounded = true;
			}
			else
			{
				grounded = false;//later use with jet pack or something simmilar
			}
		}
	}
	private void PlayerSprint_canceled(InputAction.CallbackContext obj)
	{
		maxVelocity = maxWalkVelocity;
	}

	private void PlayerSprint_performed(InputAction.CallbackContext obj)
	{
		fixedMoveInput *= speedMultyplier;
		maxVelocity = maxSprintVelocity;
	}


	private void FixedUpdate()
	{
		playerRB.AddForce(fixedMoveInput.x * Time.fixedDeltaTime * transform.right);
		playerRB.AddForce(fixedMoveInput.z * Time.fixedDeltaTime * transform.forward);

		playerRB.velocity = Vector3.ClampMagnitude(playerRB.velocity, maxVelocity);
	}
}