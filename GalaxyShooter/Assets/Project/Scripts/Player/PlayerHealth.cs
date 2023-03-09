using UnityEngine;
using UnityEngine.UI;

public enum TeamType
{
	Blue,
	Red
}

public interface IDamagable
{
	public TeamType Team { get; set; }

	public void Heal(float healthAmount);
	public void Hit(float damage);
}

public class PlayerHealth : MonoBehaviour,IDamagable
{
	public TeamType Team { get; set; }
	[SerializeField] private TeamType team;

	[SerializeField] private float	maxHealth = 100;
	[SerializeField] private Slider	healthSlider;
	private float					health;

	[SerializeField] private float	maxEnergy = 100;
	[SerializeField] private Slider energySlider;
	public float Energy { get; private set; }

	private void Start()
	{
		Team = team;

		Energy = maxEnergy;
		energySlider.maxValue = maxEnergy;
		energySlider.value = Energy;

		health = maxHealth;
		healthSlider.maxValue = maxHealth;
		healthSlider.value = health;
	}

	public void Heal(float healAmount)
	{
		health += healAmount;
		health = Mathf.Min(health, maxHealth);
		healthSlider.value = health;
		if (health <= 0)
		{
			Die();
		}
	}

	public void Hit(float damage)
	{
		health -= damage;
		healthSlider.value = health;
		if (health <= 0)
		{
			Die();
		}
	}

	public void ConsumeEnergy(float amount) 
	{
		Energy -= amount;
		UpdateEnergySlider();
	}

	public void AddEnergy(float amount) 
	{
		Energy += amount;
		UpdateEnergySlider();
	}

	private void UpdateEnergySlider()
	{
		energySlider.value = Energy;
	}

	public void Die()
	{
		//todo finish this
		//Time.timeScale = 0;
		Debug.Log("Player died");
	}
}
