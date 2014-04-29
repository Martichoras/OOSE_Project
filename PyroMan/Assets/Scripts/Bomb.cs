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
	private bool isExploded = false;

	//attach the fire/explosion particles to the bomb script
	public GameObject ExplosionPrefab;

	//the explotion sound
	public AudioClip ExplosionSound;

	 void Start (){
	}

	void Update(){
		//timerCount
		timer += Time.deltaTime;
		// checker if time is equal to or bigger than timerMax, is important because it is floats
		if (timer >= timerMax) {
			Explode ();
		}

	}

	public void Explode(){
		if (this.isExploded)
			return;
		
		this.isExploded = true;
		//Makes the bomb say an explosion sound
		AudioSource.PlayClipAtPoint(ExplosionSound,transform.position);

		float up, down, left, right;
		float unit = LevelGenerator.gameUnit;
		// Check how far we can explode in each direction
		right = this.ExplodeInDirection(this.transform.position, new Vector3(1, 0, 0), this.explodeRange * unit);
		left = this.ExplodeInDirection(this.transform.position, new Vector3(-1, 0, 0), this.explodeRange * unit);
		up = this.ExplodeInDirection(this.transform.position, new Vector3(0, 0, 1), this.explodeRange * unit);
		down = this.ExplodeInDirection(this.transform.position, new Vector3(0, 0, -1), this.explodeRange * unit);

		// Animate the explosion with some spawned particles
		Instantiate(ExplosionPrefab, this.transform.position, Quaternion.identity);

		for (float i = 1; i <= explodeRange; i++) {
			float range = i * unit;
			if (range <= right){
				Instantiate(ExplosionPrefab, this.transform.position + new Vector3(unit*i, 0, 0), Quaternion.identity);
			}
			if (range <= left){
				Instantiate(ExplosionPrefab, this.transform.position + new Vector3(-unit*i, 0, 0), Quaternion.identity);
			}
			if (range <= up){
				Instantiate(ExplosionPrefab, this.transform.position + new Vector3(0, 0, unit*i), Quaternion.identity);	
			}
			if (range <= down){
				Instantiate(ExplosionPrefab, this.transform.position + new Vector3(0, 0, -unit*i), Quaternion.identity);
			}
		}
		
		//call playerBag to say that bomb is exploded/destroyed - so bombBag will create new bomb. This is done by decresing bombPlaced
		//Instantiate the explosion and explode!!!
		Destroy (gameObject);
		playerBag.AfterExplosion (x,z);
	}

	private float ExplodeInDirection(Vector3 origin, Vector3 direction, float distance) {
		if (distance <= 0.0f)
			return 0;

		RaycastHit hit;
		if (!Physics.Raycast(origin, direction, out hit, distance))
			return distance;

		GameObject obj = hit.collider.gameObject;
		Vector3 pos = obj.transform.position;

		if (obj.tag == "Crate") {
			Crate crate = obj.GetComponent<Crate>();
			crate.OnExplode();
			Destroy(obj);
			return (origin-pos).magnitude; // Return the distance to the crate (the crate will absorb the explosion so things behind it wont be hit)
		}
		else if (obj.tag == "Player" || obj.tag == "PowerUp") {
			Destroy(obj);
			return (origin-pos).magnitude + this.ExplodeInDirection(pos, direction, distance - (origin-pos).magnitude); // Test how much further we can explode
		}
		else if (obj.tag == "Bomb") {
			Bomb bomb = obj.GetComponent<Bomb>();
			bomb.Explode();
			return (origin - pos).magnitude + this.ExplodeInDirection(pos, direction, distance - (origin - pos).magnitude); // Test how much further we can explode
		}
		else { // We must have hit a wall
			return (origin - pos).magnitude - LevelGenerator.gameUnit; // Return the distance to the position before the wall (the wall can't explode)
		}
		
	}


	public void SetBombData(int x, int z, int radius, BombBag playerBag){
		this.x = x;
		this.z = z;
		this.explodeRange = radius;
		this.playerBag = playerBag;

	}
	//audio
//	void OnCollisionEnter (Collision collision) {

		//}

}
