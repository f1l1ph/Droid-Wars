using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	[SerializeField] private TextMeshProUGUI text;

	[SerializeField] private TextMeshProUGUI blueText;
	[SerializeField] private TextMeshProUGUI redText;

	private int BluePoints = 0;
	private int RedPoints = 0;

	[SerializeField] private Point[] points;
	public Point[] Points { get; private set; }
	
	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
		}
		Points = points;

		UpdateUI();
		StartCoroutine(CountdownCoroutine());		
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
		Debug.Log(team + ":" + amount);
		UpdateUI();
	}

	private void UpdateUI()
	{
		blueText.text = BluePoints.ToString();
		redText.text  = RedPoints.ToString();
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
