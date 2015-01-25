using UnityEngine;
using System.Collections;

public class Addcomponents : MonoBehaviour {

	// Use this for initialization
	void Start () {

		gameObject.AddComponent ("PlayerMovement");		// testint - adding c# class dynamically to any class that compose "Add Components" class
		gameObject.AddComponent ("tester");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
