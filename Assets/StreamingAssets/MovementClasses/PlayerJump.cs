using UnityEngine;
using System.Collections;

public class PlayerJump : MonoBehaviour {

	// Use this for initialization
	void Start () {

		CircleCollider2D c;
		c = gameObject.GetComponent<CircleCollider2D>();
		if (c == null) 
		{
			c = gameObject.AddComponent<CircleCollider2D>();
		}
		PhysicsMaterial2D material = new PhysicsMaterial2D ();
		material.bounciness = 1f;
		material.friction = 0.4f;
		c.sharedMaterial = material;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
