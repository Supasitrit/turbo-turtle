using UnityEngine;
using System.Collections;
// To get input from joystick.cs (MobileStick)
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

	public float moveForce = 5, boostMltiplier = 10, normalSpeed = 0.5f;
	public Transform map;
	public float maxSpeed = 50f;
	public float rotSpeed = 180f;
	public Vector3 mapRenderrerBounds;
	public Vector3 playerPos, playerEdge;
	public Vector2 bounceback; // For bouncing player off walls
	public float boundx,boundy;
	Rigidbody2D myBody;

	void Start () {
		// Grab rigidbody of this sprite
		myBody = this.GetComponents<Rigidbody2D>()[0];
		// Get the map bounds
	  mapRenderrerBounds = map.GetComponent<Renderer>().bounds.size;
		// For creating bounceback from the wall vector
		bounceback = new Vector2(0,0);
		// Get Right bound of the map
		boundx = mapRenderrerBounds.x/2;
		// Get Up bound of the map
		boundy = mapRenderrerBounds.y/2;
		// Get edge of player sprite
		playerEdge = this.GetComponent<Renderer>().bounds.size;

	}

	void FixedUpdate () {
		// Get x and y from joystick
		// Vector2 moveVec = new Vector2 (CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")) * moveForce;

		// ROTATE the ship.

		// Grab our rotation quaternion
		Quaternion rot = transform.rotation;

		// Grab the Z euler angle
		float z = rot.eulerAngles.z;

		// Change the Z angle based on joystick input
		z -= CrossPlatformInputManager.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;

		// Recreate the quaternion
		rot = Quaternion.Euler( 0, 0, z );

		// Feed the quaternion into our rotation
		transform.rotation = rot;

		// Get player position
		playerPos = transform.position;
		// Get input from button
		if(CrossPlatformInputManager.GetButton("Accelerate")){
			// The playerPos move when Accelerate is pressed
			Vector3 velocity = new Vector3(0,maxSpeed * Time.deltaTime, 0);
			playerPos += (rot * velocity)*2;
			Debug.Log(velocity);
		}

		// Restrict player from getting out of the map bound
		// Catch right bound
		if ((playerPos.x + playerEdge.x) > boundx){
			myBody.velocity = new Vector3(0,0,0);
			bounceback.x = playerPos.x-1;
			bounceback.y = playerPos.y;
			myBody.MovePosition(bounceback);
		}else if((playerPos.y + playerEdge.y) > boundy){
			// Catch top bound
			myBody.velocity = new Vector3(0,0,0);
			bounceback.x = playerPos.x;
			bounceback.y = playerPos.y-1;
			myBody.MovePosition(bounceback);
		}else if((playerPos.x - playerEdge.x) < -boundx){
			// Catch left bound
			myBody.velocity = new Vector3(0,0,0);
			bounceback.x = playerPos.x+1;
			bounceback.y = playerPos.y;
			myBody.MovePosition(bounceback);
		}else if((playerPos.y - playerEdge.y) < -boundy){
			// Catch bottom bound
			myBody.velocity = new Vector3(0,0,0);
			bounceback.x = playerPos.x;
			bounceback.y = playerPos.y+1;
			myBody.MovePosition(bounceback);
		}else{
		// Move my adding The Force to the body
		// myBody.AddForce(moveVec * ( isAccelerating ? boostMltiplier : normalSpeed));
		// Finally, update our position!!
		transform.position = playerPos;
		}
	}
}
