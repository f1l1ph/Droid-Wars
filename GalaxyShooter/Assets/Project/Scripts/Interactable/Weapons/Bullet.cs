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
	[HideInInspector] public Transform	gun;

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
		float distance = 0;
		int point = 0;
		for (int i = 0; i < collision.contacts.Length; i++) 
		{
			if(distance == 0)
			{
				distance = Vector3.Distance(collision.contacts[i].point, gun.position);
			}

			if(distance > Vector3.Distance( collision.contacts[i].point, gun.position))
			{
				distance = Vector3.Distance(collision.contacts[i].point, gun.position);
				point = i;
			}
		}
		GameObject instance = Instantiate(particles, collision.contacts[point].point, Quaternion.identity);
		Destroy(instance, 1f);
		rb.velocity = Vector3.zero;
		gameObject.SetActive(false);
	}
}
