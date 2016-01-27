using UnityEngine;
using System.Collections;
// To get input from joystick.cs (MobileStick)
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour {

	public float moveForce = 5, boostMltiplier = 10, normalSpeed = 0.5f;
	public Transform map;
	// public Vector3 mapColliderBounds;
	public Vector3 mapRenderrerBounds;
	public Vector3 playerPos, playerEdge;
	private Vector3 velocity = Vector3.zero;
	public Vector2 bounceback; // For bouncing player off walls
	public float boundx,boundy;
	Rigidbody2D myBody;

	void Start () {
		myBody = this.GetComponents<Rigidbody2D>()[0];
		transform.position = new Vector3(0, 0, 0);
		// mapColliderBounds = GetComponent<Collider>().bounds.size;
	  mapRenderrerBounds = map.GetComponent<Renderer>().bounds.size;
		bounceback = new Vector2(0,0);
		boundx = mapRenderrerBounds.x/2;// Get Right bound of the map
		boundy = mapRenderrerBounds.y/2;// Get Up bound of the map
	}

	void FixedUpdate () {
		// Get x and y from joystick
		Vector2 moveVec = new Vector2 (CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")) * moveForce;

		bool isSprinting = CrossPlatformInputManager.GetButton ("Sprint");// This boolean return True if sprint button is being pressed

		playerPos = transform.position;// Get player position
		playerEdge = this.GetComponent<Renderer>().bounds.size;// Get edge of player sprite

		// Debug.Log (isSprinting ? boostMltiplier : normalSpeed);
		// Debug.Log (playerPos.x);
		// Debug.Log (playerEdge);
		Debug.Log (playerPos.y - playerEdge.y);
		Debug.Log (-boundy);
		// Debug.Log (boundy);

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
		myBody.AddForce(moveVec * ( isSprinting ? boostMltiplier : normalSpeed));
		}
	}
}
