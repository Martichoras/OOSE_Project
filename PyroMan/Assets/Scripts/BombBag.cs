using UnityEngine;
using System.Collections;

public class BombBag : MonoBehaviour {
	public GameObject bomb;
// object atributes 

	/// <summary>
	/// The bombplaced. tells how many not exploded bombs the player have placed in the game
	/// </summary>
	private int bombsPlaced;

	private int maxBombs;

	private int explodeRange;

	//methods

	// Use this for initialization
	void Start () {
	
		bombsPlaced = 0;
	//maxBomb
		maxBombs = 1;

		explodeRange = 1;
	}
	

	public void PlaceBomb(int x, int z){
		//check if possilbe to place bomb
		//1. if it has any bombs "left" to place
		if (maxBombs > bombsPlaced){
			//2. if possilbe place bomb under the player - Call bomb class
			Instantiate (
		}

		//3. call levelGenerator - and change 0 to eg 3 in the the array
		//4. increase bombPlaced
		 

	}

}
//arraycast - colliders 