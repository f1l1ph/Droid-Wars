using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponInventory : MonoBehaviour
{
	[SerializeField] private GameObject[]	guns;
	[SerializeField] private PlayerInput	input;
	[SerializeField] private Transform		shootTop;

	private bool haveSelectedGun = false;
	private IGun selectedGun;
	private int selectedGunIndex;

	private void Start()
	{
		guns = new GameObject[3];

		input.actions["Fire"].performed += Fire_Performed;
		input.actions["Fire"].canceled  += Fire_Canceled;
	}

	public void AddToInventory(GameObject gun)
	{
		if(gun.GetComponent<IGun>() == null) { return; }

		for (int i = 0; i < 3; i++) 
		{
			Debug.Log(i);
			if (guns[i] == null)
			{
				guns[i] = gun;
				SelectGun(i);
				return;
			}
			else if (guns[i] != null && i == 2)
			{

				Debug.Log("I am not working");
				DropGun(selectedGunIndex);
				guns[selectedGunIndex] = gun;
				SelectGun(selectedGunIndex);
				return;
			}
		}
	}

	public void SelectGun(int index)
	{
		if(index < 0 || index > 3) { return; }
		//play animation or sound
		for (int i = 0; i < 3; i++) 
		{
			if(i == index)
			{
				guns[i].SetActive(true);
				selectedGunIndex = i;
				selectedGun = guns[i].GetComponent<IGun>();
			}
			else
			{
				if (guns[i] != null)
				{
					guns[i].SetActive(false);
				}
			}
		}
		haveSelectedGun = true;
	}

	private void DropGun(int index)
	{
		guns[index].GetComponent<Interactable>().weaponRBody.isKinematic = false;
		guns[index].GetComponent<Collider>().GetComponent<Collider>().enabled = false;
	}

	private void Shoot()
	{
		if(!haveSelectedGun) { return; }

		selectedGun.Shoot(shootTop.position);
	}

	#region input actions
	private void Fire_Performed(InputAction.CallbackContext obj)
	{
		Shoot();
	}

	private void Fire_Canceled(InputAction.CallbackContext obj)
	{

	}
	#endregion
}
