using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataProvider : MonoBehaviour
{
	public CauseDeath cause;

	private void Start()
	{
		DontDestroyOnLoad(gameObject);
	}
}
