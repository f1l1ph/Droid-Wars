using UnityEngine;
using UnityEngine.InputSystem;

public enum PickupType
{
	Standard,
	Weapon,
	Potion
}

public class PickUp : MonoBehaviour
{
	[SerializeField] private PlayerInput	input;
	[SerializeField] private Transform		cameraTransform;
	[SerializeField] private LayerMask		interactAble;
	[SerializeField] private float			interactableDistance = 3f;
	[SerializeField] private Transform		interactText;

	public PlayerHealth						playerHealth;
	public WeaponInventory					inventory;

	private bool canInteract = false;
	RaycastHit hit;

	private void Start()
	{
		input.actions["Interact"].performed += Interact;
	}

	private void FixedUpdate()
	{
		if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity, interactAble)) 
		{
			if(hit.distance <= interactableDistance)
			{
				interactText.gameObject.SetActive(true);
				canInteract = true;
			}	
		}
		else
		{
			canInteract = false;
			interactText.gameObject.SetActive(false);
		}
	}

	private void Interact(InputAction.CallbackContext obj)
	{
		if (!canInteract) { return; }
		
		if(!hit.transform.TryGetComponent<Interactable>(out var intr)) { return; }

		intr.Interact(this);
	}

	public void PickUpWeapon(GameObject weaponObj)
	{
		weaponObj.transform.SetParent(inventory.transform);
		weaponObj.transform.localPosition = Vector3.zero;
		weaponObj.transform.localRotation = Quaternion.identity;
		weaponObj.GetComponent<Rigidbody>().isKinematic = true;
		weaponObj.GetComponent<Collider>().enabled = false;
		inventory.AddToInventory(weaponObj);
	}
}
