using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class WWWTrial : MonoBehaviour
{
	public float lat = 0;
	public float lon = 0;
	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(DelayedStart());
	}

	
	IEnumerator DelayedStart()
	{
		string url = "http://api.openweathermap.org/data/2.5/weather?lat=" +lat.ToString() +"&lon=" +lon.ToString() +"&appid=634a27863c00b54741b8171aff512e22";
		UnityWebRequest www = UnityWebRequest.Get(url);
		yield return www.SendWebRequest();
		if (www.error == null)

		{
			var data = (www.downloadHandler.text);
			Debug.Log(data);
		}
    }
	public void UpdateLocation()
	{
		StartCoroutine(DelayedStart());
	}
}
