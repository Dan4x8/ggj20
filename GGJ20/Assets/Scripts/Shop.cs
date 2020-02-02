using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
	float time = 1f;
	int waterTic = 0;
	public int waterCost = 5;
	public Button waterButton;
    // Start is called before the first frame update
    public void AddWater()
    {
        if(waterTic>=waterCost)
		{
			waterTic = 0;
		}
    }

    // Update is called once per frame
    void Update()
    {
		time -= Time.deltaTime;
		if(time<0)
		{
			time = 1;
			waterTic++;
		}
		if(waterTic<waterCost)
		{
			waterButton.enabled = false;
		}
		else
		{
			waterButton.enabled = true;
		}
    }

	public enum Ressource
	{
		WaterCan,
	}


	public void Cost(string ressource)
	{
		switch(ressource)
		{
			case "WaterCan":
				waterTic = 0;
				break;
		}
	}
}
