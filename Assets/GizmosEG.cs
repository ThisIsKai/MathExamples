using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosEG : MonoBehaviour {
	public Transform bodyA, bodyB;

	// Use this for initialization
	void Start () {
		
	}//END START
	
	// Update is called once per frame
	void Update () {
		
	}//END UPDATE
	private void OnDrawGizmos(){ //built in unity function
//		
//		if (Application.isPlaying) { //built in unity function
//			Gizmos.DrawCube (transform.position, Vector3.one);//built in unity function
//			Gizmos.DrawRay(transform.position, transform.forward); //built in unity function
		Gizmos.color=Color.red;
		Gizmos.DrawLine (transform.position, bodyA.position);

		Gizmos.color=Color.cyan;
//		Gizmos.DrawLine (transform.position, bodyB.position);
		Gizmos.DrawLine(transform.position, transform.position+transform.forward);


		Gizmos.color = Color.magenta;
		float dotprod = Vector3.Dot (bodyA.position.normalized, bodyB.position.normalized);
		Gizmos.DrawRay (transform.position, Vector3.up * dotprod);

//
//		} //end if application is playing

	}//END ON DRAW GIZMOS
}//END SCRIPT
