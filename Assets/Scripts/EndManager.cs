using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndManager : MonoBehaviour
{

	public TextMeshProUGUI endText;

	public TextMeshProUGUI endTime;

	public float delayTime;

	public TextMeshProUGUI inputText;

	private bool canRestart;

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

		endTime.text = "Your time: " + PlayerPrefs.GetString("PlayerEndTime");

		canRestart = false;
		StartCoroutine(InputDelay());
	}

	// Update is called once per frame
	void Update()
	{
		if (canRestart)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				SceneManager.LoadScene("Main");
			}
		}
	}

	private IEnumerator InputDelay()
	{
		yield return new WaitForSeconds(delayTime);
		inputText.gameObject.SetActive(true);
		canRestart = true;
	}
}
