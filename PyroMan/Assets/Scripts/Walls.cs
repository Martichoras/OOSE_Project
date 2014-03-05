
using UnityEngine;
using System.Collections;

public class Walls : MonoBehaviour {
	
	public Transform Wall;
	public Transform Wall_solid;
	
	private const int xSize = 19; 
	private const int zSize = 19;
	
	public int[,] bluePrint = new int[zSize,xSize]		
	{	{ 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
		{ 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
		{ 1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1 },
		{ 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
		{ 1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1 },
		{ 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
		{ 1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1 },
		{ 1,0,0,0,0,0,0,2,2,2,2,2,0,0,0,0,0,0,1 },
		{ 1,0,1,0,1,0,1,2,1,2,1,2,1,0,1,0,1,0,1 },
		{ 1,0,0,0,0,0,0,2,2,0,2,2,0,0,0,0,0,0,1 },
		{ 1,0,1,0,1,0,1,2,1,2,1,2,1,0,1,0,1,0,1 },
		{ 1,0,0,0,0,0,0,2,2,2,2,2,0,0,0,0,0,0,1 },
		{ 1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1 },
		{ 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
		{ 1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1 },
		{ 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
		{ 1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1,0,1 },
		{ 1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
		{ 1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 },
	};
	

	void Start () {

		GameObject[,] maze = new GameObject[zSize,xSize];
		for (int z=0 ; z<zSize ; z++){
			for (int x=0 ; x<xSize ; x++){
				if(bluePrint[z,x] == 1)
					maze[z,x] = Instantiate(Wall_solid, new Vector3(x*2.0f-xSize+1, 0.0f, -z*2.0f+zSize-1), Quaternion.identity) as GameObject;
	
				if(bluePrint[z,x] == 2)
					maze[z,x] = Instantiate(Wall, new Vector3(x*2.0f-xSize+1, 0.0f, -z*2.0f+zSize-1), Quaternion.identity) as GameObject;


			}
		}

	}

	void Update () {
		
	}
}
