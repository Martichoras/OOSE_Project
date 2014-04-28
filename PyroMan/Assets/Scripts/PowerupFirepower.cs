using UnityEngine;
using System.Collections;

public class PowerupFirepower : MonoBehaviour {
	
	void OnCollisionEnter (Collision collision) {
		BombBag bag = collision.collider.gameObject.GetComponent<BombBag>();
		bag.explodeRange+=2;
		Destroy(this.gameObject);
	}

}
