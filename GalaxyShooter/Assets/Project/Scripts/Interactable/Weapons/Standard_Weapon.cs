using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Standard_Weapon : MonoBehaviour, IGun
{
	[SerializeField] private Transform gunTop;
	public Transform GunTop { get; set; }

	[SerializeField] private float bulletSpeed = 1;
	[SerializeField] private Bullet[] bullets;
	private int activeIndex = 0;

	[SerializeField] private float shootTime = 0.3f;
	private float currentTime;
	private bool canShoot = true;

	public UIGun UI { get; set; }

	public TeamType Team { get; set; }

	private void Start()
	{
		UI = GetComponent<UIGun>();
		GunTop = gunTop;
		currentTime = shootTime;
	}

	private void FixedUpdate()
	{
		if(currentTime > 0 && !canShoot)
		{
			currentTime -= Time.fixedDeltaTime;
		}
		else if(currentTime <= 0)
		{
			canShoot = true;
			currentTime = shootTime;
		}
	}

	public void Shoot(Vector3 target)
	{
		if (!canShoot) { return; }
		canShoot = false;
		bullets[activeIndex].transform.SetParent(null);
		bullets[activeIndex].rb.velocity = Vector3.zero;
		bullets[activeIndex].team = Team;
		bullets[activeIndex].transform.position = gunTop.transform.position;
		bullets[activeIndex].gameObject.SetActive(true);
		Vector3 direction = target - gunTop.position;
		direction.Normalize();
		bullets[activeIndex].rb.AddForce(direction * bulletSpeed, ForceMode.Impulse);

		int i = activeIndex;
		StartCoroutine(DeactivateBulletOverTime(i));

		activeIndex++;
		if (activeIndex >= bullets.Length) { activeIndex = 0; }
	}

	private IEnumerator DeactivateBulletOverTime(int bulletIndex) 
	{
		yield return new WaitForSeconds(2);
		
		bullets[bulletIndex].transform.SetParent(transform);
		bullets[bulletIndex].gameObject.SetActive(false);
		
	}
}
