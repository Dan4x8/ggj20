using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldmapController : MonoBehaviour
{
	public Weather weather;
	public Canvas canvas;
	public Text text;



	void Start()
    {
		canvas = FindObjectOfType<Canvas>();
		GetComponent<Image>().alphaHitTestMinimumThreshold = 0.001f;
    }

    public void SelectLocation(BaseEventData ev)
	{
		Debug.Log(Input.mousePosition);

		var pox = Input.mousePosition.x;
		var poy = Input.mousePosition.y;

		float h = canvas.GetComponent<RectTransform>().rect.height;
		float w = canvas.GetComponent<RectTransform>().rect.width;

		//var w = 1024f;
		//var h = 508f*1.06f;

		float avg_w = w / 2f;
		float avg_h = h / 2f;

		pox -= avg_w;
		poy -= avg_h;

		var lat = poy * 180f*1.06f / h - 90f*1.06f;
		var lon = pox * 360f / w - 180f;

		lon = Mathf.Sqrt((pox * pox * ((h * h) / 4)) / (((h * h) / 4) - poy * poy));
		lon = lon * 360f / w;
		if (pox < 0)
			lon *= -1f;
		lat = lat + 90*1.06f;
		weather.RequestWeather(lon, lat, LoadLevel, text);
	}
	private void LoadLevel(Weatherdata weather)
	{
		SceneManager.LoadScene("FinalMainScene");
	}
}
