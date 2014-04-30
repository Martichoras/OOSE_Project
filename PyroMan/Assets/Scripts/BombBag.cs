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
	public void IncreaseMaxBombs(int val) { this.maxBombs += val; }//increases the number of maxBombs the player are allowed to carry by "val"

	public int explodeRange;
	public void IncreaseExplodeRange(int val) { this.explodeRange += val; }//increases the number of explodeRange by "val"

	private LevelGenerator level;

	void Start () {

		GameObject temp = GameObject.FindGameObjectWithTag ("LevelControl");//finds levelGenerator
		level = temp.GetComponent<LevelGenerator> ();// saved the script in level

		bombsPlaced = 0;
	//maxBomb
		maxBombs = 1;

		explodeRange = 1;
	}
	

	public void PlaceBomb(int x, int z){
		//check if possilbe to place bomb
		//1. if it has any bombs "left" to place
		if (maxBombs > bombsPlaced){
		
			//2. call levelGenerator - and change 0 to eg 3 in the the array
			GameObject bombObject = this.level.PlaceObject(bomb,x,z, LevelGenerator.ObjectType.Bomb) as GameObject;

			//3.calls the Bomb class 
			Bomb bombScript = bombObject.GetComponent<Bomb>(); 
			bombScript.SetBombData(x,z, explodeRange, this);//

		//4. increase bombPlaced
			bombsPlaced++;

		}

	}
	public void AfterExplosion(int x, int z){
		bombsPlaced--;//5. descrease bombPlaced and thereby allow the player to place a new bomb
		level.ClearPosition(x,z);//level calls ClearPosition for x,z 
	}

}