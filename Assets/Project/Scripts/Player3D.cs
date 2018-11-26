using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3D : MonoBehaviour {

	public float speed;
	public float invincibilityDuration;
	public float speedUpDuration;

	private bool usedInvincibility;
	private bool usedSpeedUp;

	private float invincibilityTimer;
	private float speedUpTimer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float speedMultiplier = speedUpTimer > 0 ? 2f : 1f;
			
		if (Input.GetMouseButton (0)) {
			Vector2 relativeMousePosition = new Vector2 (Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

			if (relativeMousePosition.x < 0.5f) {
				GetComponent<Rigidbody> ().velocity = new Vector3 (-speed * speedMultiplier, 0, 0);
			} else if (relativeMousePosition.x > 0.5f) {
				GetComponent<Rigidbody> ().velocity = new Vector3 (speed * speedMultiplier, 0, 0);
			}
		} else {
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}

		// Keep the player within bounds.
		float zDistance = Camera.main.transform.position.z - transform.position.z;


		float leftLimit = Camera.main.ScreenToWorldPoint(new Vector3(
			0,
			0,
			-zDistance / (Mathf.Cos(Camera.main.transform.localEulerAngles.x * Mathf.Deg2Rad))
		)).x;

		float rightLimit = Camera.main.ScreenToWorldPoint(new Vector3(
			Screen.width,
			0,
			-zDistance / (Mathf.Cos(Camera.main.transform.localEulerAngles.x * Mathf.Deg2Rad))
		)).x;

		if (transform.position.x < leftLimit) {
			transform.position = new Vector3 (leftLimit, transform.position.y, transform.position.z);
		} else if (transform.position.x > rightLimit) {
			transform.position = new Vector3 (rightLimit, transform.position.y, transform.position.z);
		}

		// Use powerups.
		if (invincibilityTimer > 0f) {
			invincibilityTimer -= Time.deltaTime;
			transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
		} else {
			transform.localScale = Vector3.one;
		}

		if (speedUpTimer > 0f) {
			speedUpTimer -= Time.deltaTime;
		}
	}

	public void ActivateInvincibility () {
		if (usedInvincibility)
			return;

		usedInvincibility = true;

		invincibilityTimer = invincibilityDuration;
	}

	public void ActivateSpeedUp () {
		if (usedSpeedUp)
			return;

		usedSpeedUp = true;

		speedUpTimer = speedUpDuration;
	}
}
