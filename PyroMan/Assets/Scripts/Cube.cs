using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour {

	float BaseSpeed = 5.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		if (h > 0.0f){
			this.gameObject.transform.Translate(Vector3.right * this.BaseSpeed * Time.deltaTime);
		}
		if (h < 0.0f){
			this.gameObject.transform.Translate(Vector3.left * this.BaseSpeed * Time.deltaTime);
		}
		if (v > 0.0f){
			this.gameObject.transform.Translate(Vector3.forward * this.BaseSpeed * Time.deltaTime);
		}
		if (v < 0.0f){
			this.gameObject.transform.Translate(Vector3.back * this.BaseSpeed * Time.deltaTime);
		}
	}
}
