using UnityEngine;
using System.Collections;

public class BallColliderControl : MonoBehaviour {
	public float moveForce = 5, boostMltiplier = 10, normalSpeed = 0.5f;
	public Transform map;
	public float maxSpeed = 50f;
	public float rotSpeed = 180f;
	public Vector3 mapRenderrerBounds;
	public Vector3 ballPos, ballEdge;
	public Vector2 bounceback; // For bouncing off walls
	public float boundx,boundy;
	public float radius;
	Rigidbody2D myBody;

	// Use this for initialization
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
		// Get edge of ball sprite
		ballEdge = this.GetComponent<Renderer>().bounds.size;
		// Get radius of the ball
		radius = (ballEdge.x)/2;
	}
	// Update is called once per frame
	void Update () {
		ballPos = transform.position;
		// Restrict ball from getting out of the map bound
		// Catch right bound
		if ((ballPos.x + ballEdge.x) > boundx){
			myBody.velocity = new Vector3(0,0,0);
			bounceback.x = ballPos.x-1;
			bounceback.y = ballPos.y;
			myBody.MovePosition(bounceback);
		}else if((ballPos.y + ballEdge.y) > boundy){
			// Catch top bound
			myBody.velocity = new Vector3(0,0,0);
			bounceback.x = ballPos.x;
			bounceback.y = ballPos.y-1;
			myBody.MovePosition(bounceback);
		}else if((ballPos.x - ballEdge.x) < -boundx){
			// Catch left bound
			myBody.velocity = new Vector3(0,0,0);
			bounceback.x = ballPos.x+1;
			bounceback.y = ballPos.y;
			myBody.MovePosition(bounceback);
		}else if((ballPos.y - ballEdge.y) < -boundy){
			// Catch bottom bound
			myBody.velocity = new Vector3(0,0,0);
			bounceback.x = ballPos.x;
			bounceback.y = ballPos.y+1;
			myBody.MovePosition(bounceback);
		}

	}
	void OnCollisionEnter2D(Collision2D coll) {
			if (coll.gameObject.tag == "Player"){
					Debug.Log("Player Collided");
					foreach(ContactPoint2D hitPoints in coll.contacts){
						Vector2 hitPoint = hitPoints.point;
						Debug.Log(GetCollisionAngle(coll.gameObject.transform,this.GetComponents<CircleCollider2D>()[0],hitPoint));
					}
			}
	}
	public float GetCollisionAngle(Transform hitobjectTransform, CircleCollider2D collider, Vector2 contactPoint)
  {
      Vector2 collidertWorldPosition = new Vector2(hitobjectTransform.position.x, hitobjectTransform.position.y);
      Vector3 pointB = contactPoint - collidertWorldPosition;

      float theta = Mathf.Atan2(pointB.x, pointB.y);
      float angle = (360 - ((theta * 180) / Mathf.PI)) % 360;
      return angle;
  }
}
