using UnityEngine;
using System.Collections;
/// <summary>
/// Script for the Speed-powerup.
/// </summary>
public class PowerupSpeed : MonoBehaviour {

	public AudioClip[] powerUpSound;
	
	void OnTriggerEnter(Collider collision) { // Activates function when game object collides with player trigger
        Character movement = collision.collider.gameObject.GetComponent<Character>(); // Retrieves info from Character.cs
        movement.IncreaseMoveSpeed(1.0f); // Increases movement speed by adding 1 to IncreaseMoveSpeed in Character.cs
		AudioSource.PlayClipAtPoint(powerUpSound [Random.Range (0, powerUpSound.Length)], Camera.main.transform.position);
		Destroy(this.gameObject); // Power-up is removed from scene
	}
	
}
