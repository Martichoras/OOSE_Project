using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bomb : MonoBehaviour {

	//Timer
	private float timer = 0.0f;//start timer
	private float timerMax = 3.0f;

	// - explodeGange
	private float explodeRange;
	public void SetExplodeRange (float range){
		explodeRange = range;
		}

	private int x;
	private int z;
	private BombBag playerBag;


	 void Start (){
	}

	void Update(){
			//timerCount
		timer += Time.deltaTime;
		// checker if time is equal to or bigger than timerMax, is important because it is floats
		if (timer >= timerMax)
		{
			Explode ();
		}

	}

	private void Explode(){
		float up, down, left, right;

		RaycastHit hit;
		//right
		if (Physics.Raycast (this.transform.position, new Vector3 (1, 0, 0), out hit, explodeRange)) {
			right = hit.distance;//distance is the distance between where we send the ray from and where we hit something.
		}else{
			right = explodeRange;//if we dont hit anything, then the bomb eplode out in the explode range
		}
		//left
		if (Physics.Raycast (this.transform.position, new Vector3 (-1, 0, 0), out hit, explodeRange)) {
			left = hit.distance;
		}else{
			left = explodeRange;
		}
		//up
		if (Physics.Raycast (this.transform.position, new Vector3 (0, 0, 1), out hit, explodeRange)) {
				up = hit.distance;
		} else {
				up = explodeRange;
		}
		//down
		if (Physics.Raycast (this.transform.position, new Vector3 (0, 0, -1), out hit, explodeRange)) {
				down = hit.distance;
		} else {
				down = explodeRange;
		}




		//check if there is something in the area
		//CheckBlastZone ();
		// makes a double forloop, check if the value is 1,

		// if 1, then don't explode
		// if 2 explode wall
		// if 3 explode other bomb
		// if 8 explode player
		// if 9 explode/destroy player2


		//call playerBag to say that bomb is exploded/destroyed - so bombBag will create new bomb. This is done by decresing bombPlaced
		//Instantiate the explosion and explode!!!
		Destroy (gameObject);
	}




	private void CheckBlastZone (){
		//call level generator, function wallCall to check the positions araound the bomb. (int x, int z, int radius) then gets an array with the 0,1,2,3,9,8 (9,8 = players) neghboor values


	}

	private void SetBombData(int x, int z, int radius, BombBag playerBag){


	}


}
