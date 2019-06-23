using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	private int currentTrack;

	public GameObject car;

	public GameObject[] startPoints;

	public int[] numberOfLaps;

	public int currentLapNumber;

	public TextMeshProUGUI lapText;

	public int maxLapPermitted;

	public bool playerCrossedEnsureCollider;

	public GameObject AI;

	public LeaderboardManager leaderboardManager;

	public TimeManager timeManager;

	private void Start()
	{
		PlayerPrefs.SetInt("HasTutorial", 1); // 1 for yes 0 for no tutorial

		// tutorial will have track id 1

		if (PlayerPrefs.GetInt("HasTutorial") == 0)
		{
			currentTrack = 1; // no tutorial
		}
		else
		{
			currentTrack = 0; // yes tutorial
		}

		car.SetActive(false);

		lapText.text = "";

		maxLapPermitted = 1;

		SetCarStartPosition();

		playerCrossedEnsureCollider = false;



	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Return))
		{
			//SetCarStartPosition();
		}
	}

	public void SetCarStartPosition()
	{
		currentTrack++;
		car.gameObject.transform.position = startPoints[currentTrack - 1].transform.position;

		car.gameObject.GetComponent<CarController>().velocity = startPoints[currentTrack - 1].GetComponent<StartPoint>().velocity;
		car.gameObject.GetComponent<CarController>().turnForce = startPoints[currentTrack - 1].GetComponent<StartPoint>().turnForce;

		car.gameObject.GetComponent<Rigidbody2D>().drag = startPoints[currentTrack - 1].GetComponent<StartPoint>().linearDrag;
		car.gameObject.GetComponent<Rigidbody2D>().angularDrag = startPoints[currentTrack - 1].GetComponent<StartPoint>().angularDrag;

		currentLapNumber = 1;

		car.gameObject.GetComponent<CarController>().SetLastKnownTrackPosition();

		SetLapText();

		Debug.Log("Setting car position to: " + startPoints[currentTrack - 1].transform.position + " for track number: " + currentTrack);

		car.SetActive(true);
	}

	public void FinishTrack(int number)
	{
		//Debug.Log("finished a track");
		if (number == currentTrack)
		{
			if (currentTrack != 1)
			{
				Debug.Log("current lap:" + currentLapNumber + " and max lap permitted:" + maxLapPermitted);
				if (currentLapNumber + 1 <= maxLapPermitted)
				{
					currentLapNumber++;
					if (currentLapNumber > numberOfLaps[currentTrack - 1])
					{
						Debug.Log("finished track number " + currentTrack);

						//car.SetActive(false);

						//car.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
						//car.GetComponent<Rigidbody2D>().angularVelocity = 0f;

						//car.transform.rotation = Quaternion.identity;

						//SetCarStartPosition();

						PlayerPrefs.SetInt("PlayerWin", 1);

						PlayerPrefs.SetString("PlayerEndTime", timeManager.RaceTimeAsString());

						SceneManager.LoadScene("End");
					}
					else
					{
						Debug.Log("Finished a lap on track number " + currentTrack + " Now at lap number: " + currentLapNumber);
						SetLapText();
						playerCrossedEnsureCollider = false;
						GameObject.FindObjectOfType<TimeManager>().ResetLap();
					}
				}
			}
			else if (currentTrack == 1)
			{
				Debug.Log("finished track number " + currentTrack);

				car.SetActive(false);

				car.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				car.GetComponent<Rigidbody2D>().angularVelocity = 0f;

				car.transform.rotation = Quaternion.identity;

				SetCarStartPosition();

				AI.SetActive(true);

				leaderboardManager.gameObject.SetActive(true);
				timeManager.gameObject.SetActive(true);
			}

		}
	}

	public void RestartTrack()
	{
		car.SetActive(false);
		car.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		car.GetComponent<Rigidbody2D>().angularVelocity = 0f;
		car.transform.rotation = Quaternion.identity;
		car.gameObject.transform.position = startPoints[currentTrack - 1].transform.position;
		car.SetActive(true);
	}

	public int GetCurrentTrack()
	{
		return currentTrack;
	}

	public void SetCurrentTrack(int newTrackNumber)
	{
		currentTrack = newTrackNumber;
	}

	private void SetLapText()
	{
		lapText.text = "Lap: " + currentLapNumber + "/" + numberOfLaps[currentTrack - 1];
	}

	public void IncreaseMaxLapPermitted()
	{
		playerCrossedEnsureCollider = true;
		maxLapPermitted++;
		if (maxLapPermitted > currentLapNumber + 1)
		{
			maxLapPermitted = currentLapNumber + 1;
		}
	}
}
