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

	//Array to store power-up types
	public GameObject[] powerArray;

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

	public void OnExplode(){

		int num = Random.Range(0,100);
	
		if(num < 45){
			if(num < 15)
				Instantiate(powerArray[0], this.transform.position, powerArray[0].transform.rotation);
			else if(num < 30) 
				Instantiate(powerArray[1], this.transform.position, powerArray[1].transform.rotation);
			else 
				Instantiate(powerArray[2], this.transform.position, powerArray[2].transform.rotation);
			
		}
	}
}
