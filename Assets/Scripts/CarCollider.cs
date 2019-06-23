using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollider : MonoBehaviour
{

	public bool outOfTrack;

	private CarController carController;

	private LevelManager levelManager;

	public GameObject leftArrow;
	public GameObject rightArrow;

	private void Start()
	{
		outOfTrack = false;
		carController = gameObject.GetComponent<CarController>();
		levelManager = GameObject.FindObjectOfType<LevelManager>();
		leftArrow.SetActive(false);
		rightArrow.SetActive(false);
	}

	private void Update()
	{
		//leftArrow.transform.rotation = Quaternion.identity;
		//rightArrow.transform.rotation = Quaternion.identity;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Track"))
		{
			//outOfTrack = true;
		}
		if (collision.gameObject.CompareTag("TrackCollider"))
		{
			carController.turnDirection = 0f;
			leftArrow.SetActive(false);
			rightArrow.SetActive(false);
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Track"))
		{
			outOfTrack = false;
		}
		if (collision.gameObject.CompareTag("TrackCollider"))
		{
			carController.turnDirection = collision.gameObject.GetComponent<TrackCollider>().turnDirection;
			if (collision.gameObject.GetComponent<TrackCollider>().turnDirection == -1f)
			{
				leftArrow.SetActive(false);
				rightArrow.SetActive(true);

			}
			else if (collision.gameObject.GetComponent<TrackCollider>().turnDirection == 1f)
			{
				leftArrow.SetActive(true);
				rightArrow.SetActive(false);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Track"))
		{
			outOfTrack = false;
		}
		if (collision.gameObject.CompareTag("TrackCollider"))
		{
			//Debug.Log("collided with a track collider" + collision.gameObject);
			carController.turnDirection = collision.gameObject.GetComponent<TrackCollider>().turnDirection;
			if (collision.gameObject.GetComponent<TrackCollider>().turnDirection == -1f)
			{
				leftArrow.SetActive(false);
				rightArrow.SetActive(true);

			}
			else if (collision.gameObject.GetComponent<TrackCollider>().turnDirection == 1f)
			{
				leftArrow.SetActive(true);
				rightArrow.SetActive(false);
			}
		}
		if (collision.gameObject.CompareTag("FinishZone"))
		{
			if (collision.gameObject.GetComponent<FinishZone>().restartTrack)
			{
				levelManager.RestartTrack();
			}
			else
			{
				levelManager.FinishTrack(collision.gameObject.GetComponent<FinishZone>().trackNumber);
			}
		}
		if (collision.gameObject.CompareTag("EnsureCollider"))
		{
			levelManager.IncreaseMaxLapPermitted();
		}
	}
}
