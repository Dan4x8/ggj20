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
			Debug.Log(data);
			var d2 = JsonConvert.DeserializeObject<Weatherdata>(data);
			Debug.Log(d2.rain._1h);
			/*
			double cloud = data.clouds.all;

			string latitude = data["coord.lat"].Value<string>();
			double rain = data.rain._3h;
			string sun = data.weather.description;
			Debug.Log(data.clouds.all);
			*/
		}
	}
}
