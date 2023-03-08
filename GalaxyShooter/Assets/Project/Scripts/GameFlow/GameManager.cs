using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	private int BluePoints;
	private int RedPoints;

	[SerializeField] private Point[] points;

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
}
