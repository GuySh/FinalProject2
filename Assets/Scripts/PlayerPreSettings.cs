using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class PlayerPreSettings : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		Rigidbody2D r;
		r = gameObject.GetComponent<Rigidbody2D> ();
		if (r == null) 
		{
			r = gameObject.AddComponent<Rigidbody2D> ();
		}
		r.mass = 1;
		r.drag = 0.3f;
		r.angularDrag = 0.05f;
		r.gravityScale = 4f;
		r.fixedAngle = true;
		
		CircleCollider2D c;
		c = gameObject.GetComponent<CircleCollider2D>();
		if (c == null) 
		{
			c = gameObject.AddComponent<CircleCollider2D> ();
		}


		c.radius = 0.39f;
		Vector2 vec = new Vector2 ();
		vec.x = 0.03f;
		vec.y = 0.03f;
		c.offset = vec;


	}
	
	// Update is called once per frame
	void Update () {

	}


}
