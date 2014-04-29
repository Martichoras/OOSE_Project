using UnityEngine;
using System.Collections;

public class PowerupSpeed : MonoBehaviour {

	void OnTriggerEnter(Collider collision) {
		Character movement = collision.collider.gameObject.GetComponent<Character>();
		movement.IncreaseMoveSpeed(1.0f);
		Destroy(this.gameObject);
	}
	
}
