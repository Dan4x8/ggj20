using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
	float time = 1f;
	int clock = 0;
	public TMPro.TMP_Text text;

	int waterTic = 3;
	public int waterCost = 3;
	public Button waterButton;

	int waterHighTic = 15;
	public int waterHighCost = 15;
	public Button waterHighBut;

	int waterLowTic = 15;
	public int waterLowCost = 15;
	public Button waterLowBut;

	int tempLowTic = 15;
	public int tempLowCost = 15;
	public Button tempLowBut;

	int tempHighTic = 15;
	public int tempHighCost = 15;
	public Button tempHighBut;

	int growPlusTic = 20;
	public int growPlusCost = 20;
	public Button growPlusBut;

	int growMinusTic = 20;
	public int growMinusCost = 20;
	public Button growMinusBut;
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
	
	public void GrowPlus()
	{
		if (growPlusTic >= growPlusCost)
		{
			growPlusTic = 0;
		}
	}

	public void GrowMinus()
	{
		if (growMinusTic >= growMinusCost)
		{
			growMinusTic = 0;
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
			clock++;
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

		if (growPlusTic < growPlusCost)
		{
			growPlusBut.enabled = false;
		}
		else
		{
			growPlusBut.enabled = true;
		}

		if (growMinusTic < growMinusCost)
		{
			growMinusBut.enabled = false;
		}
		else
		{
			growMinusBut.enabled = true;
		}
		text.text = clock.ToString();
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
			case "GrowPlus":
				growPlusTic = 0;
				break;
			case "GrowMinus":
				growMinusTic = 0;
				break;
		}
	}
}
