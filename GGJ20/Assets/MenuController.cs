using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	public CanvasGroup pauseCanvas;
	public void ChangeScene(string name)
	{
		SceneManager.LoadScene(name);
		Time.timeScale = 1f;
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void Back()
	{
		pauseCanvas.alpha = 0f;
		pauseCanvas.gameObject.SetActive(false);
		Time.timeScale = 1f;
	}
}
