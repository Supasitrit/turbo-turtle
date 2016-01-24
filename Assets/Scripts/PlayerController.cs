﻿using UnityEngine;
using System.Collections;
// To get input from joystick.cs (MobileStick)
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

	public float moveForce = 5, boostMltiplier = 10;
	Rigidbody2D myBody;

	void Start () {
		myBody = this.GetComponents<Rigidbody2D>()[0];
	}


	void FixedUpdate () {
		// Get x and y from joystick
		Vector2 moveVec = new Vector2 (CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")) * moveForce;
		bool isSprinting = CrossPlatformInputManager.GetButton ("Sprint");
		// Return True if sprint button is being pressed
		// Return boostMltiplier if true else 1
		Debug.Log (isSprinting ? boostMltiplier : 3);
		// Move my adding The Force to the body
		myBody.AddForce(moveVec * ( isSprinting ? boostMltiplier : 3));
	}
}
