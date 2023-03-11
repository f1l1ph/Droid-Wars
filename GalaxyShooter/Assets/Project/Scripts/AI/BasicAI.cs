using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

	private void Awake()
	{
		StartCoroutine(StartLate());
	}

	private IEnumerator StartLate()
	{
		yield return new WaitForSeconds(3);
		agent.SetDestination(GameManager.Instance.Points[FindClosestPoint()].transform.position);
	}

	private int FindClosestPoint()
	{
		float distance = 0;
		int index = 0;
		for (int i = 0; i < GameManager.Instance.Points.Length; i++)
		{
			if(i == 0)
			{
				index = i;
				distance = Vector3.Distance(transform.position, GameManager.Instance.Points[i].transform.position);
			}
			if(distance > Vector3.Distance(transform.position, GameManager.Instance.Points[i].transform.position))
			{
				distance = Vector3.Distance(transform.position, GameManager.Instance.Points[i].transform.position);
				index = i;
			}
		}
		return index;
	}
}
