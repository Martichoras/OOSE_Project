using UnityEngine;
using System.Collections;
/// <summary>
/// Script for the firepower-powerup.
/// </summary>
public class PowerupFirepower : MonoBehaviour {

	public AudioClip[] powerUpSound;// audio array

	void OnTriggerEnter (Collider collision) { // Activates function when game object collides with player trigger
		BombBag bag = collision.collider.gameObject.GetComponent<BombBag>(); // Retrieves info from BombBag.cs
        bag.IncreaseExplodeRange(1); // Increases the explode range by adding 1 to IncreaseExplodeRange in BombBag.cs
		AudioSource.PlayClipAtPoint(powerUpSound [Random.Range (0, powerUpSound.Length)], Camera.main.transform.position, .50F);// playes a random sound from powerUpSound array
		Destroy(this.gameObject); // Power-up is removed from scene
	}



}
