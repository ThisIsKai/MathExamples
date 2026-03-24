using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoMakerScript : MonoBehaviour { 	// *** GIZMO MAKER SCRIPT ***
													// this is the script drawing all the gizmos
													// based on random values and points on a sphere

	public int objectPosOffset; 					// just in case I need to add a modifier to the transform, 
													// for instance if it was at 0,0,0 I might want to change 
													// that without moving the actual game object

	private float offset_x; 						// the x pos, modified in script later
	private float offset_y;							// the y pos, modified in script later
	private float offset_z;							// the z pos, modified in script later

	private float offset_time;						// delta time modified by random.value,  which I can use
													// if I want a less-fluid transitions over time
		
	public int randCalls; 							// the number of random calls
		

	// GIZMO TOGGLES 								// these bools are allowing the Gizmos to be toggled on/off in the inspector
	public bool DrawSetValues_Outside; 
	public bool DrawWhiteNoise_Outside;
	public bool DrawSubrandom_Outside;
	public bool DrawSetValues_FromCenter;
	public bool DrawWhiteNoise_FromCenter;
	public bool DrawSubrandom_FromCenter;


	List <float> normalVals = new List<float>();							// making a new list to store the 'normalVals' for our set values
	List <float> randomVals = new List<float>();							// making a new list to store the 'randomVals' for our white noise values
	List<float> subrandomVals = new List<float>(); 							// making a new list to store the 'subrandomVals' for our subrandom values


	// GIZMO COLORS
	// this script is HELLA long so I'm making vars up here to make it easier

	float setValsR;
	float setValsG;
	float setValsB;

	float whiteNoiseR;
	float whiteNoiseG;
	float whiteNoiseB;

	float subRandR;
	float subRandG;
	float subRandB;



//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::



	void Start () { 														// START FUNCTION
		float subregions = 24f;												// the number of subreigions for subrandomness
		float subrange = 1f / subregions; 									// the subrange for subrandomness

		for (int i = 0; i < randCalls; i++) {		
			normalVals.Add ((float)i / (float)randCalls);					// SET VALUES, NO RANDOMNESS
		} // END SET VALUES
			
		for ( int i= 0; i < randCalls; i++) {								// WHITE NOISE RANDOMNESS
			randomVals.Add(Random.value);  	
		} // END WHITE NOISE

		for (int i = 0; i < randCalls; i++) {								// SUBRANDOMNESS
			subrandomVals.Add (Random.value * subrange);
			subrandomVals [i] += ((float)i % subregions) / subregions;
		} // END SUBRANDOMNESS

	} // END START FUNCTION


//...........................................................................................................................................


	void FixedUpdate(){ 													// FIXED UPDATE FUNCTION

		if (Input.GetKey (KeyCode.Space)) { 								// if the space key is pressed THEN
			OnDrawGizmos (); 												// run the OnDrawGizmosFunction
		
		} // END GET KEY

	}//END FIXED UPDATE


//...........................................................................................................................................



	private void OnDrawGizmos () {  		 //This is a built-in unity function telling the engine to draw gizmos																	// ON DRAW GIZMOS FUNCTION
		if (Application.isPlaying){ 																				// if in play mode
		
			for (int i = 0; i < randCalls; i++) {
				float offset_x =  this.gameObject.transform.position.x+objectPosOffset;								// setting the offset pos of x
				float offset_y =  this.gameObject.transform.position.y+objectPosOffset;								// setting the offset pos of y
				float offset_z =  this.gameObject.transform.position.z+objectPosOffset;								// setting the offset pos of z
					
						// float offset_time = Random.value * Time.deltaTime;													// setting the offset time

				float sineWaveX = 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * offset_x + Time.timeSinceLevelLoad); 		// making a sine wave based on the x pos and offset
				float sineWaveY = 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * offset_y + Time.timeSinceLevelLoad); 		// making a sine wave based on the y pos and offset
				float sineWaveZ = 0.5f + 0.5f * Mathf.Sin(2 * Mathf.PI * offset_z + Time.timeSinceLevelLoad);		// making a sine wave based on the z pos and offset

				Vector3 spherePoint = Random.insideUnitSphere;														// creating a vector 3 to hold the values/pos 
																													// of a random point inside the unit sphere


// - - - - - - SET VALUES, NO RANDOMNESS  - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
																			// SET VALUES, NO RANDOMNESS
					Vector3 from_X = new Vector3 
						(normalVals [i]*randCalls,							// x value
						spherePoint.y, 										// y value
						offset_z + (offset_z*sineWaveZ));					// z value
					
					Vector3 from_Y = new Vector3 
					(offset_x + (offset_x*sineWaveX),						// x value
						normalVals [i]*randCalls,							// y value
						spherePoint.z);										// z value
					
					Vector3 from_Z =new Vector3 
					(spherePoint.x, 										// x value
						offset_y + (offset_y*sineWaveY),					// y value
						normalVals [i]*randCalls);							// z value
				
					Vector3 to_X = spherePoint; 							// using the random point inside the unit sphere
					Vector3 to_Y = spherePoint; 							// using the random point inside the unit sphere
					Vector3 to_Z= spherePoint;								// using the random point inside the unit sphere

				Gizmos.color = Color.cyan;
	//	Gizmos.color =  new Color(255, 170, 90); 		// PREFFERED SET VALUE GIZMOS LINE COLOR! 
	
				if (DrawSetValues_Outside == true) {  						// DRAW GIZMOS LIMES FOR SET VALUES
					Gizmos.DrawLine (from_X, to_X);
					Gizmos.DrawLine (from_X, to_Y);					
					Gizmos.DrawLine (from_X, to_Z);
					Gizmos.DrawLine (from_Y, to_X); 
					Gizmos.DrawLine (from_Y, to_Y);
					Gizmos.DrawLine (from_Y, to_Z);
					Gizmos.DrawLine (from_Z, to_X);
					Gizmos.DrawLine (from_Z, to_Y);
					Gizmos.DrawLine (from_Z, to_Z);
					Gizmos.DrawLine (-from_X, to_X);
					Gizmos.DrawLine (-from_X, to_Y);
					Gizmos.DrawLine (-from_X, to_Z);
					Gizmos.DrawLine (-from_Y, to_X); 
					Gizmos.DrawLine (-from_Y, to_Y);
					Gizmos.DrawLine (-from_Y, to_Z);
					Gizmos.DrawLine (-from_Z, to_X);
					Gizmos.DrawLine (-from_Z, to_Y);
					Gizmos.DrawLine (-from_Z, to_Z);
					Gizmos.DrawLine (-from_X, -to_X);
					Gizmos.DrawLine (-from_X, -to_Y);
					Gizmos.DrawLine (-from_X, -to_Z);
					Gizmos.DrawLine (-from_Y, -to_X); 
					Gizmos.DrawLine (-from_Y, -to_Y);
					Gizmos.DrawLine (-from_Y, -to_Z);
					Gizmos.DrawLine (-from_Z, -to_X);
					Gizmos.DrawLine (-from_Z, -to_Y);
					Gizmos.DrawLine (-from_Z, -to_Z);
					Gizmos.DrawLine (from_X, -to_X);
					Gizmos.DrawLine (from_X, -to_Y);
					Gizmos.DrawLine (from_X, -to_Z);
					Gizmos.DrawLine (from_Y, -to_X); 
					Gizmos.DrawLine (from_Y, -to_Y);
					Gizmos.DrawLine (from_Y, -to_Z);
					Gizmos.DrawLine (from_Z, -to_X);
					Gizmos.DrawLine (from_Z, -to_Y);
					Gizmos.DrawLine (from_Z, -to_Z);
				} // END DRAW SET VALUES OUTSIDE GIZMOS

					if (DrawSetValues_FromCenter == true) {
						Gizmos.DrawLine (Vector3.zero, to_X);
						Gizmos.DrawLine (Vector3.zero, to_Y);
						Gizmos.DrawLine (Vector3.zero, to_Z);
						Gizmos.DrawLine (Vector3.zero, from_X); 
						Gizmos.DrawLine (Vector3.zero, from_Y);
						Gizmos.DrawLine (Vector3.zero, from_Z);
						Gizmos.DrawLine (Vector3.zero, -to_X);
						Gizmos.DrawLine (Vector3.zero, -to_Y);
						Gizmos.DrawLine (Vector3.zero, -to_Z);
						Gizmos.DrawLine (Vector3.zero, -from_X); 
						Gizmos.DrawLine (Vector3.zero, -from_Y);
						Gizmos.DrawLine (Vector3.zero, -from_Z);
					} // END DRAW SET VALUE CENTER GIZMOS
					


// - - - - - - WHITE NOISE - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
															// WHITE NOISE
				from_X = new Vector3 
					(randomVals [i]*randCalls,									// x value
						spherePoint.y,											// y value
						offset_z + (offset_z*sineWaveZ));						// z value

				from_Y = new Vector3 
					(offset_x + (offset_x*sineWaveX), 							// x value
						randomVals [i]*randCalls,								// y value
						spherePoint.z);											// z value

				from_Z =new Vector3 
					(spherePoint.x,												// x value
						offset_y + (offset_y*sineWaveY),						// y value
						randomVals [i]*randCalls);								// z value

				to_X = spherePoint; 											// using the random point inside the unit sphere
				to_Y = spherePoint; 											// using the random point inside the unit sphere
				to_Z= spherePoint; 												// using the random point inside the unit sphere

				Gizmos.color = Color.magenta;
			//	Gizmos.color = new Color(255, 210, 90);			//  WHITE NOISE GIZMOS LINE COLOR 

				if (DrawWhiteNoise_Outside == true) {							// DRAW GIZMOS LINE FOR WHITE NOISE 
					Gizmos.DrawLine (from_X, to_X);
					Gizmos.DrawLine (from_X, to_Y);
					Gizmos.DrawLine (from_X, to_Z);
					Gizmos.DrawLine (from_Y, to_X); 
					Gizmos.DrawLine (from_Y, to_Y);
					Gizmos.DrawLine (from_Y, to_Z);
					Gizmos.DrawLine (from_Z, to_X);
					Gizmos.DrawLine (from_Z, to_Y);
					Gizmos.DrawLine (from_Z, to_Z);
					Gizmos.DrawLine (-from_X, to_X);
					Gizmos.DrawLine (-from_X, to_Y);
					Gizmos.DrawLine (-from_X, to_Z);
					Gizmos.DrawLine (-from_Y, to_X); 
					Gizmos.DrawLine (-from_Y, to_Y);
					Gizmos.DrawLine (-from_Y, to_Z);
					Gizmos.DrawLine (-from_Z, to_X);
					Gizmos.DrawLine (-from_Z, to_Y);
					Gizmos.DrawLine (-from_Z, to_Z);
					Gizmos.DrawLine (-from_X, -to_X);
					Gizmos.DrawLine (-from_X, -to_Y);
					Gizmos.DrawLine (-from_X, -to_Z);
					Gizmos.DrawLine (-from_Y, -to_X); 
					Gizmos.DrawLine (-from_Y, -to_Y);
					Gizmos.DrawLine (-from_Y, -to_Z);
					Gizmos.DrawLine (-from_Z, -to_X);
					Gizmos.DrawLine (-from_Z, -to_Y);
					Gizmos.DrawLine (-from_Z, -to_Z);
					Gizmos.DrawLine (from_X, -to_X);
					Gizmos.DrawLine (from_X, -to_Y);
					Gizmos.DrawLine (from_X, -to_Z);
					Gizmos.DrawLine (from_Y, -to_X); 
					Gizmos.DrawLine (from_Y, -to_Y);
					Gizmos.DrawLine (from_Y, -to_Z);
					Gizmos.DrawLine (from_Z, -to_X);
					Gizmos.DrawLine (from_Z, -to_Y);
					Gizmos.DrawLine (from_Z, -to_Z);
				} // END DRAW WHITE NOISE OUTSIDE GIZMOS

					if (DrawWhiteNoise_FromCenter == true) {
						Gizmos.DrawLine (Vector3.zero, to_X);
						Gizmos.DrawLine (Vector3.zero, to_Y);
						Gizmos.DrawLine (Vector3.zero, to_Z);
						Gizmos.DrawLine (Vector3.zero, from_X); 
						Gizmos.DrawLine (Vector3.zero, from_Y);
						Gizmos.DrawLine (Vector3.zero, from_Z);
						Gizmos.DrawLine (Vector3.zero, -to_X);
						Gizmos.DrawLine (Vector3.zero, -to_Y);
						Gizmos.DrawLine (Vector3.zero, -to_Z);
						Gizmos.DrawLine (Vector3.zero, -from_X); 
						Gizmos.DrawLine (Vector3.zero, -from_Y);
						Gizmos.DrawLine (Vector3.zero, -from_Z);
				} // END DRAW WHITE NOISE CENTER GIZMOS



// - - - - SUBRANDOM - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
																				// SUBRANDOM
				from_X = new Vector3 
					(subrandomVals [i]*randCalls,								// x value
						spherePoint.y, 											// y value
						offset_z + (offset_z*sineWaveZ));						// z value
					
				from_Y = new Vector3 
					(offset_x + (offset_x*sineWaveX),							// x value
						subrandomVals [i]*randCalls,							// y value
						spherePoint.z);											// z value

				from_Z =new Vector3 
					(spherePoint.x,												// x value
						offset_y + (offset_y*sineWaveY),						// y value
						subrandomVals [i]*randCalls);							// z value

				to_X = spherePoint; 											// using the random point inside the unit sphere
				to_Y = spherePoint;												// using the random point inside the unit sphere
				to_Z= spherePoint;												// using the random point inside the unit sphere

				Gizmos.color = Color.yellow;
				//Gizmos.color = new Color (255,120,90); 			// SUBRANDOM GIZMOS LINE COLOR

				if (DrawSubrandom_Outside == true) {							// DRAW GIZMOS LINE FOR SUBRANDOM VALUES
					Gizmos.DrawLine (from_X, to_X);
					Gizmos.DrawLine (from_X, to_Y);
					Gizmos.DrawLine (from_X, to_Z);
					Gizmos.DrawLine (from_Y, to_X); 
					Gizmos.DrawLine (from_Y, to_Y);
					Gizmos.DrawLine (from_Y, to_Z);
					Gizmos.DrawLine (from_Z, to_X);
					Gizmos.DrawLine (from_Z, to_Y);
					Gizmos.DrawLine (from_Z, to_Z);
					Gizmos.DrawLine (-from_X, to_X);
					Gizmos.DrawLine (-from_X, to_Y);
					Gizmos.DrawLine (-from_X, to_Z);
					Gizmos.DrawLine (-from_Y, to_X); 
					Gizmos.DrawLine (-from_Y, to_Y);
					Gizmos.DrawLine (-from_Y, to_Z);
					Gizmos.DrawLine (-from_Z, to_X);
					Gizmos.DrawLine (-from_Z, to_Y);
					Gizmos.DrawLine (-from_Z, to_Z);
					Gizmos.DrawLine (-from_X, -to_X);
					Gizmos.DrawLine (-from_X, -to_Y);
					Gizmos.DrawLine (-from_X, -to_Z);
					Gizmos.DrawLine (-from_Y, -to_X); 
					Gizmos.DrawLine (-from_Y, -to_Y);
					Gizmos.DrawLine (-from_Y, -to_Z);
					Gizmos.DrawLine (-from_Z, -to_X);
					Gizmos.DrawLine (-from_Z, -to_Y);
					Gizmos.DrawLine (-from_Z, -to_Z);
					Gizmos.DrawLine (from_X, -to_X);
					Gizmos.DrawLine (from_X, -to_Y);
					Gizmos.DrawLine (from_X, -to_Z);
					Gizmos.DrawLine (from_Y, -to_X); 
					Gizmos.DrawLine (from_Y, -to_Y);
					Gizmos.DrawLine (from_Y, -to_Z);
					Gizmos.DrawLine (from_Z, -to_X);
					Gizmos.DrawLine (from_Z, -to_Y);
					Gizmos.DrawLine (from_Z, -to_Z);
				}  // END DRAW SUBRANDOM OUTSIDE GIZMOS

					if (DrawSubrandom_FromCenter == true) {
						Gizmos.DrawLine (Vector3.zero, to_X);
						Gizmos.DrawLine (Vector3.zero, to_Y);
						Gizmos.DrawLine (Vector3.zero, to_Z);
						Gizmos.DrawLine (Vector3.zero, from_X); 
						Gizmos.DrawLine (Vector3.zero, from_Y);
						Gizmos.DrawLine (Vector3.zero, from_Z);
						Gizmos.DrawLine (Vector3.zero, -to_X);
						Gizmos.DrawLine (Vector3.zero, -to_Y);
						Gizmos.DrawLine (Vector3.zero, -to_Z);
						Gizmos.DrawLine (Vector3.zero, -from_X); 
						Gizmos.DrawLine (Vector3.zero, -from_Y);
						Gizmos.DrawLine (Vector3.zero, -from_Z);
				}  // END DRAW SUBRANDOM CENTER GIZMOS

			} // END FOR LOOP

		} // END IF PLAYING

	} // END ON DRAW GIZMOS FUNCTION

// ........................................................................................................................................................................................


}//END GIZMO MAKER SCRIPT

//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
	