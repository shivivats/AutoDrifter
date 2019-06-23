using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIController : MonoBehaviour
{

	public GameObject[] markersList;

	private Rigidbody2D rb;

	public float speed;

	private GameObject nextMarker;

	private int markerIndex;

	public int numberOfLaps;

	public int currentLapNumber;

	public int maxLapPermitted;

	private int currentTrack;

	public bool aiCrossedEnsureCollider;

	public TimeManager timeManager;

	// Start is called before the first frame update
	void Start()
	{
		//markersList = GameObject.FindGameObjectsWithTag("AIMarker");
		rb = GetComponent<Rigidbody2D>();
		markerIndex = 0;
		currentLapNumber = 1;
		currentTrack = 2;
		nextMarker = markersList[markerIndex];
		aiCrossedEnsureCollider = false;
		Debug.Log("Start called from AI controller");
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void FixedUpdate()
	{
		rb.AddForce(transform.up * speed);

		Vector2 markerVector = nextMarker.transform.position - gameObject.transform.position;
		float angle = Vector2.SignedAngle(transform.up, markerVector);

		Debug.Log("marker is at "+ nextMarker.transform.position + " and angle to marker is "+angle);

		float angleDifference = transform.rotation.z - angle;

		if (angleDifference > 0)
		{
			Debug.Log("angle to marker is greater than 0");
			rb.AddTorque(-1f);
		}
		else if (angleDifference < 0)
		{
			Debug.Log("angle to marker is less than 0");
			rb.AddTorque(+1f);
		}
		else
		{
			Debug.Log("angle to marker is 0");
			rb.AddTorque(+1f);
		}

		Debug.Log("end of fixed update");
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject == nextMarker)
		{
			markerIndex++;
			if (markerIndex > markersList.Length - 1)
			{
				markerIndex = 0;
			}
			nextMarker = markersList[markerIndex];
		}
		if (collision.gameObject.CompareTag("EnsureCollider"))
		{
			IncreaseMaxLapPermitted();
		}
		if (collision.gameObject.CompareTag("FinishZone"))
		{
			FinishLap();
		}
	}

	public void IncreaseMaxLapPermitted()
	{
		aiCrossedEnsureCollider = true;
		maxLapPermitted++;
		if (maxLapPermitted > currentLapNumber + 1)
		{
			maxLapPermitted = currentLapNumber + 1;
		}
	}

	public void FinishLap()
	{
		if (currentLapNumber + 1 <= maxLapPermitted)
		{
			currentLapNumber++;
			if (currentLapNumber > numberOfLaps)
			{
				// AI finishes race
				PlayerPrefs.SetInt("PlayerWin", 0);

				PlayerPrefs.SetString("PlayerEndTime", timeManager.RaceTimeAsString());

				SceneManager.LoadScene("End");
			}
			else
			{
				Debug.Log("AI Finished a lap on track number " + currentTrack + " Now at lap number: " + currentLapNumber);
				aiCrossedEnsureCollider = false;
			}
		}
	}

}
