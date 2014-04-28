using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	//===========================================================
	//===================  Object attributes  ===================
	//===========================================================

	/// <summary>
	/// Reference to the object that keeps track of objects in the level.
	/// </summary>
	private LevelGenerator levelControl = null;

	//---------- Movement related ----------
	/// <summary>
	/// Player number.
	/// </summary>
	private int player;
	public int GetPlayer() { return this.player; }
	public void SetPlayer(int val) { this.player = val; }
	/// <summary>
	/// The x-position of the character.
	/// </summary>
	private int x;
	public void SetX(int val) { this.x = val; }
	/// <summary>
	/// The y-position of the character.
	/// </summary>
	private int z;
	public void SetZ(int val) { this.z = val; }
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
	private BombBag bombBag;

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
			this.levelControl = GameObject.FindWithTag("LevelControl").GetComponent<LevelGenerator>();
		}
		if (this.bombBag == null){
			MonoBehaviour[] scripts = this.GetComponents<MonoBehaviour>();
			foreach (MonoBehaviour elem in scripts){
				if (elem.GetType() == typeof(BombBag))
					this.bombBag = (BombBag)elem;
			}
		}
		this.isMoving = false;
		this.target = this.transform.position;
	}

	/// <summary>
	/// Update is called once per frame.
	/// </summary>
	void Update () {

		float h = 0, v = 0;
		// Get the input values from the user (arrow keys or WASD)
		if (this.player == 1) {
			h = Input.GetAxis("HorizontalP1"); // Left & right
			v = Input.GetAxis("VerticalP1"); // Up & down
			if (Input.GetButtonDown("BombP1")) {
				this.PlaceBomb();
				Debug.Log("bomb P1");
			}
		}
		else if (this.player == 2) {
			h = Input.GetAxis("HorizontalP2"); // Left & right
			v = Input.GetAxis("VerticalP2"); // Up & down
			if (Input.GetButtonDown("BombP2")) {
				this.PlaceBomb();
				Debug.Log("bomb P2");
			}
		}

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
			if (this.levelControl.CheckPosition(this.x + 1, this.z) == (int)LevelGenerator.ObjectType.Path) {
				this.target = new Vector3(this.transform.position.x + 2.0f, 0.0f, this.transform.position.z);
				this.x++;
			}
			break;
		case Direction.Left:
			if (this.levelControl.CheckPosition(this.x - 1, this.z) == (int)LevelGenerator.ObjectType.Path) {
				this.target = new Vector3(this.transform.position.x - 2.0f, 0.0f, this.transform.position.z);
				this.x--;
			}
			break;
		case Direction.Up:
			if (this.levelControl.CheckPosition(this.x, this.z - 1) == (int)LevelGenerator.ObjectType.Path) {
				this.target = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z + 2.0f);
				this.z--;
			}
			break;
		case Direction.Down:
			if (this.levelControl.CheckPosition(this.x, this.z + 1) == (int)LevelGenerator.ObjectType.Path){
				this.target = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z - 2.0f);
				this.z++;
			}
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
		this.bombBag.PlaceBomb(this.x, this.z);
	}

	void OnDestroy() {
		if (this.levelControl)
			this.levelControl.OnGameOver();
	}
}
