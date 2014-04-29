using UnityEngine;
using System.Collections;

public class PowerupSpeed : MonoBehaviour {

	public AudioClip[] powerUpSound;

	void OnTriggerEnter(Collider collision) {
		Character movement = collision.collider.gameObject.GetComponent<Character>();
		movement.IncreaseMoveSpeed(1.0f);
		AudioSource.PlayClipAtPoint(powerUpSound [Random.Range (0, powerUpSound.Length)], Camera.main.transform.position);
		Destroy(this.gameObject);
	}
	
}
