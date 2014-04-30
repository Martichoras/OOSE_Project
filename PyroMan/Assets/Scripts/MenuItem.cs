using UnityEngine;
using System.Collections;

public class MenuItem : MonoBehaviour {

	/// <summary>
	/// The distance between the two points used to distinguish whether the item is active (selected) or not.
	/// </summary>
	private static float animMoveDist = 0.5f;
	/// <summary>
	/// The time the animation should take in seconds.
	/// </summary>
	private static float animationSpeed = 5.0f;
	/// <summary>
	/// Destination of the animation (the point at which the item should stop the animation)
	/// </summary>
	private Vector3 dest;
	/// <summary>
	/// True if the animation of the item is done so the user can choose another button.
	/// </summary>
	private bool isAnimDone = true;
	public bool IsAnimDone {
		get { return this.isAnimDone; }
	}

	/// <summary>
	/// Called immediately after instantiation of the object
	/// </summary>
	void Awake() {
		this.enabled = false;
	}

	/// <summary>
	/// Called every frame
	/// </summary>
	void Update() {
		
		// Move it
		this.transform.position = Vector3.Lerp(this.transform.position, this.dest, Time.deltaTime * MenuItem.animationSpeed);
		// If close enough, allow user to select another item
		float distance = (this.transform.position - this.dest).magnitude;
		if (distance < 0.1f) {
			this.isAnimDone = true;
			// If position is practically reached, stop this animation and set position of this item to the destination.
			if (distance < 0.01f) {
				this.transform.position = this.dest;
				this.enabled = false;
			}
		}
	}

	/// <summary>
	/// Toggles the state of the menu item, which will start an animation.
	/// </summary>
	/// <param name="on">If true, the menu item will move towards the screen. Reverse if false.</param>
	public void OnSelect(bool on) {
		if (on)
			this.dest = this.transform.position + this.transform.InverseTransformDirection(Vector3.back) * MenuItem.animMoveDist;
		else
			this.dest = this.transform.position + this.transform.InverseTransformDirection(Vector3.forward) * MenuItem.animMoveDist;
		
		this.isAnimDone = false;
		this.enabled = true;
	}
}
