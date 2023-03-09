using UnityEngine;

public interface IGun{
	public float EnergyConsumption { get; set; }
	public UIGun UI { get; set; }
	public TeamType Team { get ; set; }
	public Transform GunTop { get; set; }
	public bool Shoot(Vector3 target) { return true; }
}

public enum PotionType{
	Health,
	Armor,
	Energy
}

public class Interactable : MonoBehaviour
{ 
	public PickupType PickType { get; private set; }
	public PotionType PotionType { get; private set; }

	[SerializeField] private PickupType pickType;
	[SerializeField] private PotionType potionType;

	[SerializeField] private float amountOfPotion;
	private IGun weapon;
	public Rigidbody weaponRBody;
	public bool isPicked = false;
	

	private void Start()
	{
		PickType   = pickType;
		PotionType = potionType;

		//chechking for weapons
		if(!transform.TryGetComponent<Rigidbody>(out weaponRBody) && pickType == PickupType.Weapon) 
		{
			Debug.LogError("I don't have a rigidbody");
		}

		if(weapon == null && pickType == PickupType.Weapon)
		{
			if (!transform.TryGetComponent<IGun>(out weapon))
			{
				Debug.LogError("You don't have any IGun component on " + transform.name);
			}
		}
	}

	public void Interact(PickUp sender)
	{
		if (PickType == PickupType.Potion && PotionType == PotionType.Health)
		{
			sender.playerHealth.Heal(amountOfPotion);
			gameObject.SetActive(false);
		}
		else if(PickType == PickupType.Potion && PotionType == PotionType.Energy)
		{
			sender.playerHealth.AddEnergy(amountOfPotion);
			gameObject.SetActive(false);
		}
		else if(PickType == PickupType.Weapon)
		{
			sender.PickUpWeapon(gameObject);
		}
	}
}