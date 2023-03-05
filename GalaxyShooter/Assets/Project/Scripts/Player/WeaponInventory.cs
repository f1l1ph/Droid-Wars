using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponInventory : MonoBehaviour
{
	[SerializeField] private GameObject[]	guns;
	[SerializeField] private PlayerInput	input;
	[SerializeField] private Transform		shootTop;
	[SerializeField] private Team			team;
	[SerializeField] private PlayerHealth	shealth;
	[SerializeField] private RectTransform	UIContainer;

	private bool haveSelectedGun = false;
	private IGun selectedGun;
	private int selectedGunIndex;

	private void Start()
	{
		guns = new GameObject[3];

		input.actions["SelectGun"].performed += Scroll_performed;
		input.actions["DropSelectedGun"].performed += DropGun_performed;
		input.actions["Fire"].performed += Fire_Performed;
		input.actions["Fire"].canceled  += Fire_Canceled;
	}

	public void AddToInventory(GameObject gun)
	{
		if(gun.GetComponent<IGun>() == null) { return; }

		for (int i = 0; i < 3; i++) 
		{
			if (guns[i] == null)
			{
				guns[i] = gun;
				IGun gun1 = guns[i].GetComponent<IGun>();
				gun1.Team = team;
				gun1.UI.weaponUI.transform.SetParent(UIContainer);
				gun1.UI.weaponUI.transform.rotation = new Quaternion(0, 0, 0, 0);

				SelectGun(i);
				return;
			}
			else if (guns[i] != null && i == 2)
			{
				DropGun(selectedGunIndex, false);
				guns[selectedGunIndex] = gun;
				IGun gun1 = guns[selectedGunIndex].GetComponent<IGun>();
				gun1.Team = team;
				gun1.UI.weaponUI.transform.SetParent(UIContainer);
				gun1.UI.weaponUI.transform.rotation = new Quaternion(0, 0, 0, 0);

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
				selectedGun.UI.weaponUI.localScale *= 1.1f;
			}
			else
			{
				if (guns[i] != null)
				{
					guns[i].SetActive(false);
					guns[i].GetComponent<IGun>().UI.weaponUI.localScale = Vector3.one;
				}
			}
		}
		haveSelectedGun = true;
	}

	private void DropGun(int index, bool selectAnother)
	{
		//delete from inventory, it is finaly working
		guns[index].GetComponent<Interactable>().weaponRBody.isKinematic = false;
		guns[index].GetComponent<Collider>().GetComponent<Collider>().enabled = true;
		guns[index].GetComponent<UIGun>().weaponUI.transform.SetParent(guns[index].transform);
		guns[index].GetComponent<Interactable>().isPicked = false;
		guns[index].transform.SetParent(null);
		haveSelectedGun = false;

		guns[index] = null;

		if (selectAnother)
		{
			for (int i = 0; i < 3; i++) 
			{
				if (guns[i] != null)
				{
					SelectGun(i);
				}
			}
		}
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

	private void DropGun_performed(InputAction.CallbackContext obj)
	{
		if (guns[selectedGunIndex] == null || !haveSelectedGun) { return; }

		DropGun(selectedGunIndex, true);
	}

	private void Scroll_performed(InputAction.CallbackContext obj)
	{
		if (!haveSelectedGun) { return; }
		float value = obj.ReadValue<float>();
		Debug.Log(value);
		if (value > 0)
		{
			int index = selectedGunIndex;
			index += 1;
			if (index > 2) { index = 0; }
			for (int i = 0; i < 3; i++)
			{
				if (guns[index] != null)
				{
					SelectGun(index);
					return;
				}
				else if (index >= 3)
				{
					index = 0;
				}
				index++;
			}

		}
		else if(value < 0)
		{

		}
	}
	#endregion
}
