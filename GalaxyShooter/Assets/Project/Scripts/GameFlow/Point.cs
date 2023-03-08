using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public bool Taken { get; private set; }
	public TeamType Team { get; private set; }
	private int takenValue = 0;
	[SerializeField] private TeamColorChanger changer;

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
		if(Team == TeamType.Blue)
		{
			takenValue += blue;
			takenValue -= red;
		}
		if(Team == TeamType.Red)
		{
			takenValue += red;
			takenValue -= blue;
		}

		takenValue = Mathf.Min(takenValue, 100);

		if (takenValue > 60) { GameManager.Instance.AddPoints(Team, 1); }

		if (takenValue == 0)
		{
			Taken = false;
		}
		else
		{
			Taken = true;
		}
		if(takenValue < 0)
		{ 
			if(Team == TeamType.Blue)
			{
				Team = TeamType.Red;
			}
			else
			{
				Team = TeamType.Blue;
			}
			takenValue *= -1;
		}
		yield return new WaitForSeconds(1);

		StartCoroutine(CheckPoints());
	}
}