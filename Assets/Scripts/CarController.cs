using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarController : MonoBehaviour
{

	private Camera mainCamera;
	private Rigidbody2D rb;

	public float velocity;

	public float turnForce;

	public float turnDirection;

	private Vector2 currVelocity = Vector2.zero;

	public float stopTime;

	private float resetTimer;

	private float resetWait;

	private Vector3 lastKnownTrackPosition;

	public TextMeshProUGUI resetHeader;

	public TextMeshProUGUI resetNumber;

	private bool isResetVisualRunning;

	public float smoothTime = 0.1f;

	private Vector3 cameraSmoothVelocity = Vector3.zero;

	// the track is only the y values
	// just an array of y values?
	// or make a tilemap and fill use the y values of the tiles?

	// Start is called before the first frame update
	void Start()
	{
		mainCamera = Camera.main;
		rb = gameObject.GetComponent<Rigidbody2D>();
		resetTimer = 0;
		resetWait = 4f;
		lastKnownTrackPosition = transform.position;
		resetHeader.gameObject.SetActive(false);
		resetNumber.gameObject.SetActive(false);
		isResetVisualRunning = false;
	}

	void FixedUpdate()
	{
		Vector3 goalPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, mainCamera.transform.position.z);

		//mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, goalPosition, ref cameraSmoothVelocity, smoothTime);

		mainCamera.transform.position = goalPosition;

		MoveCarOnTrack();

		if (Input.GetKey(KeyCode.Space))
		{
			rb.AddTorque(Input.GetAxis("Button") * turnForce * turnDirection);
		}
		else if (Input.GetKeyUp(KeyCode.Space))
		{
			//rb.velocity = transform.up * velocity;

		}
		else if (Input.GetKeyDown(KeyCode.Space))
		{

		}

		if (rb.velocity.magnitude < 0.001)
		{
			resetTimer += Time.deltaTime;
		}
		else
		{
			resetTimer = 0f;
			if (isResetVisualRunning)
				StopCoroutine(ResetVisual());
		}

		if (resetTimer >= 1f)
		{
			if (!isResetVisualRunning)
			{
				StartCoroutine(ResetVisual());
				isResetVisualRunning = true;
			}
		}

	}



	public void MoveCarOnTrack()
	{
		// get the track here and move according to the track

		//rb.velocity = transform.up * velocity;
		//Debug.Log(transform.up);
		if (!GetComponent<CarCollider>().outOfTrack)
		{
			rb.AddForce(transform.up * velocity);

		}
		else
		{
			//lastKnownTrackPosition = transform.position;
			//currVelocity = Vector2.zero;
			//rb.velocity = Vector2.SmoothDamp(rb.velocity, Vector2.zero, ref currVelocity, stopTime);
		}

	}

	public void SetTurnDirection(bool right)
	{
		if (right)
		{
			turnDirection = 1f;

		}
		else
		{
			turnDirection = 1f;
		}
	}

	private void ResetCar()
	{
		transform.position = lastKnownTrackPosition;
		transform.rotation = Quaternion.identity;
		Debug.Log("resetting car to: " + lastKnownTrackPosition);
	}

	private IEnumerator ResetVisual()
	{
		resetHeader.gameObject.SetActive(true);
		resetNumber.gameObject.SetActive(true);
		resetNumber.text = "3";
		yield return new WaitForSeconds(1f);
		resetNumber.text = "2";
		yield return new WaitForSeconds(1f);
		resetNumber.text = "1";
		yield return new WaitForSeconds(1f);
		resetTimer = 0f;
		resetHeader.gameObject.SetActive(false);
		resetNumber.gameObject.SetActive(false);
		ResetCar();	
	}

	public void SetLastKnownTrackPosition() {
		lastKnownTrackPosition = transform.position;
	}
}
