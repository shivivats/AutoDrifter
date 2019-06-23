using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeManager : MonoBehaviour
{

	public TextMeshProUGUI lapTime;

	public TextMeshProUGUI raceTime;

	private DateTime raceStartTime;

	private DateTime lapStartTime;



	// Start is called before the first frame update
	void Start()
	{
		raceStartTime = DateTime.Now;
		lapStartTime = DateTime.Now;
	}

	// Update is called once per frame
	void Update()
	{
		TimeSpan lap = DateTime.Now.Subtract(lapStartTime);
		//lap.ToString("{0:mm:ss.ff}");
		lapTime.text = "Lap: " + lap.Minutes.ToString("D2") + ":" + lap.Seconds.ToString("D2") + "." + lap.Milliseconds.ToString("000");
		TimeSpan race = DateTime.Now.Subtract(raceStartTime);
		raceTime.text = "Race: " + race.Minutes.ToString("D2") + ":" + race.Seconds.ToString("D2") + "." + race.Milliseconds.ToString("000");

	}

	public void ResetLap()
	{
		lapStartTime = DateTime.Now;
	}

	public string RaceTimeAsString() {
		TimeSpan race = DateTime.Now.Subtract(raceStartTime);
		return race.Minutes.ToString("D2") + ":" + race.Seconds.ToString("D2") + "." + race.Milliseconds.ToString("000");
	}
}
