using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
	[SerializeField] private float		mouseSensitivity = 100f;
	[SerializeField] private Transform	playerBody;

	private float xRotation = 0f;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	void FixedUpdate()
	{
		Vector2 lookDelta = mouseSensitivity * Mouse.current.delta.ReadValue();

		float mouseX = lookDelta.x;
		float mouseY = lookDelta.y;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		playerBody.Rotate(Vector3.up * mouseX);
	}
}