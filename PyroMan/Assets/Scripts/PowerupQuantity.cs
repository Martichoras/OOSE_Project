using UnityEngine;
using System.Collections;

public class PowerupQuantity : MonoBehaviour {

	void OnTriggerEnter(Collider collision) {
		BombBag bag = collision.collider.gameObject.GetComponent<BombBag>();
		bag.IncreaseMaxBombs(1);
		Destroy(this.gameObject);
	}
	
}
