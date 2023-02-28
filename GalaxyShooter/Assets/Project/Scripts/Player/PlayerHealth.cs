using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	[SerializeField] private float	maxHealth = 100;
	[SerializeField] private Slider	healthSlider;
	private float					health;

	private void Start()
	{
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
		Time.timeScale = 0;
		Debug.Log("Player died");
	}
}
