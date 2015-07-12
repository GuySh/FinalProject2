using UnityEngine;
using System.Collections;

public class MoveUpDown : MonoBehaviour {

	public float speed = 2f; 
	public float total = 200f;
	float now = 0;

	public bool up = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (now >= total)
		{
			if(up)
			{
				up = false;
				now = 0;
			}
			else
			{
				up = true;
				now = 0;
			}
		}

		if (up)
		{
			transform.position = new Vector3 (transform.position.x, transform.position.y + speed*Time.deltaTime, transform.position.z);
			now += speed;
		}
		else
		{
			transform.position = new Vector3 (transform.position.x, transform.position.y - speed*Time.deltaTime, transform.position.z);
			now += speed;
		}

	
	}
}
