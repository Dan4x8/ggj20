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

	int waterHighTic = 0;
	public int waterHighCost = 30;
	public Button waterHighBut;

	int waterLowTic = 0;
	public int waterLowCost = 10;
	public Button waterLowBut;

	int tempLowTic = 0;
	public int tempLowCost = 10;
	public Button tempLowBut;

	int tempHighTic = 0;
	public int tempHighCost = 10;
	public Button tempHighBut;
	// Start is called before the first frame update
	public void AddWater()
    {
        if(waterTic>=waterCost)
		{
			waterTic = 0;
		}
    }

	public void WaterResHigh()
	{
		if(waterHighTic>=waterHighCost)
		{
			waterHighTic = 0;
		}
	}
	
	public void WaterResLow()
	{
		if(waterHighTic>=waterHighCost)
		{
			waterLowTic = 0;
		}
	}

	public void TempResLow()
	{
		if (tempLowTic >= tempLowCost)
		{
			tempLowTic = 0;
		}
	}

	public void TempResHigh()
	{
		if (tempHighTic >= tempHighCost)
		{
			tempHighTic = 0;
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


		if(waterHighTic<waterHighCost)
		{
			waterHighBut.enabled = false;
		}
		else
		{
			waterHighBut.enabled = true;
		}

		if (waterLowTic < waterLowCost)
		{
			waterLowBut.enabled = false;
		}
		else
		{
			waterLowBut.enabled = true;
		}

		if (tempLowTic < tempLowCost)
		{
			tempLowBut.enabled = false;
		}
		else
		{
			tempLowBut.enabled = true;
		}

		if (tempHighTic < tempHighCost)
		{
			tempHighBut.enabled = false;
		}
		else
		{
			tempHighBut.enabled = true;
		}
	}

	public void Cost(string ressource)
	{
		switch(ressource)
		{
			case "WaterCan":
				waterTic = 0;
				break;
			case "WaterResHigh":
				waterHighTic = 0;
				break;
			case "WaterResLow":
				waterLowTic = 0;
				break;
			case "TempResLow":
				tempLowTic = 0;
				break;
			case "TempResHigh":
				tempHighTic = 0;
				break;
		}
	}
}
