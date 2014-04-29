using UnityEngine;
using System.Collections;

public class PowerupFirepower : MonoBehaviour {

	//public AudioClip[] powerTaken;
	
	void OnCollisionEnter (Collision collision) {
		BombBag bag = collision.collider.gameObject.GetComponent<BombBag>();
		bag.explodeRange+=2;
		//AudioSource.PlayClipAtPoint (powerTaken [0], transform.position);
		Destroy(this.gameObject);
	}


}
