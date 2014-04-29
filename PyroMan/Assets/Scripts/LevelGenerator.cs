
using UnityEngine;
using System.Collections;



public class LevelGenerator : MonoBehaviour {

	// enum containing the different object types e.g. player is defined as 'x'
	public enum ObjectType {
		Player = 'x',
		Path = 0,
		Wall_solid = 1,
		Crate = 2,
		Bomb = 4,
		PowerUp = 5,
	};

	public GameObject Crate;
	public GameObject Wall_solid;
	public GameObject []Player;
	/// <summary>
	/// The game unit.
	/// </summary>
	public static float gameUnit = 2.0f;

	private const int xSize = 23; 
	private const int zSize = 19;

	/// <summary>
	/// The level generator responsible for storing the 2D-array of game objects.
	/// </summary>
	public int[,] LevelGen = new int[zSize,xSize]		
	{	{ 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
		{ 1,'x',0,2,2,0,0,0,2,2,2,2,2,0,2,2,0,2,2,2,2,0,1 },
		{ 1,0,1,2,1,0,1,0,1,2,1,0,1,2,1,2,1,0,1,0,1,0,1 },
		{ 1,2,2,2,2,2,0,2,0,2,2,2,2,2,0,2,2,2,2,2,2,2,1 },
		{ 1,0,1,0,1,2,1,2,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1 },
		{ 1,2,2,0,2,2,0,2,2,2,0,0,2,2,2,2,2,2,0,2,2,2,1 },
		{ 1,0,1,2,1,2,1,2,1,0,1,0,1,0,1,0,1,0,1,2,1,2,1 },
		{ 1,0,2,2,2,2,0,2,2,2,2,2,0,2,2,2,2,2,2,2,2,2,1 },
		{ 1,0,1,2,1,0,1,2,1,2,1,2,1,2,1,0,1,2,1,2,1,2,1 },
		{ 1,2,2,2,2,2,0,2,2,0,2,2,0,2,2,2,0,2,2,2,0,2,1 },
		{ 1,0,1,0,1,2,1,2,1,2,1,2,1,2,1,0,1,0,1,2,1,2,1 },
		{ 1,2,0,2,0,2,0,2,2,2,2,2,0,2,0,2,2,2,0,2,2,2,1 },
		{ 1,2,1,2,1,2,1,0,1,0,1,2,1,2,1,0,1,0,1,0,1,0,1 },
		{ 1,2,0,2,0,2,0,2,2,2,2,2,0,2,0,2,0,2,2,2,2,0,1 },
		{ 1,2,1,2,1,2,1,2,1,0,1,2,1,2,1,2,1,2,1,2,1,2,1 },
		{ 1,0,2,2,2,0,0,2,2,2,0,2,0,2,0,2,2,0,2,2,2,2,1 },
		{ 1,2,1,0,1,0,1,0,1,2,1,2,1,2,1,2,1,2,1,2,1,0,1 },
		{ 1,2,2,2,0,2,2,2,2,0,0,2,2,2,0,2,0,2,0,2,0,'x',1 },
		{ 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
	};

	private bool isGameOver = false;
	private float gameOverCounter = 0.0f;

	void Start () {
		this.enabled = false;
		int playerCount = 1;
		GameObject[,] level = new GameObject[zSize,xSize];
		for (int z=0 ; z<zSize ; z++){
			for (int x=0 ; x<xSize ; x++){
				if(LevelGen[z,x] == (int)ObjectType.Wall_solid)
					level[z,x] = PlaceObject(Wall_solid,x,z,ObjectType.Wall_solid) as GameObject;
				
				if(LevelGen[z,x] == (int)ObjectType.Crate){
					level[z,x] = PlaceObject(Crate,x,z,ObjectType.Crate) as GameObject;
					Crate crate = level[z,x].GetComponent<Crate>();
					crate.SetX(x);
					crate.SetZ(z);
				}
				if(LevelGen[z,x] == (int)ObjectType.Player){

					level[z,x] = PlaceObject(Player[playerCount-1],x,z,ObjectType.Player) as GameObject;
					Character player = level[z,x].GetComponent<Character>();
					player.SetPlayer(playerCount);
					player.SetX(x);
					player.SetZ(z);
					playerCount++;
					LevelGen[z,x] = 0;

				}
			}
		}
		
	}
	
	void Update () {
		if (this.isGameOver) {
			if (Time.timeSinceLevelLoad - this.gameOverCounter > 2.0f) // 2 seconds have passed since the second last player died
				Application.LoadLevel("GameOverScreen");
		}
	}
	// CheckPosition FUNCTION 
	/// <summary>
	/// 
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public int CheckPosition(int x,int z)
	{
		return LevelGen[z,x];

	}
	/// <summary>
	/// Places the object.
	/// </summary>
	/// <returns>The object.</returns>
	/// <param name="original">Original.</param>
	/// <param name="x">The x coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	public Object PlaceObject(Object original, int x, int z, ObjectType type)
	{
		LevelGen[z,x] = (int)type;
		return Instantiate(original, new Vector3(x*gameUnit-xSize+1, 0.0f, -z*gameUnit+zSize-1),( original as GameObject).transform.rotation);

	}

	public void ClearPosition(int x, int z){

		LevelGen[z,x] = (int)ObjectType.Path;


	}

	public void OnGameOver() {
		GameObject[] playersLeft = GameObject.FindGameObjectsWithTag("Player");
		if (playersLeft.Length == 1) {
			this.enabled = true;
			this.isGameOver = true;
			this.gameOverCounter = Time.timeSinceLevelLoad;

			Character player = playersLeft[0].GetComponent<Character>();
			GameStats.OnGameWon(player.GetPlayer());
		}
	}

}

