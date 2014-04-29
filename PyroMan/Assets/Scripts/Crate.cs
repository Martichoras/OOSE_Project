using UnityEngine;
using System.Collections;

public class Crate : MonoBehaviour {
	private int x;
	public void SetX(int val){
		x= val;
	}
	private int z;
	public void SetZ(int val){
		z = val;
	}

	//Array to store power-up types defined in Unity
	public GameObject[] powerArray; // 0 = firepower, 1 = quantity, 2 = speed

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy (){
		GameObject level;
		if (level = GameObject.FindGameObjectWithTag ("LevelControl"))
			level.GetComponent<LevelGenerator>().ClearPosition (x, z);
		}
    /// <summary>
    /// The following section is responsible for generating an even amount of 
    /// power-ups randomly split between the spawned crates at random locations
    /// </summary>
	public void OnExplode(){

		int num = Random.Range(0,100); // generates a random number between 0 and 100.
	
		if(num < 45){ // Meaning that approx 45% of crates will contain a power-up
			if(num < 15) 
				Instantiate(powerArray[0], this.transform.position, powerArray[0].transform.rotation); // If random number is below 15, Firepower will be instantiated 
			else if(num < 30) 
				Instantiate(powerArray[1], this.transform.position, powerArray[1].transform.rotation); // If random number is below 30, Quantity will be instantiated
			else 
				Instantiate(powerArray[2], this.transform.position, powerArray[2].transform.rotation); // If random number is below 45, Speed will be instantiated
			
		}
	}
}
