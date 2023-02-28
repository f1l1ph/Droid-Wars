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

	void Update()
	{
		Vector2 lookDelta = Mouse.current.delta.ReadValue() * mouseSensitivity * Time.deltaTime;

		float mouseX = lookDelta.x;
		float mouseY = lookDelta.y;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);

		transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		playerBody.Rotate(Vector3.up * mouseX);
	}
}