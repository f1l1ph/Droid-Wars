using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Standard_Weapon : MonoBehaviour, IGun
{
	[SerializeField] private Transform gunTop;
	public Transform GunTop { get; set; }
	[SerializeField] private GameObject bullet;

	private void Start()
	{
		GunTop = gunTop;
	}

	public void Shoot(Vector3 target)
	{
		//later fix this because this is just test need to use object pooling

		GameObject instance = Instantiate(bullet, gunTop.transform.position, Quaternion.identity);
		instance.GetComponent<Rigidbody>().AddForce(target, ForceMode.Impulse);
		Debug.Log("Shooting");
		Destroy(instance, 2);
	}
}
