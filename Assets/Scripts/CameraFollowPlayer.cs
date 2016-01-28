using UnityEngine;
using System.Collections;

 public class CameraFollowPlayer : MonoBehaviour {

     public Transform target;

     // Update is called once per frame
    	void Update () {
    		if(target != null) {
          // Grab player position
    			Vector3 targPos = target.position;
    			targPos.z = transform.position.z;

    			// Consider using Vector3.Lerp for neat effects!
    			transform.position = targPos;
    		}
    	}
      void FixedUpdate() {
        // Grab our rotation quaternion
        Quaternion rot = target.rotation;

        // Grab the Z euler angle
        float z = rot.eulerAngles.z;

        // Recreate the quaternion
        rot = Quaternion.Euler( 0, 0, z );

        // Feed the quaternion into our rotation
        transform.rotation = rot;
      }
 }
