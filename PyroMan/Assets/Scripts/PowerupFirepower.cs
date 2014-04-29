using UnityEngine;
using System.Collections;

public class PowerupFirepower : MonoBehaviour {
	
	void OnTriggerEnter (Collider collision) {
		BombBag bag = collision.collider.gameObject.GetComponent<BombBag>();
		bag.IncreaseExplodeRange(1);
		Destroy(this.gameObject);
	}

}
