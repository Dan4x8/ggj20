using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class JsonStuff : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(DelayedStart());
	}


	IEnumerator DelayedStart()
	{
		string url = "http://api.openweathermap.org/data/2.5/weather?q=charlotte&appid=634a27863c00b54741b8171aff512e22";
		UnityWebRequest www = UnityWebRequest.Get(url);
		yield return www.SendWebRequest();
		if (www.error == null)

		{
			var data = (www.downloadHandler.text);
			var d2 = JsonConvert.DeserializeObject<Weatherdata>(data);
			/*
			double cloud = data.clouds.all;

			string latitude = data["coord.lat"].Value<string>();
			double rain = data.rain._3h;
			string sun = data.weather.description;
			Debug.Log(data.clouds.all);
			*/
		}
	}
	private void Update()
	{
		Debug.Log(Input.mousePosition);

		var w = 1024f;
		var h = 508f;


		float avg_w = w / 2f;
		float avg_h = h / 2f;


		var pox = Input.mousePosition.x;
		var poy = Input.mousePosition.y;

		pox -= avg_w;
		poy -= avg_h;

		var lat = poy * 180f / h - 90f;
		//lat *= (Mathf.Abs(lat) / 90f);

		var t = (pox - avg_w) / avg_w;

		/*
		if (pox > avg_w)
			pox *= 2f - Mathf.Abs(lat) / 90f;
		else if (pox < avg_w)
			pox -= (Mathf.Abs(lat) / 90f) * (avg_w - pox);
		
		lon = (1f - Mathf.Abs(lat / 90f)) * lon;
		*/

		//Luca
		var lon = pox * 360f / w - 180f;
		lon = Mathf.Sqrt((pox * pox * ((h * h) / 4)) / (((h * h) / 4) - poy * poy));
		lon = lon * 360f / w;
		if (pox < 0)
			pox *= -1f;
		lat = lat + 90;
		Debug.Log(lon +";" +lat);
	}
}
