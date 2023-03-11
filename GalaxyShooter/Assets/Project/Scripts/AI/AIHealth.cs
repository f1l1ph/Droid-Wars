using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour, IDamagable
{
	public bool isDeath { get; set; }

	private float health = 100;
	private readonly float maxHealth = 100;
	[SerializeField] private GameObject DeathParticles;

	public TeamType Team { get; set; }

	private void Start()
	{
		isDeath = false;
		Team = TeamType.Red;
		health = maxHealth;
	}

	public void Heal(float healthAmount)
	{
		health += Mathf.Abs(healthAmount);
		health = Mathf.Min(health, maxHealth);
	}

	public void Hit(float damage)
	{
		health -= Mathf.Abs(damage);
		if(health < 0) { Die(); }
	}

	private void Die()
	{
		GameObject instance = Instantiate(DeathParticles, transform.position, Quaternion.identity);
		Destroy(instance, 1f);
		isDeath = true;
		gameObject.SetActive(false);
	}
}
