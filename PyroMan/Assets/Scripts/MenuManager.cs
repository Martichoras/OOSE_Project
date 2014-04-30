using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
	
	/// <summary>
	/// Enum of button types on the menu.
	/// </summary>
	public enum ItemTypes { Start, Restart, MainMenu, Instructions, Exit };
	/// <summary>
	/// Array of button types for current shown menu.
	/// </summary>
	public ItemTypes[] types;
	/// <summary>
	/// Menu items shown (an item is a button).
	/// </summary>
	public MenuItem[] items;
	/// <summary>
	/// Array index of the currently selected item.
	/// </summary>
	private int currentlySelected = 0;
	/// <summary>
	/// Previous selected item.
	/// </summary>
	private int lastSelected = 0;
	/// <summary>
	/// Determines when the camera animation (rotation) is done.
	/// </summary>
	private bool camIsRotated = true;
	/// <summary>
	/// Speed of the rotation animation.
	/// </summary>
	private float rotSpeed = 50.0f;
	/// <summary>
	/// Determines whether the instructions screen is shown.
	/// </summary>
	private bool showingInstructions = false;

	/// <summary>
	/// Called when the object is instantiated.
	/// </summary>
	void Start () {
		// Selects the first item on load.
		this.items[this.currentlySelected].OnSelect(true);
	}
	
	/// <summary>
	/// Called once per frame.
	/// </summary>
	void Update () {
		if (!this.showingInstructions && this.items[this.currentlySelected].IsAnimDone) {
			float v = Input.GetAxis("Vertical"); // Up and down to move up and down
			float h = Input.GetAxis("Horizontal"); // Left and right to move up and down respectively
			this.lastSelected = this.currentlySelected;

			// v-h means that if up and right are pressed at the same time, the selection won't change as right means down (so, up & down == 0)
			if (v-h > 0.1f) { // Up pressed
				this.currentlySelected--;
				if (this.currentlySelected < 0)
					this.currentlySelected = 0;
			}
			else if (v-h < -0.1f) { // Down pressed
				this.currentlySelected++;
				if (this.currentlySelected >= this.items.Length)
					this.currentlySelected = this.items.Length - 1;
			}

			// Call the objects being deselected and selected respectively.
			if (this.currentlySelected != this.lastSelected) {
				this.items[this.lastSelected].OnSelect(false);
				this.items[this.currentlySelected].OnSelect(true);
			}
		}

		// Select the item
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
			// Go back to main menu if instructions are shown
			if (this.showingInstructions) {
				this.camIsRotated = false;
			}
			else {
				// Check what item is selected and make an action accordingly
				switch (this.types[this.currentlySelected]) {
					case ItemTypes.Start:
					case ItemTypes.Restart:
						Application.LoadLevel("GameScreen");
						break;
					case ItemTypes.MainMenu:
						if (this.showingInstructions) {
							this.camIsRotated = false;
							this.showingInstructions = false;
						}
						else {
							Application.LoadLevel("TitleMenu");
						}
						break;
					case ItemTypes.Instructions:
						this.camIsRotated = false;
						break;
					case ItemTypes.Exit:
						Application.Quit();
						break;
					default:
						break;
				}
			}
		}

		// Animate camera rotation
		if (!this.camIsRotated) {
			this.RotateCamera(this.showingInstructions ? ItemTypes.MainMenu : ItemTypes.Instructions);
		}
	}

	/// <summary>
	/// Rotating the camera a percentage between two fixed points each frame, when the animation is activated.
	/// </summary>
	/// <param name="screen">The screen to show (instructions or main menu)</param>
	private void RotateCamera(ItemTypes screen) {
		// Calculate target rotation
		Quaternion newRot = Camera.main.transform.rotation;
		if (screen == ItemTypes.Instructions) {
			newRot = Quaternion.Euler(63.34f, 0, 0);
		}
		else if (screen == ItemTypes.MainMenu) {
			newRot = Quaternion.Euler(3.115f, 0, 0);
		}
		
		// Rotate. If rotation reached target, stop further rotation
		if ((Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation, newRot, this.rotSpeed * Time.deltaTime)) == newRot) {
			this.camIsRotated = true;
			this.showingInstructions = (screen == ItemTypes.Instructions) ? true : false;
		}
	}
}
