using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public enum ItemTypes { Start, Instructions, Exit };
	public ItemTypes[] types;
	public MenuItem[] items;
	private int currentlySelected = 0;
	private int lastSelected = 0;

	// Use this for initialization
	void Start () {
		this.items[this.currentlySelected].OnSelect(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (this.items[this.currentlySelected].IsAnimDone) {
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

		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
			switch (this.types[this.currentlySelected]) {
				case ItemTypes.Start:
					Application.LoadLevel("GameScreen");
					break;
				case ItemTypes.Instructions:
					// Load an instructions screen or in someway present instructions
					break;
				case ItemTypes.Exit:
					Application.Quit();
					break;
				default:
					break;
			}
		}
	}
}
