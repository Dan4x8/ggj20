using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Weather : MonoBehaviour
{
	private static Weather weather;
	private Weatherdata _data;
	public Weatherdata Data { get { return _data; } private set { _data = value; } }
	public static Weather Current
	{
		get { return weather.GetComponent<Weather>(); }
	}
	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
		weather = this;
	}

	public void RequestWeather(float lon, float lat, Action<Weatherdata> action)
	{
		StartCoroutine(WebRequestWeather(lon, lat,action));
	}


	IEnumerator WebRequestWeather(float lon, float lat, Action<Weatherdata> callback)
	{
		string url = "http://api.openweathermap.org/data/2.5/weather?lon=" + lon + "&lat=" + lat + "&appid=634a27863c00b54741b8171aff512e22";
		UnityWebRequest www = UnityWebRequest.Get(url);
		yield return www.SendWebRequest();
		if (www.error == null)

		{
			var data = (www.downloadHandler.text);
			var d2 = JsonConvert.DeserializeObject<Weatherdata>(data);
			Debug.Log(d2.name + " - " + d2.sys.country);
			Data = d2;
			callback.Invoke(d2);
		}
	}
}
