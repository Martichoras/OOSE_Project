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
	private bool isExploded = false;//set isExploded to be false.

	//attach the fire/explosion particles to the bomb script
	public GameObject ExplosionPrefab;

	//the explotion sound array.
	public AudioClip[] ExplosionSound;
	//the die sound (hard to hear because of the explsionsound playes at the same time.)
	public AudioClip MenDieSound;

	 void Start (){
	}

	void Update(){
		//timerCount
		timer += Time.deltaTime;
		// checker if time is equal to or bigger than timerMax, 
		if (timer >= timerMax) {//if time is bigger or equal to timerMax then run the Explode function.
			Explode ();
		}

	}

	//The explosion function 
	public void Explode(){
		if (this.isExploded)//this function does so if to bomb is beside each other and one goes of before the other, then is the other also explodes, but the other will not let the first go off again.
			return;
		
		this.isExploded = true;//set isExploded to true.

		AudioSource.PlayClipAtPoint(ExplosionSound[Random.Range(0, ExplosionSound.Length)], Camera.main.transform.position, 0.5f );//plays the explosion sound when the bomb explodes

		float up, down, left, right;
		float unit = LevelGenerator.gameUnit;// assign LevelGenerator which calls gameUnit, to unit.

		// Check how far we can explode in each direction
		right = this.ExplodeInDirection(this.transform.position, new Vector3(1, 0, 0), this.explodeRange * unit);
		left = this.ExplodeInDirection(this.transform.position, new Vector3(-1, 0, 0), this.explodeRange * unit);
		up = this.ExplodeInDirection(this.transform.position, new Vector3(0, 0, 1), this.explodeRange * unit);
		down = this.ExplodeInDirection(this.transform.position, new Vector3(0, 0, -1), this.explodeRange * unit);

		// Animate the explosion with some spawned particles
		Instantiate(ExplosionPrefab, this.transform.position, Quaternion.identity);

		//check if possible go make the explosion prefabs in each direction
		for (float i = 1; i <= explodeRange; i++) {//forloop, so everytime explodeRange is less then or equal to i, then add i with one
			float range = i * unit;//assign range to i*unit
			if (range <= right){//if range is less than or equal to right (read next comment below)
				Instantiate(ExplosionPrefab, this.transform.position + new Vector3(unit*i, 0, 0), Quaternion.identity);//instansiates the ExplosionPrefab for the bomb instans for the explosions startposision and continues in the right direction in a certain range, Quaternion.identity means that it is not rotated
			}
			if (range <= left){//if range is less than or equal to left (read next comment below)
				Instantiate(ExplosionPrefab, this.transform.position + new Vector3(-unit*i, 0, 0), Quaternion.identity);//instansiates the ExplosionPrefab for the bomb instans for the explosions startposision and continues in the left direction in a certain range, Quaternion.identity means that it is not rotated
			}
			if (range <= up){//if range is less than or equal to up (read next comment below)
				Instantiate(ExplosionPrefab, this.transform.position + new Vector3(0, 0, unit*i), Quaternion.identity);	//instansiates the ExplosionPrefab for the bomb instans for the explosions startposision and continues in the up direction in a certain range, Quaternion.identity means that it is not rotated
			}
			if (range <= down){//if range is less than or equal to down (read next comment below)
				Instantiate(ExplosionPrefab, this.transform.position + new Vector3(0, 0, -unit*i), Quaternion.identity);//instansiates the ExplosionPrefab for the bomb instans for the explosions startposision and continues in the down direction in a certain range, Quaternion.identity means that it is not rotated
			}
		}
		
		//call playerBag to say that bomb is exploded/destroyed - so bombBag will create new bomb. This is done by decreasing bombPlaced
		//Instantiate the explosion and explode!!!
		Destroy (gameObject);//destroys the gameObject
		playerBag.AfterExplosion (x,z);//playerBag calls AfterExplosion with the inputs x,z.
	}
	//in the explosion direction 
	private float ExplodeInDirection(Vector3 origin, Vector3 direction, float distance) {
		if (distance <= 0.0f)
			return 0;//returns 0 if distance is less or equal to 0.0f

		RaycastHit hit; //check if raycast hits anything
		if (!Physics.Raycast(origin, direction, out hit, distance))// cast a raycast to check if it does not hits anything, if it does not, then it only returns the distance the ray was cast in.
			return distance;//retrns the distance if it does not hit anything.


		//if the raycast hits an object then the object is assigned to hit.colider.gameObject. 
		GameObject obj = hit.collider.gameObject;// assigning obj to hit.colider.gameObject
		Vector3 pos = obj.transform.position;// and assigning pos to obj.transform.position

		if (obj.tag == "Crate") {// check it obj is with the tag Crate, if it is then GetComponent to Crate class, destroy obj
			Crate crate = obj.GetComponent<Crate>();// assigning crate to obj.transform.position;
			crate.OnExplode();
			Destroy(obj);//destroy the object with the  "Crate" tag
			return (origin-pos).magnitude; // Return the distance to the crate (the crate will absorb the explosion so things behind it wont be hit)
		}
		else if (obj.tag == "Player" || obj.tag == "PowerUp") {// check if object is Player or PowerUp
			if(obj.tag == "Player"){
				AudioSource.PlayClipAtPoint (MenDieSound ,Camera.main.transform.position);//if it is player then play this audio when the player is destroied.
			}
			Destroy(obj);//destroy the object if it is a Player or PowerUp
			return (origin-pos).magnitude + this.ExplodeInDirection(pos, direction, distance - (origin-pos).magnitude); // Test how much further we can explode
		}

		else if (obj.tag == "Bomb") {// check if object is a bomb
			Bomb bomb = obj.GetComponent<Bomb>();// get bomb class
			bomb.Explode();//instead of destroing the bomb, then it starts its explosion function instead
			return (origin - pos).magnitude + this.ExplodeInDirection(pos, direction, distance - (origin - pos).magnitude); // Test how much further we can explode
		}
		else { // We must have hit a Wall
			return (origin - pos).magnitude - LevelGenerator.gameUnit; // Return the distance to the position before the wall (the wall can't explode)
		}
		
	}


	public void SetBombData(int x, int z, int radius, BombBag playerBag){//set the bobmData...
		this.x = x;
		this.z = z;
		this.explodeRange = radius;
		this.playerBag = playerBag;

	}

}
