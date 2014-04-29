using UnityEngine;
using System.Collections;

public class PowerupQuantity : MonoBehaviour {

	public AudioClip[] powerUpSound;

	void OnTriggerEnter(Collider collision) {
		BombBag bag = collision.collider.gameObject.GetComponent<BombBag>();
		bag.IncreaseMaxBombs(1);
		AudioSource.PlayClipAtPoint(powerUpSound [Random.Range (0, powerUpSound.Length)], Camera.main.transform.position, .50F);
		Destroy(this.gameObject);
	}
	
}
