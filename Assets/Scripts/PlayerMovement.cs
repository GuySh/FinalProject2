using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {


	public float speed; //set the speed value of the main character
	public int jumpHeight; //set the jump height value of the main character

	bool left, right;


	// Use this for initialization
	void Start () {

		speed = 5;				//set the speed
		jumpHeight = 300;		//set the jump height

	}
	
	// Update is called once per frame
	void Update () {


		Movement (); //call the movement function below

		var absVelY = Mathf.Abs (rigidbody2D.velocity.y);
		
		if (absVelY == 0)						//Jump by detecting if is touching the ground
		{
			rigidbody2D.AddForce (Vector2.up * jumpHeight);			// set the new force values
			Debug.Log("Grounded");				// print to console
		}
		else
		{

		}
	
	}


	void Movement()
	{
		//Move Right
		if (Input.GetKey (KeyCode.RightArrow) || right) 
		{
			transform.Translate (Vector2.right * speed * Time.deltaTime);		// set the new position
			transform.eulerAngles = new Vector2(0,0); 
		}
		//Move Left
		if (Input.GetKey (KeyCode.LeftArrow) || left) 
		{
			transform.Translate (Vector2.right * speed * Time.deltaTime);		// set the new position
			transform.eulerAngles = new Vector2(0,180); //flip the character on its x axis
		}
	
		
	}


	public void setLeft()		//set the left flag for the touch screen 
	{
		left = true;
	}

	public void setRight()		//set the right flag for the touch screen 
	{
		right = true;
	}

	public void clearLeftRight()		//clear the right and right flag
	{
		left = false;
		right = false;
	}

}
