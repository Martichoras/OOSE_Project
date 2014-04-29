using UnityEngine;
using System.Collections;
/// <summary>
///  Script for the Bomb Quantity-powerup. (increase carry-capacity)
/// </summary>
public class PowerupQuantity : MonoBehaviour {

	void OnTriggerEnter(Collider collision) { // Activates function when game object collides with player trigger
        BombBag bag = collision.collider.gameObject.GetComponent<BombBag>(); // Retrieves info from BombBag.cs
        bag.IncreaseMaxBombs(1); // Increases amount of bombs by adding 1 to IncreaseMaxBombs in BombBag.cs
        Destroy(this.gameObject); // Power-up is removed from scene
	}
	
}
