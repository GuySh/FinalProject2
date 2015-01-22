using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {


	public float speed; //declare the player move speed in Unity inspector
	public int jumpHeight; //this is set in the Unity Inspector
	public bool isGrounded = false; //this can be seen working in the Unity inspector
	public Transform groundedEnd; //declares the empty game object in Unity acting as a collider set to the position of the player
	public float distToGround;

	// Use this for initialization
	void Start () {

		speed = 5;
		jumpHeight = 300;

	}
	
	// Update is called once per frame
	void Update () {


		Movement (); //call the movement function below

		var absVelY = Mathf.Abs (rigidbody2D.velocity.y);
		
		if (absVelY == 0)
		{
			rigidbody2D.AddForce (Vector2.up * jumpHeight);
			Debug.Log("Grounded");
		}
		else
		{

		}
		//isGrounded = Physics2D.Linecast(this.transform.position, groundedEnd.position, 1 << LayerMask.NameToLayer("Ground")); 
		//the above line of code draws a linecast downwards to detect the ground game objects that have been placed in a ground layer
	
	}


	void Movement()
	{
		//Move Right
		if (Input.GetKey (KeyCode.RightArrow)) 
		{
			transform.Translate (Vector2.right * speed * Time.deltaTime);
			transform.eulerAngles = new Vector2(0,0); 
		}
		//Move Left
		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			transform.Translate (Vector2.right * speed * Time.deltaTime);
			transform.eulerAngles = new Vector2(0,180); //flip the character on its x axis
		}
		//Jump by detecting if the user presses W and then checking to see if the linecast is touching the ground
		
	}
	
}
