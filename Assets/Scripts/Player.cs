using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 10f; 
	public Vector2 maxVelocity = new Vector2(3, 5);
	public bool standing;
	public float jetSpeed = 15f;
	public float airSpeedMultiplier = .3f;
	
	public PlayerController controller;
	//public TouchCode controller;
	//private TouchCode controller;

	void Start (){
		controller = GetComponent<PlayerController> ();
		//controller = GetComponent<TouchCode> ();
	}

	// Update is called once per frame
	void Update () {
		var forceX = 0f;
		var forceY = 0f;
	
		var absVelX = Mathf.Abs (rigidbody2D.velocity.x);
		var absVelY = Mathf.Abs (rigidbody2D.velocity.y);

		if (absVelY < .2f)
		{
			standing = true;
		}
		else
		{
			standing = false;
		}



		if (controller.moving.x != 0) {
			if (absVelX < maxVelocity.x)
			{
				forceX = standing ? speed * controller.moving.x : (speed * controller.moving.x * airSpeedMultiplier);

				float f = transform.localScale.x;
				if(f > 0)
				{
					f = f*-1;
				}
				transform.localScale = new Vector3 (forceX > 0 ? f : f*-1, transform.localScale.y, transform.localScale.z);
			}

			}
			else 
			{

			}


		if (controller.moving.y > 0) {
			if (absVelY < maxVelocity.y) 
			{
				forceY = jetSpeed * controller.moving.y;
			}


		}
		else if (absVelY > 0) 
		{

		}

		if (Input.GetKey ("up")) 
		{
			if(absVelY < maxVelocity.y)
			{
				forceY = jetSpeed;
			}
		}

		rigidbody2D.AddForce (new Vector2 (forceX, forceY));
	}
}
