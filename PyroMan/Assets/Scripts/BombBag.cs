using UnityEngine;
using System.Collections;

public class BombBag : MonoBehaviour {
	public GameObject bomb;
	// object atributes 

	/// <summary>
	/// The bombplaced. tells how many not exploded bombs the player have placed in the game
	/// </summary>
	private int bombsPlaced;

	public int maxBombs;

	public int explodeRange;

	private LevelGenerator level;
	//methods

	// Use this for initialization
	void Start () {

		GameObject temp = GameObject.FindGameObjectWithTag ("LevelControl");//finds levelGenerator
		level = temp.GetComponent<LevelGenerator> ();// saved the script in level

		bombsPlaced = 0;
	//maxBomb
		maxBombs = 1;

		explodeRange = 10;
	}
	

	public void PlaceBomb(int x, int z){
		//check if possilbe to place bomb
		//1. if it has any bombs "left" to place
		if (maxBombs > bombsPlaced){
			//2. if possilbe place bomb under the player - Call bomb class - Emil dose this
		
			//3. call levelGenerator - and change 0 to eg 3 in the the array
			GameObject bombObject = this.level.PlaceObject(bomb,x,z, LevelGenerator.ObjectType.Bomb) as GameObject;

			Bomb bombScript = bombObject.GetComponent<Bomb>(); 
			bombScript.SetBombData(x,z, explodeRange, this);

		//4. increase bombPlaced
			bombsPlaced++;

		}

	}
	public void AfterExplosion(int x, int z){
		bombsPlaced--;
		level.ClearPosition(x,z);
	}

}