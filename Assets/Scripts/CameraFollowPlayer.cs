// ﻿using UnityEngine;
// using System.Collections;
//
// public class CameraFollowPlayer: MonoBehaviour {
// 	public Transform target;
// 	public float smooth= 5.0f;
// 	public targetx = target.postion[0];
// 	public targety = target.postion[1];
// 	void  Update (){
// 	    transform.position = Vector2.Lerp (
// 	        transform.position, target.position,
// 	        Time.deltaTime * smooth);
// 	}
// }
//


using UnityEngine;
using System.Collections;

 public class CameraFollowPlayer : MonoBehaviour {

     public float dampTime = 5.0f;
     private Vector3 velocity = Vector3.zero;
     public Transform target;

     // Update is called once per frame
     void Update ()
     {
         if (target)
         {
             Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
             Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
             Vector3 destination = transform.position + delta;
             transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
         }

     }
 }
