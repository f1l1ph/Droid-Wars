using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public bool Taken { get; private set; }
	private TeamType team;
	private int takenValue;

	private List<IDamagable> units;

	private void OnTriggerEnter(Collider other)
	{
		if(other.TryGetComponent<IDamagable>(out IDamagable damagable))
		{
			units.Add(damagable);
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent<IDamagable>(out IDamagable damagable))
		{
			units.Remove(damagable);
		}
	}

	private void Start()
	{
		Taken = false;
		StartCoroutine(CheckPoints());
	}

	private IEnumerator CheckPoints()
	{
		int blue = 0;
		int red = 0;

		foreach(var unit in units)
		{
			if(unit.Team == TeamType.Blue)
			{
				blue++;
			}
			if (unit.Team == TeamType.Red)
			{
				red++;
			}
		}
		if(team == TeamType.Blue)
		{
			takenValue += blue;
			takenValue -= red;
		}
		else if(team == TeamType.Red)
		{
			takenValue += red;
			takenValue -= blue;
		}

		takenValue = Mathf.Min(takenValue, 100);

		if(takenValue == 0)
		{
			Taken = false;
		}
		else
		{
			Taken = true;
		}
		if(takenValue < 0)
		{
			if(team == TeamType.Blue)
			{
				team = TeamType.Red;
			}
			if(team == TeamType.Red)
			{
				team = TeamType.Blue;
			}
		}
		yield return new WaitForSeconds(1);

		StartCoroutine(CheckPoints());
	}
}