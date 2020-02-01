using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Weatherdata
{
	public Lonlat coord;
	public Weather[] weather;
	public Data main;
	public Wind wind;
	public Cloud clouds;
	public Water rain;
	public Snow snow;
	public Sun sys;

	public struct Lonlat
	{
		public double lon;
		public double lat;
	}

	public struct Weather
	{
		public int id;
		public string main;
		public string description;
		public string icon;
	}

	[JsonProperty(PropertyName = "base")]
	public string _base;

	public struct Data
	{
		public double temp;
		public double feels_like;
		public double pressure;
		public double humidity;
		public double temp_min;
		public double temp_max;
		public double sea_level;
		public double grnd_level;
	}

	public struct Wind
	{
		public double speed;
		public double deg;
	}

	public struct Cloud
	{
		public double all;
	}

	public struct Water
	{
		[JsonProperty(PropertyName = "1h")]
		public double _1h;
		[JsonProperty(PropertyName = "3h")]
		public double _3h;
	}

	public struct Snow
	{
		public double _1h;
		public double _3h;
	}

	public int dt;

	public struct Sun
	{
		public string type;
		public int id;
		public double message;
		public string country;
		public int sunrise;
		public int sunset;
	}

	public int timezone;

	public int id;

	public string name;

	public int cod;
}