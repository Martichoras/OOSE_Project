using UnityEngine;
using System.Collections;

public class MenuItem : MonoBehaviour {

	/// <summary>
	/// The z-position of the item when not selected.
	/// </summary>
	private static float inactiveZ = 0;
	/// <summary>
	/// The z-position of the item when selected.
	/// </summary>
	private static float activeZ = -0.5f;
	/// <summary>
	/// The time the animation takes in seconds.
	/// </summary>
	private static float animationSpeed = 5.0f;

	/// <summary>
	/// True if the item is currently selected, otherwise false.
	/// </summary>
	private bool on = false;
	/// <summary>
	/// True if the animation of the item is done so the user can choose another button.
	/// </summary>
	private bool isAnimDone = true;
	public bool IsAnimDone {
		get { return this.isAnimDone; }
	}

	/// <summary>
	/// Toggles the state of the menu item, which will start an animation.
	/// </summary>
	/// <param name="on">If true, the menu item will move towards the screen. Reverse if false.</param>
	public void OnSelect(bool on) {
		this.on = on;
		this.isAnimDone = false;
		this.enabled = true;
	}

	void Update() {
		// Calculate the position at which this item is moving towards
		Vector3 destination = this.transform.position;
		if (this.on) {
			destination.z = MenuItem.activeZ;
		}
		else {
			destination.z = MenuItem.inactiveZ;
		}
		// Move it
		this.transform.position = Vector3.Lerp(this.transform.position, destination, Time.deltaTime * MenuItem.animationSpeed);
		// If close enough, allow user to select another item
		float distance = (this.transform.position - destination).magnitude;
		if (distance < 0.1f) {
			this.isAnimDone = true;
			// If position is practically reached, stop this animation and set position of this item to the destination.
			if (distance < 0.01f) {
				this.transform.position = destination;
				this.enabled = false;
			}
		}
	}
}
