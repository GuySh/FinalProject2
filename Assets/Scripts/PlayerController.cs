using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Vector2 moving  = new Vector2();
	public bool up,left,right;

	// Use this for initialization
	void Start () {
		up = false;
		left = false;
		right = false;
	}
	
	// Update is called once per frame
	void Update () {


		moving.x = moving.y = 0;


		if (Input.GetKey ("right") || right) 
		{
			moving.x = 1;
		}
		else if (Input.GetKey ("left") || left) 
		{
			moving.x = -1;	
		}


		if (Input.GetKey ("up") || up)
		{
			moving.y = 1;
		}
		else if (Input.GetKey ("down")) 
		{
			moving.y = -1;	
		}
			
	}

	public void moveUp()
	{
		up = true;
	}

	public void moveLeft()
	{
		left = true;
	}

	public void moveRight()
	{
		right = true;
	}

	public void clearUp()
	{
		up = false;
	}
	
	public void clearLeft()
	{
		left = false;
	}
	
	public void clearRight()
	{
		right = false;
	}
	
}
