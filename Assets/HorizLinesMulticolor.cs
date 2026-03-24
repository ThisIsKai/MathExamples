using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizLinesMulticolor : MonoBehaviour {


	private float offset1;
	private float offset2;
	public int randCalls = 100;
	List <float> normalVals = new List<float>();
	List <float> randomVals = new List<float>();
	List<float> subrandomVals = new List<float>();

	// Use this for initialization
	void Start () {

		//set, no randomness
		for (int i = 0; i < randCalls; i++) {
			normalVals.Add ((float)i / (float)randCalls); 
		}//end for 1
		//whitenoise
		for ( int i= 0; i < randCalls; i++) {
			randomVals.Add(Random.value); 
			//whitenoise
		}//end for 2
		float subregions = 24f;
		float subrange = 1f / subregions;
		for (int i = 0; i < randCalls; i++) {
			subrandomVals.Add (Random.value * subrange);
			//Debug.Log (" # " + i.ToString () + " : " + subrandomVals[i].ToString() + " plus " + (((float)i % subregions) / subregions).ToString());
			subrandomVals [i] += ((float)i % subregions) / subregions;

		}//end for 3
	}//end start

	void FixedUpdate(){

		if (Input.GetKey (KeyCode.Space)) {
			OnDrawGizmos ();

		}
	}//end update

	private void OnDrawGizmos () {
		if (Application.isPlaying){
			for (int i = 0; i < randCalls; i++) {
				float offset1 = this.gameObject.transform.position.y;
				float offset2 = Random.value * Time.deltaTime;

				//*** NORMAL VALS HEIGHT
				Vector3 from2 = new Vector3 (normalVals [i]*100f, offset1*offset2,  0);
				Vector3 to2 = new Vector3 (normalVals [i]*100f, offset1 + from2.y++,  0);
				Gizmos.color = Color.white;
				Gizmos.DrawLine (to2, from2); // horizontal flux



				// NORMAL VALS
				Vector3 from1 = new Vector3 (normalVals [i]*100f, offset1 + (offset1*offset2), 0);
				Vector3 to1 = new Vector3 (from1.x++, offset1+(-offset1*offset2)+3, 0);
				Gizmos.color = Color.yellow;
				Gizmos.DrawLine (from1, to1); // horizontal flux



				// WHITE NOISE
				from1 = new Vector3 (randomVals [i]*100f, offset1 + (offset1*offset2), 0);
				to1 = new Vector3 (from1.x++, offset1+(-offset1*offset2)+3, 0);
				Gizmos.color = Color.magenta;
				Gizmos.DrawLine (from1, to1); 



				//SUBRANDOM
				from1 = new Vector3 (subrandomVals [i]*100f, offset1 + (offset1*offset2), 0);
				to1 = new Vector3 (from1.x++, offset1+(-offset1*offset2)+3, 0);
				Gizmos.color = Color.cyan;
				Gizmos.DrawLine (from1, to1); 


			} //end for
		} //end if playing
	} //end on draw gizmos
} //end script
