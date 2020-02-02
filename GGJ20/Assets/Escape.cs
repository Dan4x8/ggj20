using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{
	public CanvasGroup pauseCanvas;

	private void Start()
	{
		pauseCanvas.alpha = 0f;
		pauseCanvas.gameObject.SetActive(false);
	}
	// Update is called once per frame
	private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
		{
			pauseCanvas.alpha=1f;
			pauseCanvas.gameObject.SetActive(true);
			Time.timeScale = 0f;
		}
    }
}
