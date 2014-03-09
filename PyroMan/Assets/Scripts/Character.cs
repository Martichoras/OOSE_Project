using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	//===========================================================
	//===================  Object attributes  ===================
	//===========================================================

	/// <summary>
	/// Reference to the object that keeps track of objects in the level.
	/// </summary>
	public GameObject levelControl;

	//---------- Movement related ----------
	/// <summary>
	/// The x-position of the character.
	/// </summary>
	private int x;
	/// <summary>
	/// The y-position of the character.
	/// </summary>
	private int z;
	/// <summary>
	/// The target position that the character is currently trying to move towards.
	/// </summary>
	private Vector3 target;
	/// <summary>
	/// Tells whether the character is moving.
	/// </summary>
	private bool isMoving;
	/// <summary>
	/// Speed at which the character is moving.
	/// </summary>
	private float moveSpeed = 10.0f;

	//------------ Bomb related ------------
	//public BombManager bombMng;

	//===============================================
	//===================  Enums  ===================
	//===============================================
	
	/// <summary>
	/// Enum of the different directions the character can move in.
	/// </summary>
	private enum Direction{
		Right,
		Up,
		Left,
		Down
	};

	//===============================================================
	//===================  MonoBehaviour methods  ===================
	//===============================================================

	/// <summary>
	/// Start is called when the object is instantiated and is ready to be used.
	/// </summary>
	void Start () {
		if (this.levelControl == null){
			this.levelControl = GameObject.FindWithTag("LevelControl");
		}
		this.isMoving = false;
	}

	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	void Update () {

		// Get the input values from the user (arrow keys or WASD)
		float h = Input.GetAxis("Horizontal"); // Left & right
		float v = Input.GetAxis("Vertical"); // Up & down

		// Checks if the user is pressing a movement key
		if (h > 0.1f){ // Right
			this.CheckMove(Direction.Right);
		}
		else if (h < -0.1f){ // Left
			this.CheckMove(Direction.Left);
		}
		else if (v > 0.1f){ // Up
			this.CheckMove(Direction.Up);
		}
		else if (v < -0.1f){ // Down
			this.CheckMove(Direction.Down);
		}
		// Move the character
		if (this.isMoving)
			this.Move();

		if (Input.GetButton("Bomb")){
			this.PlaceBomb();
		}

	}

	//=========================================================
	//===================  Private methods  ===================
	//=========================================================


	/// <summary>
	/// Check if a movement in the desired direction is valid.
	/// If so, the target attribute will be set to the desired destination.
	/// </summary>
	/// <param name="dir">Direction to move in</param>
	private void CheckMove(Direction dir) {
		// If the character is already moving towards a position, abort new movement.
		if (this.isMoving)
			return;

		// Check if the character can move in the dir direction
		switch (dir){
		case Direction.Right:
			//if (this.levelControl.GetComponent<TYPE>().CHECKPOSITION(this.x + 1, this.z))
				this.target = new Vector3(this.transform.position.x + 2.0f, 0.0f, this.transform.position.z);
			break;
		case Direction.Left:
			//if (this.levelControl.GetComponent<TYPE>().CHECKPOSITION(this.x - 1, this.z))
				this.target = new Vector3(this.transform.position.x - 2.0f, 0.0f, this.transform.position.z);
			break;
		case Direction.Up:
			//if (this.levelControl.GetComponent<TYPE>().CHECKPOSITION(this.x, this.z + 1))
				this.target = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z + 2.0f);
			break;
		case Direction.Down:
			//if (this.levelControl.GetComponent<TYPE>().CHECKPOSITION(this.x, this.z - 1))
				this.target = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z - 2.0f);
			break;
		default:
			break;
		}

		// Abort movement if requested position was not free
		if (	this.target.x == this.transform.position.x
		    &&	this.target.z == this.transform.position.z)
			return;
		else
			this.isMoving = true;
	}

	/// <summary>
	/// Move the character towards the target position and stop when the position is reached.
	/// </summary>
	private void Move() {
		this.transform.position = Vector3.MoveTowards(this.transform.position, this.target, Time.deltaTime * this.moveSpeed);
		this.isMoving = this.transform.position == this.target ? false:true;
	}

	/// <summary>
	/// Place a bomb beneath the character.
	/// </summary>
	private void PlaceBomb() {
		//this.bombMng.PutBomb();
	}
}
