using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
	DataProvider provider;
	public TMPro.TMP_Text text;
	private void Start()
	{
		provider = GameObject.Find("GameOverKeepAlive").GetComponent<DataProvider>();
		switch(provider.cause)
		{
			case CauseDeath.TempHigh:
				text.text = "Cause: Your tree died of heat";
				break;
			case CauseDeath.TempLow:
				text.text = "Cause: Your tree froze to death";
				break;
			case CauseDeath.Waterhigh:
				text.text = "Cause: Your tree drowned";
				break;
			case CauseDeath.WaterLow:
				text.text = "Cause: Your tree died of thirst";
				break;
		}
	}

	public void ChangeScene(string name)
	{
		DestroyImmediate(provider.gameObject);
		GetComponent<MenuController>().ChangeScene(name);
	}
}
