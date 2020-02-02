using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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

	public void RequestWeather(float lon, float lat, Action<Weatherdata> action, Text text)
	{
		StartCoroutine(WebRequestWeather(lon, lat,action, text));
	}


	IEnumerator WebRequestWeather(float lon, float lat, Action<Weatherdata> callback, Text text)
	{
		string url = "http://api.openweathermap.org/data/2.5/weather?lon=" + lon + "&lat=" + lat + "&appid=634a27863c00b54741b8171aff512e22";
		UnityWebRequest www = UnityWebRequest.Get(url);
		yield return www.SendWebRequest();
		if (string.IsNullOrEmpty(www.error))
		{
			var data = (www.downloadHandler.text);
			var d2 = JsonConvert.DeserializeObject<Weatherdata>(data);
			Data = d2;
			callback.Invoke(d2);
		}
	}
}
