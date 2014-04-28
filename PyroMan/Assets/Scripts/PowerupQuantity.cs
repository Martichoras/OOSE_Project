using UnityEngine;
using System.Collections;

public class PowerupQuantity : MonoBehaviour {
	
	void OnCollisionEnter (Collision collision) {
		BombBag bag = collision.collider.gameObject.GetComponent<BombBag>();
		bag.maxBombs+=1;
		Destroy(this.gameObject);
	}
	
}
