using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public struct NameAndDistance
{
	public string name;
	public float distance;
}

public class LeaderboardManager : MonoBehaviour
{

	public TextMeshProUGUI[] leaderboardEntries;

	public GameObject[] AI;

	private LevelManager levelManager;

	private float playerDistance;

	public float[] aiDistances;

	private GameObject player;

	public GameObject finishZone;

	private NameAndDistance[] namesAndDistances;

	public float halfDistance;

	public float lapWeight;

	// Start is called before the first frame update
	void Start()
	{
		foreach (TextMeshProUGUI entry in leaderboardEntries)
		{
			entry.text = "";
		}

		levelManager = GameObject.FindObjectOfType<LevelManager>();
		player = GameObject.FindGameObjectWithTag("Car");
		//Debug.Log(player);
		namesAndDistances = new NameAndDistance[2];
	}

	// Update is called once per frame
	void Update()
	{
		UpdateLeaderboard();
	}

	public void UpdateLeaderboard()
	{
		playerDistance = (player.transform.position - finishZone.transform.position).magnitude;

		int i = 0;

		if (levelManager.playerCrossedEnsureCollider)
		{
			playerDistance = halfDistance - playerDistance;
			playerDistance += halfDistance;
		}
		playerDistance += lapWeight * levelManager.currentLapNumber;

		namesAndDistances[i].name = "Player";
		namesAndDistances[i].distance = playerDistance;

		foreach (GameObject ai in AI)
		{
			aiDistances[i] = (ai.transform.position - finishZone.transform.position).magnitude;
			if (ai.GetComponent<AIController>().aiCrossedEnsureCollider)
			{

				aiDistances[i] = halfDistance - aiDistances[i];
				aiDistances[i] += halfDistance;
			}
			aiDistances[i] += lapWeight * ai.GetComponent<AIController>().currentLapNumber;

			i++;
			namesAndDistances[i].name = "AI";
			namesAndDistances[i].distance = aiDistances[i - 1];
		}

		//Debug.Log(namesAndDistances[0].distance + "," + namesAndDistances[1].distance);

		// arrange the distances in order
		BubbleSortDistances();
		i = 0;
		foreach (NameAndDistance nad in namesAndDistances)
		{
			leaderboardEntries[i].text = (i + 1).ToString() + ". " + nad.name;
			i++;
		}

	}

	private void BubbleSortDistances()
	{
		int i, j;
		int N = namesAndDistances.Length;

		for (j = N - 1; j > 0; j--)
		{
			for (i = 0; i < j; i++)
			{
				if (namesAndDistances[i].distance < namesAndDistances[i + 1].distance)
					exchange(i, i + 1);
			}
		}
	}

	private void exchange(int m, int n)
	{
		NameAndDistance temporary;

		temporary = namesAndDistances[m];
		namesAndDistances[m] = namesAndDistances[n];
		namesAndDistances[n] = temporary;
	}
}

