using UnityEngine;
using System.Collections;
/// <summary>
/// Script for the firepower-powerup.
/// </summary>
public class PowerupFirepower : MonoBehaviour {

	void OnTriggerEnter (Collider collision) { // Activates function when game object collides with player trigger
		BombBag bag = collision.collider.gameObject.GetComponent<BombBag>(); // Retrieves info from BombBag.cs
        bag.IncreaseExplodeRange(1); // Increases the explode range by adding 1 to IncreaseExplodeRange in BombBag.cs
		Destroy(this.gameObject); // Power-up is removed from scene
	}

}
