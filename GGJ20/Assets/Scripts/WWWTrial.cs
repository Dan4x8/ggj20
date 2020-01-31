using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class WWWTrial : MonoBehaviour
{
	// Start is called before the first frame update
	[System.Obsolete]
	IEnumerator Start()
	{
		string url = "http://api.openweathermap.org/data/2.5/weather?lat=0&lon=12&appid=634a27863c00b54741b8171aff512e22";
		UnityWebRequest www = UnityWebRequest.Get(url);
		yield return www.SendWebRequest();
		if (www.error == null)

		{
			var data = (www.downloadHandler.text);

			double cloud = data.clouds.all;

			string latitude = data["coord.lat"].Value<string>();
			double rain = data.rain._3h;
			string sun = data.weather.description;
			Debug.Log(data.clouds.all);
		}
    }
}
