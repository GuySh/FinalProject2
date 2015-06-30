using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {


	public float speed; //set the speed value of the main character

	bool left, right;


	// Use this for initialization
	void Start () {

		speed = 5;				//set the speed

		Rigidbody2D r = gameObject.AddComponent<Rigidbody2D> ();
		r.mass = 1;
		r.drag = 0.3f;
		r.angularDrag = 0.05f;
		r.gravityScale = 4f;
		r.fixedAngle = true;
		
		CircleCollider2D c = gameObject.AddComponent<CircleCollider2D>();
		PhysicsMaterial2D material = new PhysicsMaterial2D ();
		material.bounciness = 1f;
		material.friction = 0.4f;
		c.sharedMaterial = material;
		c.radius = 0.37f;
		Vector2 vec = new Vector2 ();
		vec.x = 0.03f;
		vec.y = 0.03f;
		c.offset = vec;

	}
	
	// Update is called once per frame
	void Update ()
	{
		Movement (); //call the movement function below
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
