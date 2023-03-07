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

	private void Start()
	{
		Team = team;

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

	public void Die()
	{
		//todo finish this
		//Time.timeScale = 0;
		Debug.Log("Player died");
	}
}
