using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	[SerializeField] private TextMeshProUGUI text;

	private int BluePoints;
	private int RedPoints;

	[SerializeField] private Point[] points;

	private void Awake()
	{
		StartCoroutine(CountdownCoroutine());

		if (Instance != null && Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
		}
	}

	public void AddPoints(TeamType team, int amount)
	{
		if(team == TeamType.Blue)
		{
			BluePoints += amount;
		}
		if(team == TeamType.Red)
		{
			RedPoints += amount;
		}
	}

	IEnumerator CountdownCoroutine()
	{
		int count = 3;
		while (count > 0)
		{
			text.text = count.ToString();
			yield return new WaitForSeconds(1f);
			count--;
		}

		text.text = "Game started!";
		yield return new WaitForSeconds(1f);
		text.text = string.Empty;

		// Start the game here
	}
}
