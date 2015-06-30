using UnityEngine;
using System.Collections;

public class PlayerLeftRight : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Movement ();
	}

	void Movement()
	{
		float speed = 5;
		//Move Right
		if (Input.GetKey (KeyCode.RightArrow)) 
		{

			transform.Translate (Vector2.right * speed * Time.deltaTime);		// set the new position
			transform.eulerAngles = new Vector2(0,0); 
		}
		//Move Left
		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			transform.Translate (Vector2.right * speed * Time.deltaTime);		// set the new position
			transform.eulerAngles = new Vector2(0,180); //flip the character on its x axis
		}
		/*
		if (Input.GetKey (KeyCode.UpArrow)) 
		{

			transform.Translate (Vector2.up * speed * Time.deltaTime);		// set the new position
			transform.eulerAngles = new Vector2(0,0); 
		}
		*/
		
	}

}
