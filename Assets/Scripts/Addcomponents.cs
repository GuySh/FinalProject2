using UnityEngine;
using System.Collections;

public class Addcomponents : MonoBehaviour {

	// Use this for initialization
	void Start () {
		gameObject.AddComponent ("PlayerMovement");
		gameObject.AddComponent ("tester");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
