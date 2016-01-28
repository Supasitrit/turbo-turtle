using UnityEngine;
using System.Collections;
// To get input from joystick.cs (MobileStick)
using UnityStandardAssets.CrossPlatformInput;

public class BotController : MonoBehaviour {

	public float moveForce = 5, boostMltiplier = 10, normalSpeed = 0.5f;
	public Transform map;
	public float maxSpeed = 5f;
	public float rotSpeed = 180f;
	public Vector3 mapRenderrerBounds;
	public Vector3 botPos, botEdge;
	public Vector2 bounceback; // For bouncing bot off walls
	public float boundx,boundy;
	public AudioClip AccelerateSound;
	Rigidbody2D myBody;
	Transform ball;

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
		// Get edge of bot sprite
		botEdge = this.GetComponent<Renderer>().bounds.size;

	}

	void FixedUpdate () {
		if(ball == null) {
			// Find the player's ship!
			GameObject go = GameObject.FindWithTag("Ball");

			if(go != null) {
				ball = go.transform;
				Debug.Log(ball);
			}
		}
		// ROTATE the bot.
		// At this point, we've either found the ball,

		if(ball == null){
			return;	// Try again next frame!
		}
		// HERE -- we know for sure we have a ball. Turn to face it!

		Vector3 dir = ball.position - transform.position;
		dir.Normalize();

		float zAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;

		Quaternion rot = transform.rotation;

		// Recreate the quaternion
		rot = Quaternion.Euler( 0, 0, zAngle );

		// Feed the quaternion into our rotation
		transform.rotation = rot;

		// Get bot position
		botPos = transform.position;
		// The botPos move
		Vector3 velocity = new Vector3(0,maxSpeed * Time.deltaTime, 0);
		botPos += (rot * velocity)*0.25f;
		GetComponent<AudioSource>().PlayOneShot(AccelerateSound);

		// Restrict bot from getting out of the map bound
		// Catch right bound
		if ((botPos.x + botEdge.x) > boundx){
			myBody.velocity = new Vector3(0,0,0);
			bounceback.x = botPos.x-1;
			bounceback.y = botPos.y;
			myBody.MovePosition(bounceback);
		}else if((botPos.y + botEdge.y) > boundy){
			// Catch top bound
			myBody.velocity = new Vector3(0,0,0);
			bounceback.x = botPos.x;
			bounceback.y = botPos.y-1;
			myBody.MovePosition(bounceback);
		}else if((botPos.x - botEdge.x) < -boundx){
			// Catch left bound
			myBody.velocity = new Vector3(0,0,0);
			bounceback.x = botPos.x+1;
			bounceback.y = botPos.y;
			myBody.MovePosition(bounceback);
		}else if((botPos.y - botEdge.y) < -boundy){
			// Catch bottom bound
			myBody.velocity = new Vector3(0,0,0);
			bounceback.x = botPos.x;
			bounceback.y = botPos.y+1;
			myBody.MovePosition(bounceback);
		}else{
		// Move my adding The Force to the body
		// myBody.AddForce(moveVec * ( isAccelerating ? boostMltiplier : normalSpeed));
		// Finally, update our position!!
		transform.position = botPos;
		}
	}
}
