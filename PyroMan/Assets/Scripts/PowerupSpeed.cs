using UnityEngine;
using System.Collections;

public class PowerupSpeed : MonoBehaviour {
	
	void OnCollisionEnter (Collision collision) {
		Character movement = collision.collider.gameObject.GetComponent<Character>();
		movement.moveSpeed+=1.0f;
		Destroy(this.gameObject);
	}
	
}
