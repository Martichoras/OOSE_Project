using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bomb : MonoBehaviour {

	//Timer
	private float timer = 0.0f;//start timer
	private float timerMax = 3.0f;

	// - explodeGange
	private float explodeRange;

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

		//check if there is something in the area
		CheckBlastZone ();
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






