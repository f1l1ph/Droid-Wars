using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public bool Taken { get; private set; }
	public TeamType Team { get; private set; }
	private int takenValue = 0;
	[SerializeField] private TeamColorChanger changer;

	[SerializeField] private int pointPower = 1;

	private List<IDamagable> units = new();

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.GetComponent<IDamagable>() != null)
		{
			IDamagable damagable;
			damagable = other.transform.GetComponent<IDamagable>();
			units.Add(damagable);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.transform.GetComponent<IDamagable>() != null)
		{
			IDamagable damagable;
			damagable = other.transform.GetComponent<IDamagable>();
			units.Remove(damagable);
		}
	}

	private void Start()
	{
		Taken = false;
		StartCoroutine(CheckPoints());
	}

	IEnumerator CheckPoints()
	{
		changer.ChangeCollor(Team, Taken);

		int blue = 0;
		int red = 0;

		if (units != null)
		{
			foreach (var unit in units)
			{
				if (unit.Team == TeamType.Blue)
				{
					blue++;
				}
				if (unit.Team == TeamType.Red)
				{
					red++;
				}
			}
			
		}
		if (Team == TeamType.Blue)
		{
			takenValue += blue;
			takenValue -= red;
		}
		if (Team == TeamType.Red)
		{
			takenValue += red;
			takenValue -= blue;
		}

		takenValue = Mathf.Min(takenValue, 20);

		if (takenValue > 10) { GameManager.Instance.AddPoints(Team, pointPower); }

		if (takenValue == 0)
		{
			Taken = false;
		}
		else
		{
			Taken = true;
		}
		if (takenValue < 0)
		{
			if (Team == TeamType.Blue)
			{
				Team = TeamType.Red;
			}
			else
			{
				Team = TeamType.Blue;
			}
			takenValue *= -1;
		}

		if (units != null) 
		{
			List<IDamagable> unitsToDestroy = new List<IDamagable>();

			foreach (var unit in units)
			{
				if (unit.isDeath)
				{
					unitsToDestroy.Add(unit);
				}
			} 
			foreach(var unit in unitsToDestroy)
			{
				units.Remove(unit);
			}
		}

		yield return new WaitForSeconds(1);
		StartCoroutine(CheckPoints());
	}
}