using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndManager : MonoBehaviour
{

	public TextMeshProUGUI endText;

	public TextMeshProUGUI endTime;

	// Start is called before the first frame update
	void Start()
	{
		if (PlayerPrefs.GetInt("PlayerWin") == 0)
		{
			endText.text = "You lost!";

		}
		else
		{
			endText.text = "You won!";
		}

		endTime.text = "Your time: "+PlayerPrefs.GetString("PlayerEndTime");
	}

	// Update is called once per frame
	void Update()
	{

	}
}
