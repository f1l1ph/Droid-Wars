using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float		damage;
	[SerializeField] private float		forceMultipler;
	public Rigidbody					rb;
	[SerializeField] private GameObject particles;
	[HideInInspector] public TeamType	team;

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.GetComponent<Collider>().isTrigger == true) { return; }

		if (collision.gameObject.TryGetComponent<IDamagable>(out IDamagable Idamagable))
		{
			if(Idamagable.Team == team) { return; }
			Idamagable.Hit(damage);
			gameObject.SetActive(false);
		}

		if (collision.gameObject.TryGetComponent<Rigidbody>(out Rigidbody body))
		{
			body.AddForce(transform.position * -forceMultipler, ForceMode.Impulse);
		}
		GameObject instance = Instantiate(particles, collision.contacts[0].point, Quaternion.identity);
		Destroy(instance, 1.5f);
		rb.velocity = Vector3.zero;
		gameObject.SetActive(false);
	}
}
