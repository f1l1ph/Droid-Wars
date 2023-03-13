using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField] private GameObject AI;

	private void Start()
	{
		GameManager.OnNextWaveEvenet += OnNextWaveEvenet;
	}

	private void OnNextWaveEvenet()
	{
		//spawn some enmies
		Debug.Log("spawning enemies");
		Instantiate(AI, transform.position, Quaternion.identity);
	}

	private void OnDestroy()
	{
		GameManager.OnNextWaveEvenet -= OnNextWaveEvenet;
	}
}
