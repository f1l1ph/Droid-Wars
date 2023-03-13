using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void Play(int sceneIndex)
	{
		SceneManager.LoadScene(sceneIndex);
	}

	public void Activate(GameObject toActivate)
	{
		toActivate.SetActive(true);
	}

	public void Deactivate(GameObject toDeactivate)
	{
		toDeactivate.SetActive(false);
	}

	public void Exit()
	{
		Application.Quit();
	}
}
