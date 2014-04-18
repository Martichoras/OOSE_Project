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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy (){
		GameObject level = GameObject.FindGameObjectWithTag ("LevelControl");
		level.GetComponent<LevelGenerator> ().ClearPosition (x, z);
	}
}
