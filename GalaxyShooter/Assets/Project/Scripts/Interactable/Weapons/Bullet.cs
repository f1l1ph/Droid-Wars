using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float		damage;
	[SerializeField] private float		forceMultipler;
	public Rigidbody					rb;
	[HideInInspector] public Team		team;

	private void Start()
	{
		//gameObject.SetActive(false);
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<IDamagable>(out IDamagable Idamagable))
		{
			if(Idamagable.Team == team) { return; }
			Idamagable.Hit(damage);
			gameObject.SetActive(false);
		}

		if (other.TryGetComponent<Rigidbody>(out Rigidbody body))
		{
			body.AddForce(transform.position * -forceMultipler, ForceMode.Impulse);
		}

		rb.velocity = Vector3.zero;
		gameObject.SetActive(false);
	}
}
