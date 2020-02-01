using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldmapController : MonoBehaviour
{
	public	Weather weather;
    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.001f;
    }

    public void SelectLocation(BaseEventData ev)
	{
		Debug.Log(Input.mousePosition);

		var pox = Input.mousePosition.x;
		var poy = Input.mousePosition.y;
		var w = 1024f;
		var h = 508f;


		float avg_w = w / 2f;
		float avg_h = h / 2f;

		var lat = poy * 180f / h - 90f;
		var lon = pox * 360f / w - 180f;
		lon = (1f - Mathf.Abs(lat / 90f)) * lon;
		weather.RequestWeather(lon, lat, LoadLevel);
	}

	private void LoadLevel(Weatherdata weather)
	{
		SceneManager.LoadScene("Sandbox - DAN");
	}
}
